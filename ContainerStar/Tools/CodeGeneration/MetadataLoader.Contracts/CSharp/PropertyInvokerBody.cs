using System.Diagnostics.Contracts;

namespace MetadataLoader.Contracts.CSharp
{
    public sealed class PropertyInvokerBody
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public PropertyInvokerBody(string text, params string[] namespaces)
        {
            Contract.Requires(!string.IsNullOrEmpty(text));

            Text = text;
            Namespaces = namespaces;
        }
        #endregion
        #region	Public properties
        public string[] Namespaces { get; private set; }
        public string Text { get; private set; }
        #endregion
    }
}