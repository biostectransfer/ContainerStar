using System;
using System.Collections.Generic;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.Content
{
    public static class ContentLoaderExtensions
    {
        public static void Handle<T>(this IEnumerable<T> tables, Action<T> action = null)
        {
            if (action == null)
            {
                return;
            }
            foreach (var table in tables)
            {
                action(table);
            }
        }
    }
}