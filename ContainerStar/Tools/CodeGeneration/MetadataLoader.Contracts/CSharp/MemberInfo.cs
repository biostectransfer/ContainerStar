using System.Collections.Generic;
using System.Linq;
using MetadataLoader.Contracts.Utils;

namespace MetadataLoader.Contracts.CSharp
{
    public abstract class MemberInfo : Info
    {
        #region	Private fields
        private readonly List<AttributeUsageInfo> _attributes;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        protected MemberInfo()
        {
            _attributes = new List<AttributeUsageInfo>();
        }
        #endregion
        #region	Public properties
        public AccessibilityLevels Accessibility { get; set; }
        public abstract AccessibilityLevels DefaultAccessibility { get; }
        public string Description { get; set; }
        public List<AttributeUsageInfo> Attributes
        {
            get { return _attributes; }
        }
        public virtual string Namespace { get; set; }
        #endregion
        #region	Public methods
        public override IEnumerable<string> GetUsedNamespaces(bool includeSelf = false)
        {
            return Namespace.Yield().Union(Attributes.SelectMany(attribute => attribute.GetUsedNamespaces()));
        }
        
        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", Accessibility, Name);
        }
        #endregion
    }
}