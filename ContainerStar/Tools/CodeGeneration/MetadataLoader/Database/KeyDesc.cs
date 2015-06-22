using System.Diagnostics.Contracts;

namespace MetadataLoader.Database
{
    public sealed class KeyDesc<TTableKey, TColumnKey>
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public KeyDesc(TTableKey table, TColumnKey[] columns)
        {
            Contract.Requires(columns != null);
            Contract.Requires(columns.Length > 0);

            Table = table;
            Columns = columns;
        }
        #endregion
        #region	Public properties
        public TTableKey Table { get; private set; }
        public TColumnKey[] Columns { get; private set; }
        #endregion
    }
}