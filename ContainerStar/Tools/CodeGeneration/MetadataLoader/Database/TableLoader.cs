using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using MetadataLoader.Contracts;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.Database
{
    public sealed class TableLoader<TTableKey, TColumnKey, TTable, TTableContent, TColumn, TColumnContent> : ITableLoader<TTable>
        where TTable : Table<TTable, TTableContent, TColumn, TColumnContent>
        where TColumn : Column<TTable, TTableContent, TColumn, TColumnContent>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        #region Private fields
        private readonly IDbObjectExtractor<TTableKey, TColumnKey, TTable, TTableContent, TColumn, TColumnContent> _extractor;
        private readonly IAdditionalExtractor<TTableKey, TTable, TColumnKey, TColumn>[] _additionalExtractors;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public TableLoader(IDbObjectExtractor<TTableKey, TColumnKey, TTable, TTableContent, TColumn, TColumnContent> extractor,
            params IAdditionalExtractor<TTableKey, TTable, TColumnKey, TColumn>[] additionalExtractors)
        {
            Contract.Requires(extractor != null);

            _extractor = extractor;
            _additionalExtractors = additionalExtractors;
        }
        #endregion
        #region Public methods
        public List<TTable> Load(ILog log = null, Func<TTable, bool> tableFilter = null, TableLoadOptions options = TableLoadOptions.None)
        {
            log = log ?? EmptyLog.Instance;
            tableFilter = tableFilter ?? (table => true);
            var tables = _extractor.ExtractTables().ToDictionary(pair => pair.Key, pair => new TableDesc(pair.Value));
            #region Columns
            foreach (var columnPair in _extractor.ExtractColumns())
            {
                TableDesc table;
                if (!tables.TryGetValue(columnPair.Key.Item1, out table))
                {
                    //TODO: Log error
                    continue;
                }
                table.AddColumn(columnPair.Key.Item2, columnPair.Value);
            }
            #endregion
            #region Keys
            foreach (var key in _extractor.ExtractKeys())
            {
                TableDesc table;
                if (!tables.TryGetValue(key.Table, out table))
                {
                    if (options.HasFlag(TableLoadOptions.SkipBrokenRelationship))
                    {
                        //TODO: Log error
                    }
                    continue;
                }
                //TODO: Log missed columns
                var columns = key.Columns.Select(cKey => table.GetColumn(cKey)).ToArray();

                if (!tableFilter(table.Table))
                {
                    continue;
                }
                var i = 0;
                foreach (var column in columns)
                {
                    column.KeyOrder = i++;
                }
            }
            #endregion
            #region Additional extractors
            if (_additionalExtractors != null)
            {
                foreach (var extractor in _additionalExtractors)
                {
                    extractor.Run(log, key => tables[key].Table, (tKey, cKey) => tables[tKey].GetColumn(cKey));
                }
            }
            #endregion
            if (!options.HasFlag(TableLoadOptions.SkipRelationships))
            {
                #region Relationships
                foreach (var relationship in _extractor.ExtractRelationships())
                {
                    TableDesc fromTable;
                    if (!tables.TryGetValue(relationship.FromTable, out fromTable))
                    {
                        if (options.HasFlag(TableLoadOptions.SkipBrokenRelationship))
                        {
                            //TODO: Log error
                        }
                        continue;
                    }
                    //TODO: Log missed columns
                    var fromColumns = relationship.FromColumns.Select(key => fromTable.GetColumn(key)).ToArray();


                    TableDesc toTable;
                    if (!tables.TryGetValue(relationship.ToTable, out toTable))
                    {
                        if (options.HasFlag(TableLoadOptions.SkipBrokenRelationship))
                        {
                            //TODO: Log error
                        }
                        continue;
                    }
                    //TODO: Log missed columns
                    var toColumns = relationship.ToColumns.Select(key => toTable.GetColumn(key)).ToArray();

                    if (!tableFilter(fromTable.Table) || !tableFilter(toTable.Table))
                    {
                        continue;
                    }
                    TableHelper<TTable, TTableContent, TColumn, TColumnContent>
                        .AddRelationship(fromTable.Table, fromColumns, toTable.Table, toColumns, relationship.WillCascadeOnDelete);
                }
                #endregion
            }
            return tables.Select(pair => pair.Value.Table).Where(tableFilter).ToList();
        }
        #endregion
        //--
        #region Nested type: TableDesc
        private sealed class TableDesc
        {
            #region Constructor
            /// <summary>
            ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
            /// </summary>
            public TableDesc(TTable table)
            {
                Table = table;
                Columns = new List<KeyValuePair<TColumnKey, TColumn>>();
            }
            #endregion
            #region	Public properties
            public TTable Table { get; private set; }
            public List<KeyValuePair<TColumnKey, TColumn>> Columns { get; private set; }
            #endregion
            #region	Public methods
            public void AddColumn(TColumnKey key, TColumn column)
            {
                Columns.Add(new KeyValuePair<TColumnKey, TColumn>(key, column));
                TableHelper<TTable, TTableContent, TColumn, TColumnContent>.AddColumn(Table, column);
                column.Parent = Table;
            }
            public TColumn GetColumn(TColumnKey key)
            {
                return Columns.First(pair => Equals(pair.Key, key)).Value;
            }
            #endregion
        }
        #endregion
    }
}