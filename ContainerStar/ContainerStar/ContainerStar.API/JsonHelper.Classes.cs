using System.Collections;
using System.Collections.Generic;

namespace ContainerStar.API
{
    public static partial class JsonHelper
    {
        private sealed class TableMapping : MappingBase, IEnumerable<KeyValuePair<string, ColumnMapping>>
        {
            private readonly Dictionary<string, ColumnMapping> _columns;
            #region Constructor
            public TableMapping(string dbName, string codeName, int count)
                : base(dbName, codeName)
            {
                _columns = new Dictionary<string, ColumnMapping>(count);
            }
            #endregion
            #region	Public methods
            public void Add(string dbName, string codeName)
            {
                _columns.Add(dbName, new ColumnMapping(dbName, codeName));
            }

            /// <summary>
            ///     Gets the value associated with the specified key.
            /// </summary>
            /// <returns>
            ///     true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified key;
            ///     otherwise, false.
            /// </returns>
            /// <param name="key">The key of the value to get.</param>
            /// <param name="value">
            ///     When this method returns, contains the value associated with the specified key, if the key is
            ///     found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is
            ///     passed uninitialized.
            /// </param>
            /// <exception cref="T:System.ArgumentNullException"><paramref name="key" /> is null.</exception>
            public bool TryGetValue(string key, out ColumnMapping value)
            {
                return _columns.TryGetValue(key, out value);
            }

            /// <summary>
            ///     Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
            /// </returns>
            public IEnumerator<KeyValuePair<string, ColumnMapping>> GetEnumerator()
            {
                return _columns.GetEnumerator();
            }
            /// <summary>
            ///     Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
            /// </returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            #endregion
        }

        private sealed class ColumnMapping : MappingBase
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
            /// </summary>
            public ColumnMapping(string dbName, string codeName)
                : base(dbName, codeName)
            {
            }
        }

        private class MappingBase
        {
            #region Constructor
            /// <summary>
            ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
            /// </summary>
            public MappingBase(string dbName, string codeName)
            {
                DbName = dbName;
                CodeName = codeName;
            }
            #endregion
            public string CodeName { get; set; }
            public string DbName { get; set; }
        }
    }
}