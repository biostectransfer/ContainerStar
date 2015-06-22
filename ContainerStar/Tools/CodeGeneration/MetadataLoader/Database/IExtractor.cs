using System.Collections.Generic;

namespace MetadataLoader.Database
{
    public interface IExtractor<TKey, T>
    {
        IEnumerable<KeyValuePair<TKey, T>> Extract();
    }
}