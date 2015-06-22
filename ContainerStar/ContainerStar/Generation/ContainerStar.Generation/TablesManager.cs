using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using MetadataLoader.Content;
using MetadataLoader.Contracts.Utils;
using MetadataLoader.Database;
using MetadataLoader.Excel;
using MetadataLoader.MSSQL;
using MetadataLoader.MSSQL.Contracts.Database;

namespace ContainerStar.Generation
{
    /// <summary>
    /// <see cref=""/>
    /// </summary>
    public sealed class TablesManager
    {
        #region Static
        public static TablesManager LoadFromDatabase(string conn, string tableContext, string tableGroup = null, string tableSubGroup = null)
        {
            var loader = MSSQLLoadManager<TableContent, ColumnContent>.Instance.GetDatabaseLoader(conn);
            return new TablesManager(loader, tableContext, tableGroup, tableSubGroup);
        }
        public static TablesManager LoadFromDacPac(string filePath, string tableContext, string tableGroup = null, string tableSubGroup = null)
        {
            var loader = MSSQLLoadManager<TableContent, ColumnContent>.Instance.GetDacPacLoader(filePath);
            return new TablesManager(loader, tableContext, tableGroup, tableSubGroup);
        }

        private static readonly Regex NameRegEx = new Regex(@"\A((DRL)_)(?<NAME>(\w|_)+?)(_(RSP))?$",
            RegexOptions.RightToLeft
            | RegexOptions.CultureInvariant
            | RegexOptions.Compiled
            );

        private static string TransformName(string name)
        {
            //NOTE: exclude crossing names 
            switch (name)
            {
                case "DRL_AUTHORITY_RETURN_TYPE_RSP": //crossing with DRL_AUTHORITY_RETURN_TYPE entity
                    return "AUTHORITY_RETURN_TYPE_RSP";
            }
            return NameRegEx.Replace(name, "${NAME}");
        }
        #endregion
        #region	Private fields
        private readonly ITableLoader<MSSQLTable<TableContent, ColumnContent>> _loader;
        private readonly string _tableContext;
        private readonly string _tableGroup;
        private readonly string _tableSubGroup;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        private TablesManager(ITableLoader<MSSQLTable<TableContent, ColumnContent>> loader, string tableContext, string tableGroup = null, string tableSubGroup = null)
        {
            _loader = loader;
            _tableContext = tableContext;
            _tableGroup = tableGroup;
            _tableSubGroup = tableSubGroup;
        }
        #endregion
        #region	Public methods
        public List<MSSQLTable<TableContent, ColumnContent>> Load(params string[] contentFiles)
        {
            Contract.Requires(contentFiles != null);
            Contract.Requires(contentFiles.Length != 0);

            List<MSSQLTable<TableContent, ColumnContent>> tables;
            #region Load
            {
                var rows = DataSheetManager.Read(contentFiles[0], TableDescriptor.GetRead("Tables{T}", 5)).Tables[0].Rows.Cast<DataRow>();

                rows = rows.AddFilter(_tableContext, 2).AddFilter(_tableGroup, 3).AddFilter(_tableSubGroup, 4);
                var tableNames = rows.Select(row => GetFullTableName(row[0].ToString(), row[1].ToString())).ToArray();
                tables = _loader.Load(tableFilter: table => /*!table.IsView &&*/ tableNames.Contains(GetFullTableName(table.Schema, table.Name)));
            }
            #endregion
            #region Prepare content
            tables.Handle(PrepareEntityName);
            tables.SelectMany(table => table.Columns).Handle(PrepareEntityPropertyName);
            #endregion
            #region Content load
            var contentLoader = MSSQLLoadManager<TableContent, ColumnContent>.Instance.GetContentLoader(tables);
            foreach (var contentFile in contentFiles)
            {
                contentLoader.Load(contentFile);
            }
            #endregion
            return tables;
        }
        private static string GetFullTableName(string schema, string name)
        {
            return (schema + "." + name).ToLower();
        }
        #endregion
        #region	Private methods
        private void PrepareEntityName(MSSQLTable<TableContent, ColumnContent> obj)
        {
            obj.Content.CodeName = TransformName(obj.Name).ToPascalCase();
            obj.Content.CodeNamePlural = Pluralize(obj.Content.CodeName);
        }
        private void PrepareEntityPropertyName(MSSQLColumn<TableContent, ColumnContent> obj)
        {
            obj.Content.CodeName = TransformName(obj.Name).ToPascalCase();
            obj.Content.CodeNamePlural = Pluralize(obj.Content.CodeName);
        }

        private static string Singularize(string value, bool inputIsKnownToBePlural = true)
        {
            return Handle(value, s => s.Singularize(inputIsKnownToBePlural));
        }
        private static string Pluralize(string value, bool inputIsKnownToBeSingular = true)
        {
            return Handle(value, s => s.Pluralize(inputIsKnownToBeSingular));
        }

        private static string Handle(string value, Func<string, string> handle)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            int i;
            for (i = value.Length - 1; i >= 0; i--)
            {
                if (char.IsUpper(value, i))
                {
                    break;
                }
            }
            var lastWord = value.Substring(i);
            var lastWordPlural = handle(lastWord);

            return value.Substring(0, i) + lastWordPlural;
        }
        #endregion
    }

    internal static class TablesExtensions
    {
        public static IEnumerable<DataRow> AddFilter(this IEnumerable<DataRow> rows, string filter, int colIndex)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                rows = rows.Where(row => string.Equals(row[colIndex].ToString(), filter, StringComparison.InvariantCultureIgnoreCase));
            }
            return rows;
        }
    }
}