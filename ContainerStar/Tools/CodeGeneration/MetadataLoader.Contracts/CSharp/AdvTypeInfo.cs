using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace MetadataLoader.Contracts.CSharp
{
    public abstract class AdvTypeInfo : TypeInfo
    {
        #region	Private fields
        private readonly TypeUsageInfo[] _typeArguments;
        private readonly List<PropertyInfo> _properties;
        private readonly List<TypeUsageInfo> _inherits;
        #endregion
        #region Constructor
        protected AdvTypeInfo(params TypeUsageInfo[] typeArguments)
        {
            Contract.Requires(typeArguments != null && (typeArguments.Length == 0 || typeArguments.All(t => t.IsTypeArgument)));

            _typeArguments = typeArguments;
            _properties = new List<PropertyInfo>();
            _inherits = new List<TypeUsageInfo>();
        }
        #endregion
        #region	Public properties
        public bool IsPartial { get; set; }
        public bool IsGeneric
        {
            get { return TypeArguments.Count != 0; }
        }
        public IReadOnlyCollection<TypeUsageInfo> TypeArguments
        {
            get { return _typeArguments; }
        }
        public IReadOnlyCollection<TypeUsageInfo> Inherits
        {
            get { return _inherits; }
        }
        public virtual IEnumerable<PropertyInfo> Properties
        {
            get { return _properties; }
        }
        #endregion
        #region Public methods
        public virtual void AddProperty(PropertyInfo property)
        {
            if (!property.IsExplicit && Properties.Where(p => !p.IsExplicit).Any(p => p.Name == property.Name))
            {
                throw new ArgumentOutOfRangeException("property", "Property with same name is already exists");
            }
            _properties.Add(property);
        }
        public virtual void InheritsFrom(TypeUsageInfo info)
        {
            if (Inherits.Any(t => t.Equals(info)))
            {
                throw new ArgumentOutOfRangeException("info", string.Format("Type already inherits from this type {0}", info));
            }
            if (IsGeneric && info.IsGeneric)
            {
                if (info.TypeArguments.Where(t => t.IsTypeArgument).Any(t => !TypeArguments.Contains(t)))
                {
                    throw new ArgumentOutOfRangeException("info", string.Format("Trying inherit from type {0} with unknown type argument", info));
                }
            }
            CheckInherits(info);
            _inherits.Add(info);
        }
        public override IEnumerable<string> GetUsedNamespaces(bool includeSelf = false)
        {
            return base.GetUsedNamespaces(includeSelf)
                .Union(Properties.SelectMany(p => p.GetUsedNamespaces()))
                .Union(Inherits.SelectMany(t => t.GetUsedNamespaces(true)))
                .Union(TypeArguments.SelectMany(t => t.GetUsedNamespaces(true)));
        }
        protected abstract void CheckInherits(TypeUsageInfo info);
        #endregion
    }
}