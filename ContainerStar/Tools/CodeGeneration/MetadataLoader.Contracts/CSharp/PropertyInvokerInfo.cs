using System.Collections.Generic;
using System.Linq;

namespace MetadataLoader.Contracts.CSharp
{
    public sealed class PropertyInvokerInfo
    {
        #region	Private fields
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public PropertyInvokerInfo(string body,
            AccessibilityLevels accessibility = AccessibilityLevels.Public)
            : this(new PropertyInvokerBody(body), accessibility)
        {
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public PropertyInvokerInfo(PropertyInvokerBody body = null,
            AccessibilityLevels accessibility = AccessibilityLevels.Public)
        {
            Body = body;
            Accessibility = accessibility;
            Attributes = new List<AttributeUsageInfo>();
        }
        #endregion
        #region	Public properties
        public AccessibilityLevels Accessibility { get; set; }
        public IReadOnlyCollection<AttributeUsageInfo> Attributes { get; private set; }
        public PropertyInvokerBody Body { get; set; }
        public bool IsDefault
        {
            get { return Body == null; }
        }
        public bool HasBody
        {
            get { return Body != null; }
        }
        #endregion
        #region	Public methods
        public IEnumerable<string> GetUsedNamespaces()
        {
            foreach (var ns in Attributes.SelectMany(info => GetUsedNamespaces()))
            {
                yield return ns;
            }
            if (HasBody)
            {
                foreach (var ns in Body.Namespaces)
                {
                    yield return ns;
                }
            }
        }
        #endregion
    }
}