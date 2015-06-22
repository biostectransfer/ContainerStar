namespace MetadataLoader.MSSQL.Contracts.Database
{
    public sealed class MSSQLExtendedProperty
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public MSSQLExtendedProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }
        #endregion
        #region	Public properties
        public string Name { get; set; }
        public string Value { get; set; }
        #endregion
        #region	Public methods
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}, Value: {1}", Name, Value);
        }
        #endregion
        
    }
}