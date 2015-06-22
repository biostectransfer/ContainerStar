using System.Collections.Generic;
using System.Diagnostics;
using MetadataLoader.Contracts.CSharp;
using MetadataLoader.Contracts.Database;
using MetadataLoader.MSSQL.Contracts.ServerTypes;

namespace MetadataLoader.MSSQL.Contracts.Database
{
    public sealed class MSSQLColumn<TTableContent, TColumnContent>
        : Column<MSSQLTable<TTableContent, TColumnContent>, TTableContent, MSSQLColumn<TTableContent, TColumnContent>, TColumnContent>
            , IExtendedPropertyHolder
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        #region	Private fields
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<MSSQLExtendedProperty> _extendedProperties;
        #endregion
        #region Constructor
        public MSSQLColumn()
        {
            _extendedProperties = new List<MSSQLExtendedProperty>();
        }
        #endregion
        #region	Public properties
        public override TypeUsageInfo TypeInfo
        {
            get { return TypeDescription.TypeInfo; }
        }
        public ColumnTypes ColumnType { get; set; }
        public int Length { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public string Collation { get; set; }
        public string Default { get; set; }
        public bool IsRowGuid { get; set; }
        public string ComputedBody { get; set; }
        public NativeTypes NativeType { get; set; }
        public MSSQLTypeDesc TypeDescription
        {
            get { return MSSQLTypeDescriptions.Dictionary.GetDesc(NativeType); }
        }
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
            return string.Format("{0}, NativeType: {1}", base.ToString(), NativeType);
        }
        #endregion
    }
}