using MetadataLoader.Contracts.CSharp;
using MetadataLoader.Contracts.Database;
using MetadataLoader.EntityFramework;

namespace ContainerStar.Generation
{
    public sealed class PropertyModelInfo : PropertyInfo
    {
        #region Constructor
        public PropertyModelInfo(SimplePropertyEntityInfo property, IColumn<ColumnContent> column)
            : base(column.Content.CodeNameCamelCase, property.Type)
        {
            Property = property;
            Column = column;
        }
        #endregion
        #region	Public properties
        public SimplePropertyEntityInfo Property { get; set; }
        public IColumn<ColumnContent> Column { get; set; }

        public ColumnContent Content
        {
            get { return Column.Content; }
        }
        #endregion
    }
}