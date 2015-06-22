namespace MetadataLoader.Contracts.CSharp
{
    public sealed class AttributeUsageInfo : UsageInfo
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public AttributeUsageInfo(string name, string initializationString, params string[] namespaces)
            : base(name, namespaces)
        {
            InitializationString = initializationString;
        }
        #endregion
        #region	Public properties
        public string InitializationString { get; set; }
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
            return string.IsNullOrEmpty(InitializationString)
                ? string.Format("[{0}]", Name)
                : string.Format("[{0}({1})]", Name, InitializationString);
        }
        #endregion
    }
}