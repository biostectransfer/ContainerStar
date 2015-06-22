using System;
using System.Collections.Generic;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.Database
{
    public interface IDbObjectExtractor<TTableKey, TColumnKey, TTable, TTableContent, TColumn, TColumnContent>
        where TTable : Table<TTable, TTableContent, TColumn, TColumnContent>
        where TColumn : Column<TTable, TTableContent, TColumn, TColumnContent>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        IEnumerable<KeyValuePair<TTableKey, TTable>> ExtractTables();
        IEnumerable<KeyValuePair<Tuple<TTableKey, TColumnKey>, TColumn>> ExtractColumns();
        IEnumerable<RelationshipDesc<TTableKey, TColumnKey>> ExtractRelationships();
        IEnumerable<KeyDesc<TTableKey, TColumnKey>> ExtractKeys();
    }
}