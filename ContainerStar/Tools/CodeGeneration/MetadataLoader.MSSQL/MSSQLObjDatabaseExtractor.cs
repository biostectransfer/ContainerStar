using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MetadataLoader.Contracts;
using MetadataLoader.Contracts.Database;
using MetadataLoader.MSSQL.Contracts.Database;
using MetadataLoader.MSSQL.Contracts.ServerTypes;
using MetadataLoader.Contracts.Utils;
using MetadataLoader.Database;
using MetadataLoader.MSSQL.Utils;

namespace MetadataLoader.MSSQL
{
    internal sealed class MSSQLObjDatabaseExtractor<TTableContent, TColumnContent>
        : IDbObjectExtractor<int, int, MSSQLTable<TTableContent, TColumnContent>, TTableContent,
            MSSQLColumn<TTableContent, TColumnContent>, TColumnContent>
        , IAdditionalExtractor<int, MSSQLTable<TTableContent, TColumnContent>, int, MSSQLColumn<TTableContent, TColumnContent>>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        private abstract class Scripts
        {
            public const string Tables = "Tables.sql";
            public const string Columns = "Columns.sql";
            public const string Relationships = "Relationships.sql";
            public const string PrimaryKeys = "PrimaryKeys.sql";
            public const string ExtendedProperties = "ExtendedProperties.sql";
        }
        #region	Private fields
        private readonly ScriptEngine _engine;
        private readonly ResourceFileExtractor _scriptsExtractor;
        #endregion
        #region Constructor
        public MSSQLObjDatabaseExtractor(string conn)
        {
            _engine = new ScriptEngine(conn);
            _scriptsExtractor = new ResourceFileExtractor(".Scripts.");
        }
        #endregion
        #region Public methods
        public IEnumerable<KeyValuePair<int, MSSQLTable<TTableContent, TColumnContent>>> ExtractTables()
        {
            return RunQuery(Scripts.Tables, ExtractTable);
        }
        public IEnumerable<KeyValuePair<Tuple<int, int>, MSSQLColumn<TTableContent, TColumnContent>>> ExtractColumns()
        {
            return RunQuery(Scripts.Columns, ExtractColumn);
        }
        public IEnumerable<RelationshipDesc<int, int>> ExtractRelationships()
        {
            var script = _scriptsExtractor.ReadFileFromRes(Scripts.Relationships);
            var table = _engine.ExecuteQuery(script);
            //NOTE: Skip unsuported objects
            var rId = -1;
            var fromTable = -1;
            var fromColumns = new List<int>();
            var toTable = -1;
            var toColumns = new List<int>();

            foreach (var row in table.Rows.Cast<DataRow>())
            {
                var id = (int) row["id"];
                var fromTableId = (int) row["from_table_id"];
                var fromColumnId = (int) row["from_column_id"];
                var toTableId = (int) row["to_table_id"];
                var toColumnId = (int) row["to_column_id"];

                if (id != rId)
                {
                    if (rId != -1)
                    {
                        yield return new RelationshipDesc<int, int>(fromTable, fromColumns.ToArray(), toTable, toColumns.ToArray());
                    }
                    rId = id;
                    fromTable = fromTableId;
                    toTable = toTableId;
                    fromColumns.Clear();
                    toColumns.Clear();
                }
                fromColumns.Add(fromColumnId);
                toColumns.Add(toColumnId);
            }
            if (rId != -1)
            {
                yield return new RelationshipDesc<int, int>(fromTable, fromColumns.ToArray(), toTable, toColumns.ToArray());
            }
        }
        public IEnumerable<KeyDesc<int, int>> ExtractKeys()
        {
            var script = _scriptsExtractor.ReadFileFromRes(Scripts.PrimaryKeys);
            var table = _engine.ExecuteQuery(script);
            //NOTE: Skip unsuported objects
            var tableId = -1;
            var columns = new List<int>();

            foreach (var row in table.Rows.Cast<DataRow>())
            {
                var id = (int)row["parent_id"];
                var columnId = (int)row["column_id"];

                if (id != tableId)
                {
                    if (tableId != -1)
                    {
                        yield return new KeyDesc<int, int>(tableId, columns.ToArray());
                    }
                    tableId = id;
                    columns.Clear();
                }
                columns.Add(columnId);
            }
            if (tableId != -1)
            {
                yield return new KeyDesc<int, int>(tableId, columns.ToArray());
            }
        }
        #endregion
        #region	Private methods
        private IEnumerable<T> RunQuery<T>(string queryName, Func<DataRow, T> extractor)
        {
            var script = _scriptsExtractor.ReadFileFromRes(queryName);
            var table = _engine.ExecuteQuery(script);
            //NOTE: Skip unsuported objects
            return table.Rows.Cast<DataRow>().Select(extractor).Where(o => !Equals(o, default(T)));
        }
        private static KeyValuePair<int, MSSQLTable<TTableContent, TColumnContent>> ExtractTable(DataRow row)
        {
            var key = (int) row["id"];

            var table = new MSSQLTable<TTableContent, TColumnContent>
            {
                Name = row["name"].ToString(),
                Schema = row["schema"].ToString(),
                IsView = (bool) row["is_view"]
            };

            return new KeyValuePair<int, MSSQLTable<TTableContent, TColumnContent>>(key, table);
        }
        private static KeyValuePair<Tuple<int, int>, MSSQLColumn<TTableContent, TColumnContent>> ExtractColumn(DataRow row)
        {
            var key = new Tuple<int, int>((int) row["parent_id"], (int) row["id"]);

            var column = new MSSQLColumn<TTableContent, TColumnContent>();

            column.Name = row["name"].ToString();
            column.ColumnType = (ColumnTypes) Enum.ToObject(typeof (ColumnTypes), row["type"]);
            column.IsRequired = !(bool) row["is_nullable"];
            column.Default = row["default"].ToString();
            column.IsRowGuid = (bool) row["is_rowguidcol"];

            if (column.ColumnType == ColumnTypes.IsComputed)
            {
                column.ComputedBody = row["computed_body"].ToString();
            }

            var typeId = (int) row["type_id"];
            var systemTypeId = (NativeTypes) Enum.ToObject(typeof (NativeTypes), row["system_type_id"]);

            //NOTE: System types from 1 to 255
            column.NativeType = typeId < 256 ? (NativeTypes) Enum.ToObject(typeof (NativeTypes), typeId) : systemTypeId;

            var desc = MSSQLTypeDescriptions.Dictionary.GetDesc(column.NativeType);
            if (desc.IsDerived)
            {
                desc = MSSQLTypeDescriptions.Dictionary.GetDesc(systemTypeId);
            }
            #region Skip unsuported types
            switch (desc.BaseType)
            {
                case NativeTypes.Clr:
                case NativeTypes.Table:
                    return default(KeyValuePair<Tuple<int, int>, MSSQLColumn<TTableContent, TColumnContent>>);
            }
            #endregion
            #region Length
            if (desc.HasLength)
            {
                var length = (short) row["length"];
                if (desc.BaseType == NativeTypes.NVarchar ||
                    desc.BaseType == NativeTypes.NChar)
                {
                    length /= 2;
                }
                column.Length = length;
            }
            #endregion
            #region Precision
            if (desc.HasPrecision)
            {
                column.Precision = (byte) row["precision"];
            }
            #endregion
            #region Scale
            if (desc.HasScale)
            {
                column.Scale = (byte)row["scale"];
            }
            #endregion
            #region Collation
            if (desc.HasCollation)
            {
                column.Collation = row["collation"].ToString();
            }
            #endregion
            return new KeyValuePair<Tuple<int, int>, MSSQLColumn<TTableContent, TColumnContent>>(key, column);
        }
        #endregion
        #region Additional loading(Extended properties)
        void IAdditionalExtractor<int, MSSQLTable<TTableContent, TColumnContent>, int, MSSQLColumn<TTableContent, TColumnContent>>.Run(ILog log, Func<int, MSSQLTable<TTableContent, TColumnContent>> getTable, Func<int, int, MSSQLColumn<TTableContent, TColumnContent>> getColumn)
        {
            #region Extended properties
            var script = _scriptsExtractor.ReadFileFromRes(Scripts.ExtendedProperties);
            var tableResult = _engine.ExecuteQuery(script);

            foreach (var row in tableResult.Rows.Cast<DataRow>())
            {
                var id = (int) row["major_id"];
                var columnId = (int) row["minor_id"];

                var name = row["name"].ToString();
                var value = row["value"].ToString();

                var extendedProperty = new MSSQLExtendedProperty(name, value);
                
                var table = getTable(id);
                IExtendedPropertyHolder holder = table;
                if (columnId > 0)
                {
                    holder = getColumn(id, columnId);
                }
                holder.ExtendedProperties.Add(extendedProperty);
                if (extendedProperty.Name == "MS_Description")
                {
                    holder.Description = extendedProperty.Value;
                }
            }
            #endregion
        }
        #endregion
        
    }
}