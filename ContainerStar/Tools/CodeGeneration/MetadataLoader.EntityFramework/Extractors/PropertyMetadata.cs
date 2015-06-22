using MetadataLoader.Contracts.Database;

namespace MetadataLoader.EntityFramework.Extractors
{
    internal sealed class PropertyMetadata<TTable, TTableContent, TColumn, TColumnContent> : IPropertyMetadata<TTable, TTableContent, TColumn, TColumnContent>
        where TTable : ITable<TTableContent, TColumnContent> 
        where TColumn : IColumn<TColumnContent>
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public PropertyMetadata(TTable table, TColumn column)
        {
            Table = table;
            Column = column;
        }
        #endregion
        #region	Public properties
        public TTable Table { get; private set; }
        public TColumn Column { get; private set; }
        #endregion
        
    }
}