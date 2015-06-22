using System.Collections.Generic;
using System.Linq;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.EntityFramework.Extractors
{
    public static class RelationshipExtensions
    {
        public static SimplePropertyEntityInfo[] GetKeyProperties<TColumn>(this EntityInfo entity, IEnumerable<TColumn> key)
            where TColumn : IColumn
        {
            return key.Select(c => entity.SimpleProperties.First(p => p.ColumnName == c.Name)).ToArray();
        }
    }
}