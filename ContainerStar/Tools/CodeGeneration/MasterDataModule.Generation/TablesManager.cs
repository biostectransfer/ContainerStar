using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using MetadataLoader.Content;
using MetadataLoader.MSSQL.Contracts.Database;
using MetadataLoader.Contracts.Utils;
using MetadataLoader.Database;
using MetadataLoader.Excel;
using MetadataLoader.MSSQL;

namespace MasterDataModule.Generation
{
    public sealed class TablesManager
    {
        #region Static
        public static TablesManager LoadFromDatabase(string conn, string tableGroupFilter = null)
        {
            var loader = MSSQLLoadManager<TableContent, ColumnContent>.Instance.GetDatabaseLoader(conn);
            return new TablesManager(loader, tableGroupFilter);
        }
        public static TablesManager LoadFromDacPac(string filePath, string tableGroupFilter = null)
        {
            var loader = MSSQLLoadManager<TableContent, ColumnContent>.Instance.GetDacPacLoader(filePath);
            return new TablesManager(loader, tableGroupFilter);
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
        private readonly string _tableGroupFilter;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        private TablesManager(ITableLoader<MSSQLTable<TableContent, ColumnContent>> loader, string tableGroupFilter)
        {
            _loader = loader;
            _tableGroupFilter = tableGroupFilter;
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
                var rows = DataSheetManager.Read(contentFiles[0], TableDescriptor.GetRead("Tables{T}", 3)).Tables[0].Rows.Cast<DataRow>();
                if (!string.IsNullOrEmpty(_tableGroupFilter))
                {
                    rows = rows.Where(row => string.Equals(row[2].ToString(), _tableGroupFilter, StringComparison.InvariantCultureIgnoreCase));
                }
                var tableNames = rows.Select(row => row[1].ToString()).ToArray();
                tables = _loader.Load(tableFilter: table => !table.IsView && table.Schema == "DATA" && tableNames.Contains(table.Name));
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
        private static string Pluralize(string value)
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
            var lastWordPlural = lastWord.Pluralize();

            return value.Substring(0, i) + lastWordPlural;
        }
        #endregion
    }
}