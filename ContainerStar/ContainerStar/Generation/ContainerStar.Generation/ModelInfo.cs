using System.Collections.Generic;
using System.Linq;
using MetadataLoader.Contracts.CSharp;
using MetadataLoader.Contracts.Database;
using MetadataLoader.EntityFramework;

namespace ContainerStar.Generation
{
    public sealed class ModelInfo : ClassInfo
    {
        #region Constructor
        internal ModelInfo(EntityInfo entity, ITable<TableContent, ColumnContent> table, List<PropertyModelInfo> properties)
        {
            IsPartial = true;
            Entity = entity;
            Table = table;
            ModelProperties = properties;

            Name = Entity.Name + "Model";
            Description = string.Format("Model for <see cref=\"{0}\"/> entity", entity.Name);
        }
        #endregion
        #region	Public properties
        public TableContent Content
        {
            get { return Table.Content; }
        }
        public EntityInfo Entity { get; set; }
        public ITable<TableContent, ColumnContent> Table { get; set; }

        public override IEnumerable<PropertyInfo> Properties
        {
            get { return ModelProperties.Union(base.Properties); }
        }
        public List<PropertyModelInfo> ModelProperties { get; private set; }
        #endregion
    }
}