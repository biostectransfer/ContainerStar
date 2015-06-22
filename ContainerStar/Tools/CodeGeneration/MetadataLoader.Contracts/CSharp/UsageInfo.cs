using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace MetadataLoader.Contracts.CSharp
{
    public abstract class UsageInfo : Info
    {
        #region	Private fields
        private readonly string[] _namespaces;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        protected UsageInfo(string name, params string[] namespaces)
        {
            Name = name;
            _namespaces = namespaces;
        }
        #endregion
        #region	Public properties
        public override sealed string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        #endregion
        #region	Public methods
        public override IEnumerable<string> GetUsedNamespaces(bool includeSelf = false)
        {
            return _namespaces;
        }
        #endregion
    }
}