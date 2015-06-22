using System;
using MetadataLoader.Contracts;

namespace MetadataLoader.Database
{
    public interface IAdditionalExtractor<out TTableKey, in TTable, out TColumnKey, in TColumn>
    {
        void Run(ILog log, Func<TTableKey, TTable> getTable, Func<TTableKey, TColumnKey, TColumn> getColumn);
    }
}