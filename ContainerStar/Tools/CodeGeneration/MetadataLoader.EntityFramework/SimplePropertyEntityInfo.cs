using MetadataLoader.Contracts.CSharp;

namespace MetadataLoader.EntityFramework
{
    public sealed class SimplePropertyEntityInfo : PropertyInfo
    {
        #region Constructor
        public SimplePropertyEntityInfo(string name, TypeUsageInfo type)
            : base(name, type)
        {
            IncludeToShallowCopy = true;
        }
        #endregion
        #region	Public properties
        /// <summary>
        ///     Is key column
        /// </summary>
        public bool IsKey { get; set; }
        /// <summary>
        ///     Column's order in key
        /// </summary>
        public int KeyOrder { get; set; }
        public string ColumnName { get; set; }
        /// <summary>
        ///     Is required field in db
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        ///     Is unicode
        /// </summary>
        public bool IsUnicode { get; set; }
        /// <summary>
        ///     Is row guid
        /// </summary>
        public bool IsRowGuid { get; set; }
        public int? Length { get; set; }
        public bool IsMaxLength
        {
            get { return Length.HasValue && Length.Value == -1; }
        }
        public bool IsFixedLength { get; set; }
        public bool HasDefaultValue
        {
            get { return !string.IsNullOrWhiteSpace(DefaultValue); }
        }
        public string DefaultValue { get; set; }
        public DatabaseGeneratedOption DatabaseGeneratedOption { get; set; }
        public bool IncludeToShallowCopy { get; set; }
        public bool IsConcurrencyToken { get; set; }
        #endregion
    }
}