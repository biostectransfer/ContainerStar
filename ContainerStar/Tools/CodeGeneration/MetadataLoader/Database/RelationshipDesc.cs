using System.Diagnostics.Contracts;

namespace MetadataLoader.Database
{
    public sealed class RelationshipDesc<TTableKey, TColumnKey>
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public RelationshipDesc(TTableKey fromTable, TColumnKey[] fromColumns, TTableKey toTable, TColumnKey[] toColumns, bool willCascadeOnDelete = false)
        {
            Contract.Requires(fromColumns != null);
            Contract.Requires(fromColumns.Length > 0);
            Contract.Requires(toColumns != null);
            Contract.Requires(toColumns.Length > 0);

            FromTable = fromTable;
            FromColumns = fromColumns;
            ToTable = toTable;
            ToColumns = toColumns;

            WillCascadeOnDelete = willCascadeOnDelete;
        }
        #endregion
        #region	Public properties
        public TTableKey FromTable { get; private set; }
        public TColumnKey[] FromColumns { get; private set; }
        public TTableKey ToTable { get; private set; }
        public TColumnKey[] ToColumns { get; private set; }
        public bool WillCascadeOnDelete { get; set; }
        #endregion
    }
}