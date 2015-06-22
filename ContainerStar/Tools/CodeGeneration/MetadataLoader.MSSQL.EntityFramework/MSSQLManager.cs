using System.Collections.Generic;
using MetadataLoader.Contracts.Database;
using MetadataLoader.EntityFramework;
using MetadataLoader.EntityFramework.Extractors;
using MetadataLoader.MSSQL.Contracts.Database;

namespace MetadataLoader.MSSQL.EntityFramework
{
    public sealed class MSSQLManager<TTableContent, TColumnContent> : MSSQLLoadManager<TTableContent, TColumnContent>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        public new static readonly MSSQLManager<TTableContent, TColumnContent> Instance = new MSSQLManager<TTableContent, TColumnContent>();

        public IEntityLoader<MSSQLTable<TTableContent, TColumnContent>> GetEntityLoader(
            IEntityExtractor<IBaseObject<TTableContent>> entityExtractor = null,
            IEntityPropertyExtractor<ITable<TTableContent, TColumnContent>, TTableContent, IColumn<TColumnContent>, TColumnContent> propertyExtractor = null,
            IEntityRelationshipExtractor<ITable<TTableContent, TColumnContent>, TTableContent, IColumn<TColumnContent>, TColumnContent> relationshipExtractor = null,
            IEntityHandler<TTableContent, EntityInfo>[] tableContentHandlers = null,
            IEntityHandler<TColumnContent, SimplePropertyEntityInfo>[] propertyContentHandlers = null)
        {
            var tableHandlers = new List<IEntityHandler<MSSQLTable<TTableContent, TColumnContent>, EntityInfo>>();
            var columnHandlers = new List<IEntityHandler<MSSQLColumn<TTableContent, TColumnContent>, SimplePropertyEntityInfo>>
            {
                new MSSQLColumnHandler<TTableContent, TColumnContent>(),
                new MSSQLColumnDefaultValuetHandler<TTableContent, TColumnContent>()
            };

            if (tableContentHandlers != null && tableContentHandlers.Length != 0)
            {
                tableHandlers.Add(new ContentEntityHandler<MSSQLTable<TTableContent, TColumnContent>, TTableContent, EntityInfo>(tableContentHandlers));
            }

            if (propertyContentHandlers != null && propertyContentHandlers.Length != 0)
            {
                columnHandlers.Add(new ContentEntityHandler<MSSQLColumn<TTableContent, TColumnContent>, TColumnContent, SimplePropertyEntityInfo>(propertyContentHandlers));
            }

            var loader = new EntityLoader<MSSQLTable<TTableContent, TColumnContent>, TTableContent, MSSQLColumn<TTableContent, TColumnContent>, TColumnContent>
                (entityExtractor, propertyExtractor, relationshipExtractor, tableHandlers, columnHandlers);

            return loader;
        }
    }
}