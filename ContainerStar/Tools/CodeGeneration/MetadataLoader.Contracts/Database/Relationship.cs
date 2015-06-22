using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace MetadataLoader.Contracts.Database
{
    public sealed class Relationship<TTable, TTableContent, TColumn, TColumnContent>
        where TTable : Table<TTable, TTableContent, TColumn, TColumnContent>
        where TColumn : Column<TTable, TTableContent, TColumn, TColumnContent>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public Relationship(TTable fromTable, IReadOnlyCollection<TColumn> fromColumns,
            TTable toTable, IReadOnlyCollection<TColumn> toColumns, bool willCascadeOnDelete = false)
        {
            Contract.Requires(fromTable != null);
            Contract.Requires(fromColumns != null);
            Contract.Requires(fromColumns.Count > 0);
            Contract.Requires(toTable != null);
            Contract.Requires(toColumns != null);
            Contract.Requires(toColumns.Count > 0);

            FromTable = fromTable;
            FromColumns = fromColumns;
            ToTable = toTable;
            ToColumns = toColumns;
            WillCascadeOnDelete = willCascadeOnDelete;
        }
        #endregion
        #region	Public properties
        public TTable FromTable { get; private set; }
        public IReadOnlyCollection<TColumn> FromColumns { get; private set; }
        public TTable ToTable { get; private set; }
        public IReadOnlyCollection<TColumn> ToColumns { get; private set; }
        public bool WillCascadeOnDelete { get; private set; }
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
            return string.Format("{0}({1}) -> {2}({3})",
                FromTable, string.Join(", ", FromColumns.Select(column => column.Name)),
                ToTable, string.Join(", ", ToColumns.Select(column => column.Name)));
        }
        #endregion
    }
}