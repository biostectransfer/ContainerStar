using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MetadataLoader.Content;
using MetadataLoader.Excel;
using MetadataLoader.Excel.OfficeOpenXML;

namespace MetadataLoader.TestApp.PrepareData
{
    public sealed class DataMigration
    {
        private static TableDescriptor GetTableDescriptor(ExcelWorksheet worksheet, ContentLoadType type)
        {
            var countOfKeys = 2;
            if (type.HasFlag(ContentLoadType.Column))
            {
                countOfKeys++;
            }
            var descriptor = TableDescriptor.GetReadDynamic(worksheet.Name, new TableKeyDescriptor((new[] {1, 2, 3}).Take(countOfKeys).ToArray()), 2);
            return descriptor;
        }

        private readonly string _path;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public DataMigration(string path)
        {
            _path = path;
        }

        public void Migrate()
        {
            var tables = new List<TableRow>();
            var columns = new List<ColumnRow>();

            foreach (var entry in Directory.EnumerateFileSystemEntries(_path, "*.xlsx"))
            {
                Load(tables, columns, entry);
            }

            using (var stream = File.Create(Path.Combine(_path, "tables.csv")))
            using (var writer = new StreamWriter(stream, Encoding.Unicode))
            {
                foreach (var t in tables)
                {
                    writer.WriteLine(string.Join(";", t.Schema, t.Table, t.Context, t.Group, t.SubGroup, t.IsRelated, t.PrimaryObject, t.ViewCollections, t.DeName, t.EnName));
                }
            }

            using (var stream = File.Create(Path.Combine(_path, "columns.csv")))
            using(var writer= new StreamWriter(stream,Encoding.Unicode))
            {
                foreach (var c in columns)
                {
                    writer.WriteLine(string.Join(";", c.Schema, c.Table, c.Column, c.Context, c.InModel, c.IsModelRequired, c.DE, c.EN));
                }
            }
        }

        public void Load(List<TableRow> tables, List<ColumnRow> columns, string file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file).Split('.');
            var context = fileName[0];
            var group = fileName[1];
            var subGroup = fileName[2];

            using (var package = ExcelPackage.Open(file))
            {
                foreach (var worksheet in package.Workbook.Worksheets)
                {
                    var sheetName = worksheet.Name;
                    TableDescriptor descriptor;
                    Action<string[]> addRow;
                    if (worksheet.Name == "Tables")
                    {
                        descriptor = GetTableDescriptor(worksheet, ContentLoadType.Table);
                        addRow = row => tables.Add(GetTableRow(context, group, subGroup, sheetName, row));
                    }
                    else
                    {
                        descriptor = GetTableDescriptor(worksheet, ContentLoadType.Column);
                        addRow = row => columns.Add(GetColumnRow(context, group, subGroup, sheetName, row));
                    }

                    foreach (var data in OpenXmlManager.Read(package, descriptor))
                    {
                        addRow(data);
                    }
                }
            }
        }

        private static readonly Regex parseTableName = new Regex(@"\[(.*)\]\.\[(.*)\]",
            RegexOptions.Multiline
            | RegexOptions.CultureInvariant
            | RegexOptions.Compiled
            );

        public TableRow GetTableRow(string context, string group, string subGroup, string sheetName, string[] row)
        {
            var tableName = row[0];
            var schema = context == "Drl" ? "DATA" : "dbo";
            var table = parseTableName.Match(tableName).Groups[2].Value;

            return new TableRow
            {
                Context = context,
                Group = group,
                SubGroup = subGroup,
                Schema = schema,
                Table = table,
                IsRelated = row[1] == "0" ? string.Empty : (row[1] == "1" ? "x" : row[1]),
                PrimaryObject = parseTableName.Match(row[2]).Groups[2].Value,
                ViewCollections = row[7],
                DeName = row[8],
                EnName = row[9]
            };
        }

        public ColumnRow GetColumnRow(string context, string group, string subGroup, string sheetName, string[] row)
        {
            var columnName = row[0];
            var schema = context == "Drl" ? "DATA" : "dbo";
            var table = sheetName;

            return new ColumnRow
            {
                Context = context,
                Schema = schema,
                Table = table,
                Column = columnName,
                InModel = "x",
                IsModelRequired = row[3] == "0" ? string.Empty : (row[3] == "1" ? "x" : row[3]),
                DE = row[5],
                EN = row[6]
            };
        }


        public class TableRow
        {
            public string Schema { get; set; }
            public string Table { get; set; }
            public string Context { get; set; }
            public string Group { get; set; }
            public string SubGroup { get; set; }
            public string IsRelated { get; set; }
            public string PrimaryObject { get; set; }
            public string ViewCollections { get; set; }
            public string DeName { get; set; }
            public string EnName { get; set; }
        }

        public class ColumnRow
        {
            public string Schema { get; set; }
            public string Table { get; set; }
            public string Column { get; set; }
            public string Context { get; set; }
            public string InModel { get; set; }
            public string IsModelRequired { get; set; }
            public string DE { get; set; }
            public string EN { get; set; }
        }
    }
}