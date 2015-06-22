using System;
using System.Collections.Generic;
using MetadataLoader.Contracts;

namespace MetadataLoader.Database
{
    public interface ITableLoader<TTable>
    {
        List<TTable> Load(ILog log = null, Func<TTable, bool> tableFilter = null, TableLoadOptions options = TableLoadOptions.None);
    }
}