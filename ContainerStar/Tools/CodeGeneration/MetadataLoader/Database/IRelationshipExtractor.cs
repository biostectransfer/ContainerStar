using System.Collections.Generic;

namespace MetadataLoader.Database
{
    public interface IRelationshipExtractor<TTableKey, TColumnKey>
    {
        IEnumerable<RelationshipDesc<TTableKey, TColumnKey>> Extract();
    }
}