using System.Collections.Generic;
using System.Diagnostics;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.MSSQL.Contracts.Database
{
    public sealed class MSSQLTable<TTableContent, TColumnContent> :
        Table<MSSQLTable<TTableContent, TColumnContent>, TTableContent, MSSQLColumn<TTableContent, TColumnContent>, TColumnContent>
        , IExtendedPropertyHolder
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        #region	Private fields
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<MSSQLExtendedProperty> _extendedProperties;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public MSSQLTable()
        {
            _extendedProperties = new List<MSSQLExtendedProperty>();
        }
        #endregion
        #region	Public properties
        public bool IsView { get; set; }

        public List<MSSQLExtendedProperty> ExtendedProperties
        {
            get { return _extendedProperties; }
        }
        #endregion
        #region	Public methods
        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{2} {0}.{1}", Schema, Name, IsView ? "view" : "table");
        }
        #endregion
    }
}