using System;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.EntityFramework.Extractors
{
    public class EntityExtractor<T, TContent> : IEntityExtractor<T>
        where T : IBaseObject<TContent>
        where TContent : IContent
    {
        public virtual string GetName(T obj, Func<string, bool> checkDuplicates)
        {
            return obj.Content.CodeName;
        }
    }
}