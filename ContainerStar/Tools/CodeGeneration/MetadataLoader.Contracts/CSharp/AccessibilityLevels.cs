namespace MetadataLoader.Contracts.CSharp
{
    public enum AccessibilityLevels
    {
        None = 0,
        /// <summary>
        ///     Access is not restricted.
        /// </summary>
        Public = 1,

        /// <summary>
        ///     Access is limited to the containing class or types derived from the containing class.
        /// </summary>
        Protected = 2,

        /// <summary>
        ///     Access is limited to the current assembly.
        /// </summary>
        Internal = 3,

        /// <summary>
        ///     Access is limited to the current assembly or types derived from the containing class.
        /// </summary>
        ProtectedInternal = 4,

        /// <summary>
        ///     Access is limited to the containing type.
        /// </summary>
        Private = 5
    }
}