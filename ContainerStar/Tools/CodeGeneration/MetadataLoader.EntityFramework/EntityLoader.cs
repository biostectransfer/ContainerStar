using System;
using System.Collections.Generic;
using System.Linq;
using MetadataLoader.Contracts;
using MetadataLoader.Contracts.CSharp;
using MetadataLoader.Contracts.Database;
using MetadataLoader.Contracts.Generation;
using MetadataLoader.EntityFramework.Extractors;

namespace MetadataLoader.EntityFramework
{
    public class EntityLoader<TTable, TTableContent, TColumn, TColumnContent> : IEntityLoader<TTable>
        where TTable : Table<TTable, TTableContent, TColumn, TColumnContent>
        where TColumn : Column<TTable, TTableContent, TColumn, TColumnContent>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        private readonly IEntityRelationshipExtractor<TTable, TTableContent, TColumn, TColumnContent> _relationshipExtractor;
        #region	Private fields
        private readonly IEntityExtractor<TTable> _entityExtractor;
        private readonly IEntityPropertyExtractor<TTable, TTableContent, TColumn, TColumnContent> _propertyExtractor;
        private readonly IReadOnlyCollection<IEntityHandler<TTable, EntityInfo>> _tableHandlers;
        private readonly IReadOnlyCollection<IEntityHandler<TColumn, SimplePropertyEntityInfo>> _simplePropertiesHandlers;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public EntityLoader(IEntityExtractor<TTable> entityExtractor = null,
            IEntityPropertyExtractor<TTable, TTableContent, TColumn, TColumnContent> propertyExtractor = null,
            IEntityRelationshipExtractor<TTable, TTableContent, TColumn, TColumnContent> relationshipExtractor = null,
            IReadOnlyCollection<IEntityHandler<TTable, EntityInfo>> tableHandlers = null,
            IReadOnlyCollection<IEntityHandler<TColumn, SimplePropertyEntityInfo>> simplePropertiesHandlers = null)
        {
            _entityExtractor = entityExtractor ?? new EntityExtractor<TTable, TTableContent>();
            _propertyExtractor = propertyExtractor ?? new EntityPropertyExtractor<ITable<TTableContent, TColumnContent>, TTableContent, IColumn<TColumnContent>, TColumnContent>();
            _relationshipExtractor = relationshipExtractor;

            _tableHandlers = tableHandlers ?? new IEntityHandler<TTable, EntityInfo>[0];
            _simplePropertiesHandlers = simplePropertiesHandlers ?? new IEntityHandler<TColumn, SimplePropertyEntityInfo>[0];
        }
        #endregion
        #region	Public methods
        public List<EntityInfo> Load(IEnumerable<TTable> inputTables, ILog log = null)
        {
            log = log ?? EmptyLog.Instance;
            var tables = inputTables.ToArray();
            var dict = new Dictionary<TTable, EntityInfo>();

            foreach (var table in tables)
            {
                var entityName = GetName(table, _entityExtractor.GetName,
                    s => dict.Values.Any(t => t.Name == s),
                    log, "Duplicate table name '{0}'.");

                var entity = new EntityInfo
                {
                    Name = entityName,
                    TableSchemaName = table.Schema,
                    TableName = table.Name,
                    EntitySetName = table.Content.CodeNamePlural,
                    Description = table.Description
                };
                dict.Add(table, entity);
                #region Columns
                foreach (var column in table.Columns)
                {
                    var data = new PropertyMetadata<TTable, TTableContent, TColumn, TColumnContent>(table, column);


                    var name = GetName(data, _propertyExtractor.GetSimplePropertyName,
                        s => entity.Properties.Any(p => p.Name == s),
                        log, string.Format("Duplicate property name '{{0}}' in entity '{0}'.", entity.Name));

                    var typeInfo = _propertyExtractor.GetSimplePropertyTypeInfo(data);

                    var property = new SimplePropertyEntityInfo(name, typeInfo)
                    {
                        ColumnName = column.Name,
                        Description = column.Description,
                        IsKey = column.IsKeyColumn,
                        IsRequired = column.IsRequired
                    };
                    if (column.KeyOrder.HasValue)
                    {
                        property.KeyOrder = column.KeyOrder.Value;
                    }
                    foreach (var handler in _simplePropertiesHandlers)
                    {
                        handler.Run(column, property);
                    }
                    entity.SimpleProperties.Add(property);
                }
                #endregion
            }
            #region Navigation properties
            //TODO: Implement relationship recognition after custom indexes!!!!! one - one one,zero - one etc. 
            //TODO: Implement many to many

            foreach (var relation in tables.SelectMany(table => table.FromRelations))
            {
                var fromEntity = dict[relation.FromTable];
                var fromType = fromEntity.GetKeyProperties(relation.FromColumns).Any(p => !p.IsRequired) ? NavigationType.Optional : NavigationType.Required;
                var fromSide = GetRelationSideMetadata(relation.FromTable, relation.FromColumns.ToArray(), dict[relation.FromTable], fromType);

                var toSide = GetRelationSideMetadata(relation.ToTable, relation.ToColumns.ToArray(), dict[relation.ToTable], NavigationType.Many);


                var data = new RelationshipData<TTable, TTableContent, TColumn, TColumnContent>(fromSide, toSide) {WillCascadeOnDelete = relation.WillCascadeOnDelete};
                if (!_relationshipExtractor.HandleAndApproveRelationship(data))
                {
                    continue;
                }

                var from = GetRelationSide(log, new NavigationPropertyNameData<TTable, TColumn>(data, NavigationPropertyName.From));
                var to = GetRelationSide(log, new NavigationPropertyNameData<TTable, TColumn>(data, NavigationPropertyName.To));

                AddNavPropertyToEntity(from);
                AddNavPropertyToEntity(to);

                var relationship = new EntityRelationship(from, to, data.WillCascadeOnDelete);
                from.Entity.Relations.Add(relationship);
                to.Entity.Relations.Add(relationship);
            }
            #endregion
            #region Final handling
            foreach (var entity in dict)
            {
                foreach (var handler in _tableHandlers)
                {
                    handler.Run(entity.Key, entity.Value);
                }
            }
            #endregion

            //TODO: Need resolve duplicated names in tables and columns
            return dict.Values.ToList();
        }
        private EntityRelationSide GetRelationSide(ILog log, INavigationPropertyNameData<TTable, TColumn> nameData)
        {
            var sideData = nameData.TypeSide;
            var otherSideData = nameData.NameSide;

            var name = GetName(nameData, _relationshipExtractor.GetNavigationPropertyName,
                s => sideData.Entity.Properties.Any(p => p.Name == s),
                log, string.Format("Duplicate property name '{{0}}' in entity '{0}'.", sideData.Entity.Name));

            NavigationPropertyEntityInfo property = null;
            if (!sideData.IsUndirect)
            {
                switch (sideData.Type)
                {
                    case NavigationType.Optional:
                    case NavigationType.Required:
                        property = NavigationPropertyEntityInfo.One(name, otherSideData.Entity);
                        break;
                    case NavigationType.Many:
                        property = NavigationPropertyEntityInfo.Many(name, otherSideData.Entity);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return new EntityRelationSide(sideData.Entity, property, sideData.Type, sideData.GetKeyProperties());
        }
        private static RelationshipSideData<TTable, TTableContent, TColumn, TColumnContent> GetRelationSideMetadata(TTable table, TColumn[] columns, EntityInfo entity, NavigationType type)
        {
            return new RelationshipSideData<TTable, TTableContent, TColumn, TColumnContent>(table, columns, entity, type);
        }
        private static void AddNavPropertyToEntity(EntityRelationSide side)
        {
            if (side.IsUndirect)
            {
                return;
            }
            side.Entity.NavigationProperties.Add(side.Property);
            if (side.NavigationType != NavigationType.Many)
            {
                side.Entity.AddProperty(new PropertyInfo(string.Format("Has{0}", side.Property.Name),
                    typeof (bool).ToUsageInfo(),
                    new PropertyInvokerInfo(string.Format("return !ReferenceEquals({0}, null);", side.Property.Name))));
            }
        }
        private static string GetName<T>(T data, Func<T, Func<string, bool>, string> get, Func<string, bool> check, ILog log, string message)
        {
            var name = get(data, check);
            if (!check(name))
            {
                return name;
            }

            var result = name.GenerateUniqueName(check);
            log.SendWarning(string.Format(message, name) + string.Format(" Generated name '{0}' will be used", result));
            return result;
        }
        #endregion
    }
}