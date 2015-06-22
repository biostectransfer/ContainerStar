using System;
using System.Collections.Generic;
using System.IO;
using MetadataLoader.Contracts;
using MetadataLoader.Contracts.Database;
using MetadataLoader.MSSQL.Contracts.Database;
using MetadataLoader.Database;

namespace MetadataLoader.MSSQL
{
    internal sealed class MSSQLObjDacPacExtractor<TTableContent, TColumnContent>
        : IDbObjectExtractor<int, int, MSSQLTable<TTableContent, TColumnContent>, TTableContent,
            MSSQLColumn<TTableContent, TColumnContent>, TColumnContent>
            , IAdditionalExtractor<int, MSSQLTable<TTableContent, TColumnContent>, int, MSSQLColumn<TTableContent, TColumnContent>>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        #region	Private fields
        private FileInfo _file;
        #endregion
        #region Constructor
        public MSSQLObjDacPacExtractor(string file)
        {
            _file = new FileInfo(file);
        }
        #endregion
        #region Public methods
        public IEnumerable<KeyValuePair<int, MSSQLTable<TTableContent, TColumnContent>>> ExtractTables()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<KeyValuePair<Tuple<int, int>, MSSQLColumn<TTableContent, TColumnContent>>> ExtractColumns()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<RelationshipDesc<int, int>> ExtractRelationships()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<KeyDesc<int, int>> ExtractKeys()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Additional loading(Extended properties)
        void IAdditionalExtractor<int, MSSQLTable<TTableContent, TColumnContent>, int, MSSQLColumn<TTableContent, TColumnContent>>.Run(ILog log,
            Func<int, MSSQLTable<TTableContent, TColumnContent>> getTable, Func<int, int, MSSQLColumn<TTableContent, TColumnContent>> getColumn)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}