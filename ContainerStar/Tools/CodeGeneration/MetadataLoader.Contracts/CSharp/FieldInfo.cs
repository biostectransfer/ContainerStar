namespace MetadataLoader.Contracts.CSharp
{
    public sealed class FieldInfo : MemberInfo
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public FieldInfo(string name, TypeUsageInfo type)
        {
            Name = name;
            Type = type;
        }
        #endregion
        #region	Public properties
        public override AccessibilityLevels DefaultAccessibility
        {
            get { return AccessibilityLevels.Private; }
        }
        public bool IsReadOnly { get; set; }
        public TypeUsageInfo Type { get; private set; }
        #endregion
    }
}