using MetadataLoader.Contracts.Database;

namespace MetadataLoader.EntityFramework.Extractors
{
    internal sealed class RelationshipData<TTable, TTableContent, TColumn, TColumnContent> : IRelationshipData<TTable, TColumn>
        where TTable : ITable<TTableContent, TColumnContent>
        where TColumn : IColumn<TColumnContent>
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public RelationshipData(IRelationshipSideData<TTable, TColumn> from, IRelationshipSideData<TTable, TColumn> to)
        {
            From = from;
            To = to;
        }
        #endregion
        #region	Public properties
        public IRelationshipSideData<TTable, TColumn> From { get; private set; }
        public IRelationshipSideData<TTable, TColumn> To { get; private set; }
        public bool WillCascadeOnDelete { get; set; }
        #endregion
    }
}