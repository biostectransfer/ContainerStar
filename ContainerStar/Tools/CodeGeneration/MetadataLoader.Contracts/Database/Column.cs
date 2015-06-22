using MetadataLoader.Contracts.CSharp;

namespace MetadataLoader.Contracts.Database
{
    public abstract class Column<TTable, TTableContent, TColumn, TColumnContent> : Base<TColumnContent>
        , IColumn<TColumnContent>
        where TTable : Table<TTable, TTableContent, TColumn, TColumnContent>
        where TColumn : Column<TTable, TTableContent, TColumn, TColumnContent>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        #region Public properties
        public TTable Parent { get; set; }
        public bool IsKeyColumn
        {
            get { return KeyOrder != null; }
        }
        public int? KeyOrder { get; set; }
        public abstract TypeUsageInfo TypeInfo { get; }
        public bool IsRequired { get; set; }
        #endregion
        #region	Public methods
        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}