using System.Collections.Generic;
using System.Linq;
using MetadataLoader.Contracts.CSharp;

namespace MetadataLoader.EntityFramework
{
    public sealed class EntityInfo : ClassInfo
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public EntityInfo()
        {
            Accessibility = AccessibilityLevels.Public;
            IsPartial = true;

            SimpleProperties = new List<SimplePropertyEntityInfo>();
            NavigationProperties = new List<NavigationPropertyEntityInfo>();
            Relations=new List<EntityRelationship>();
        }
        #endregion
        #region	Public properties
        public string EntitySetName { get; set; }
        public ClassInfo Mapping
        {
            get
            {
                var mappingClass = new ClassInfo
                {
                    Name = Name + "Mapping",
                    Accessibility = AccessibilityLevels.Internal,
                    Description = string.Format("Mappping table {0} to entity <see cref=\"{1}\"/>", GetFullTableName(), Name),
                    IsSealed = true
                };

                mappingClass.InheritsFrom(TypeUsageInfo.CreateClass("EntityTypeConfiguration",
                    "System.Data.Entity.ModelConfiguration",
                    new[] {GetTypeUsage()}));
                return mappingClass;
            }
        }
        public string TableSchemaName { get; set; }
        public string TableName { get; set; }
        public string GetFullTableName()
        {
            return string.IsNullOrWhiteSpace(TableSchemaName) ? TableName : TableSchemaName + "." + TableName;
        }

        public override IEnumerable<PropertyInfo> Properties
        {
            get
            {
                return SimpleProperties.Cast<PropertyInfo>()
                    .Union(NavigationProperties)
                    .Union(base.Properties);
            }
        }
        public List<SimplePropertyEntityInfo> SimpleProperties { get; private set; }
        public List<NavigationPropertyEntityInfo> NavigationProperties { get; private set; }
        public List<EntityRelationship> Relations { get; private set; }
        #endregion
        #region	Public methods
        public IEnumerable<EntityRelationship> GetToRelationships()
        {
            return Relations.Where(r => r.To.Entity == this);
        }
        public IEnumerable<EntityRelationship> GetFromRelationships()
        {
            return Relations.Where(r => r.From.Entity == this);
        }
        #endregion
    }
}