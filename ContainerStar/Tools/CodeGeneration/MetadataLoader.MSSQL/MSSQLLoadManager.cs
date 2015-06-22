using System.Collections.Generic;
using MetadataLoader.Content;
using MetadataLoader.Contracts.Database;
using MetadataLoader.MSSQL.Contracts.Database;
using MetadataLoader.Database;

namespace MetadataLoader.MSSQL
{
    public class MSSQLLoadManager<TTableContent, TColumnContent>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        public static readonly MSSQLLoadManager<TTableContent, TColumnContent> Instance = new MSSQLLoadManager<TTableContent, TColumnContent>();

        public ITableLoader<MSSQLTable<TTableContent, TColumnContent>> GetDatabaseLoader(string conn)
        {
            var extractor = new MSSQLObjDatabaseExtractor<TTableContent, TColumnContent>(conn);
            return
                new TableLoader<int, int, MSSQLTable<TTableContent, TColumnContent>, TTableContent, MSSQLColumn<TTableContent, TColumnContent>, TColumnContent>
                    (extractor, extractor);
        }

        public ITableLoader<MSSQLTable<TTableContent, TColumnContent>> GetDacPacLoader(string filePath)
        {
            var extractor = new MSSQLObjDacPacExtractor<TTableContent, TColumnContent>(filePath);
            return
                new TableLoader<int, int, MSSQLTable<TTableContent, TColumnContent>, TTableContent, MSSQLColumn<TTableContent, TColumnContent>, TColumnContent>
                    (extractor, extractor);
        }

        public IExcelContentLoader GetContentLoader(List<MSSQLTable<TTableContent, TColumnContent>> tables)
        {
            return new ExcelContentLoader<MSSQLTable<TTableContent, TColumnContent>, TTableContent, MSSQLColumn<TTableContent, TColumnContent>, TColumnContent>(tables);
        }
    }
}