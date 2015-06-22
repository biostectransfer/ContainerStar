using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MetadataLoader.Contracts.Database;
using MetadataLoader.Contracts.Utils;
using MetadataLoader.Excel;
using MetadataLoader.Excel.OfficeOpenXML;

namespace MetadataLoader.Content
{
    public sealed class ExcelContentLoader<TTable, TTableContent, TColumn, TColumnContent> : IExcelContentLoader
        where TTable : Table<TTable, TTableContent, TColumn, TColumnContent>
        where TColumn : Column<TTable, TTableContent, TColumn, TColumnContent>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        #region Static
        private static TableDescriptor GetTableDescriptor(ExcelWorksheet worksheet, bool hasSchema, ContentLoadType type)
        {
            var countOfKeys = 1;
            if (hasSchema)
            {
                countOfKeys++;
            }
            if (type.HasFlag(ContentLoadType.Column))
            {
                countOfKeys++;
            }
            var descriptor = TableDescriptor.GetReadDynamic(worksheet.Name, new TableKeyDescriptor((new[] {1, 2, 3}).Take(countOfKeys).ToArray()), 2);
            return descriptor;
        }
        private static ContentLoadType GetType(string name)
        {
            var match = WorksheetNameRegEx.Match(name);
            if (!match.Success)
            {
                return ContentLoadType.None;
            }

            switch (match.Groups["type"].Value.ToLower())
            {
                case "t":
                    return ContentLoadType.Table;
                case "c":
                    return ContentLoadType.Column;
                case "ct":
                case "tc":
                    return ContentLoadType.TableOrColumn;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
        private readonly ResourceFileExtractor _extractor = new ResourceFileExtractor();

        private static readonly Regex WorksheetNameRegEx = new Regex(@".*\{(?<type>C|T|(CT)|(TC))\}",
            RegexOptions.CultureInvariant | RegexOptions.Compiled);
        #region	Private fields
        private readonly List<TTable> _tables;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public ExcelContentLoader(List<TTable> tables)
        {
            _tables = tables;
        }
        #endregion
        #region	Public methods
        public void Load(string filePath, bool hasSchema = true)
        {
            Load(new ContentLoaderConfiguration(filePath), hasSchema);
        }
        public void Load(ContentLoaderConfiguration configuration, bool hasSchema = true)
        {
            using (var package = ExcelPackage.Open(configuration.FilePath))
            {
                foreach (var worksheet in package.Workbook.Worksheets)
                {
                    var type = GetType(worksheet.Name);
                    if (type == ContentLoadType.None)
                    {
                        continue;
                    }

                    var descriptor = GetTableDescriptor(worksheet, hasSchema, type);
                    var searcher = new Searcher(_tables, descriptor, type, hasSchema);
                    var columnNames = OpenXmlManager.ReadColumnsNames(package, descriptor);

                    IContentMapper<TTableContent> tableContentMapper = null;
                    if (type.HasFlag(ContentLoadType.Table))
                    {
                        tableContentMapper = new ContentMapper<TTableContent>(columnNames);
                    }
                    IContentMapper<TColumnContent> columnContentMapper = null;
                    if (type.HasFlag(ContentLoadType.Column))
                    {
                        columnContentMapper = new ContentMapper<TColumnContent>(columnNames);
                    }

                    var currentRow = descriptor.BeginRow;
                    foreach (var data in OpenXmlManager.Read(package, descriptor))
                    {
                        currentRow++;

                        TTable table;
                        TColumn column;
                        searcher.Search(data, out table, out column);

                        if (column != null && columnContentMapper != null)
                        {
                            columnContentMapper.Map(column.Content, data);
                            continue;
                        }
                        if (!type.HasFlag(ContentLoadType.Table))
                        {
                            //TODO: Log not found column if type is Column
                            continue;
                        }
                        if (table != null && tableContentMapper != null)
                        {
                            tableContentMapper.Map(table.Content, data);
                            // ReSharper disable once RedundantJumpStatement
                            continue;
                        }
                        //TODO: Log not found table
                    }
                }
            }
        }
        public void CreateDefaultTemplate(string filePath, ContentLoadType type, bool hasSchema = true)
        {
            string[] names;
            #region Names
            switch (type)
            {
                case ContentLoadType.Column:
                case ContentLoadType.TableOrColumn:
                    names = hasSchema ? new[] {"Schema", "Table", "Column"} : new[] {"Table", "Column"};
                    break;
                case ContentLoadType.Table:
                    names = hasSchema ? new[] {"Schema", "Table"} : new[] {"Table"};
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            #endregion
            var descriptor = TableDescriptor.GetWrite("template", names);
            var set = new DataSet();
            var dataTable = new DataTable(descriptor.SheetName);
            set.Tables.Add(dataTable);
            #region Fill table
            foreach (var columnName in descriptor.ColumnNames)
            {
                dataTable.Columns.Add(columnName, typeof (string));
            }

            var getTableRow = hasSchema
                ? (Func<TTable, object[]>) (table => new object[] {table.Schema, table.Name})
                : (table => new object[] {table.Name});
            var getColumnRow = hasSchema
                ? (Func<TColumn, object[]>) (column => new object[] {column.Parent.Schema, column.Parent.Name, column.Name})
                : (column => new object[] {column.Parent.Name, column.Name});

            foreach (var table in _tables)
            {
                if (type.HasFlag(ContentLoadType.Table))
                {
                    dataTable.Rows.Add(getTableRow(table));
                }

                if (type.HasFlag(ContentLoadType.Column))
                {
                    foreach (var column in table.Columns)
                    {
                        dataTable.Rows.Add(getColumnRow(column));
                    }
                }
            }
            #endregion
            #region Create file
            using (var sourceStream = _extractor.ReadFileFromResToStream("model.xlsx"))
            using (var destinationStream = File.Create(filePath))
            {
                sourceStream.CopyTo((destinationStream));
            }
            #endregion
            DataSheetManager.Write(filePath, set, descriptor);
        }
        #endregion
        //--
        #region Nested type: Searcher
        private sealed class Searcher
        {
            #region	Private fields
            private readonly int _columnKeyIndex;
            private readonly List<TTable> _tables;
            private readonly Func<string[], Func<TTable, bool>> _tablePredicate;
            #endregion
            #region Constructor
            /// <summary>
            ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
            /// </summary>
            public Searcher(List<TTable> tables, TableDescriptor descriptor, ContentLoadType type, bool hasSchema = true, Func<TTable, string[], bool> customPredicate = null)
            {
                Contract.Requires(descriptor != null);
                Contract.Requires(type.HasFlag(ContentLoadType.Table));
                Contract.Requires((type.HasFlag(ContentLoadType.Table) && descriptor.Key.Items.Length >= 1));
                Contract.Requires((type.HasFlag(ContentLoadType.Column) && descriptor.Key.Items.Length >= 2));

                _tables = tables;

                var shift = 1;
                _columnKeyIndex = -1;
                if (type.HasFlag(ContentLoadType.Column))
                {
                    _columnKeyIndex = descriptor.Key.Items[descriptor.Key.Items.Length - shift++] - 1;
                }
                var schemaKeyIndex = -1;
                var tableKeyIndex = descriptor.Key.Items[descriptor.Key.Items.Length - shift++] - 1;

                if (hasSchema && shift < descriptor.Key.Items.Length)
                {
                    schemaKeyIndex = descriptor.Key.Items[descriptor.Key.Items.Length - shift] - 1;
                }

                var additionalIndexes = descriptor.Key.Items.Take(descriptor.Key.Items.Length - shift).Select(i => i - 1).ToArray();

                if (additionalIndexes.Length != 0)
                {
                    if (customPredicate == null)
                    {
                        throw new ArgumentNullException("customPredicate");
                    }
                    if (schemaKeyIndex >= 0)
                    {
                        _tablePredicate = dataRow =>
                        {
                            var schema = dataRow[schemaKeyIndex];
                            var name = dataRow[tableKeyIndex];
                            var additional = dataRow.Where((s, i) => additionalIndexes.Contains(i)).ToArray();
                            return table => table.Schema == schema && table.Name == name && customPredicate(table, additional);
                        };
                    }
                    else
                    {
                        _tablePredicate = dataRow =>
                        {
                            var name = dataRow[tableKeyIndex];
                            var additional = dataRow.Where((s, i) => additionalIndexes.Contains(i)).ToArray();
                            return table => table.Name == name && customPredicate(table, additional);
                        };
                    }
                }
                else
                {
                    if (schemaKeyIndex >= 0)
                    {
                        _tablePredicate = dataRow =>
                        {
                            var schema = dataRow[schemaKeyIndex];
                            var name = dataRow[tableKeyIndex];
                            return table => table.Schema == schema && table.Name == name;
                        };
                    }
                    else
                    {
                        _tablePredicate = dataRow =>
                        {
                            var name = dataRow[tableKeyIndex];
                            return table => table.Name == name;
                        };
                    }
                }
            }
            #endregion
            #region	Public methods
            public void Search(string[] rowData, out TTable table, out TColumn column)
            {
                table = _tables.FirstOrDefault(_tablePredicate(rowData));
                column = null;
                if (table != null && _columnKeyIndex >= 0)
                {
                    var colName = rowData[_columnKeyIndex];
                    column = table.Columns.FirstOrDefault(c => c.Name == colName);
                }
            }
            #endregion
        }
        #endregion
    }
}