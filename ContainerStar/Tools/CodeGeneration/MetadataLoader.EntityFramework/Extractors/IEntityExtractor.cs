using System;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.EntityFramework.Extractors
{
    public interface IEntityExtractor<in T>
        where T : IBaseObject
    {
        string GetName(T obj, Func<string, bool> checkDuplicates);
    }
}