using System.Collections.Generic;
using MetadataLoader.Contracts;

namespace MetadataLoader.EntityFramework
{
    public interface IEntityLoader<in T>
    {
        List<EntityInfo> Load(IEnumerable<T> inputTables, ILog log = null);
    }
}