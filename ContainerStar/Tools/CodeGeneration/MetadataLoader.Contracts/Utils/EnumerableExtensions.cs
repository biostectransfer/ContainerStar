using System.Collections.Generic;

namespace MetadataLoader.Contracts.Utils
{
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Wraps this object instance into an IEnumerable&lt;T&gt;
        ///     consisting of a single item.
        /// </summary>
        /// <typeparam name="T"> Type of the object. </typeparam>
        /// <param name="item"> The instance that will be wrapped. </param>
        /// <returns> An IEnumerable&lt;T&gt; consisting of a single item. </returns>
        public static IEnumerable<T> Yield<T>(this T item, bool skipNull = true)
        {
            if (skipNull && item == null)
            {
                yield break;
            }
            yield return item;
        }
    }
}