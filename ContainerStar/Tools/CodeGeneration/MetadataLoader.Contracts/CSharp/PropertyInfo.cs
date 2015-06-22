using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace MetadataLoader.Contracts.CSharp
{
    public class PropertyInfo : MemberInfo, IClassMember
    {
        private TypeUsageInfo _type;
        #region	Private fields
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public PropertyInfo(string name, TypeUsageInfo type, PropertyInvokerInfo getter, PropertyInvokerInfo setter = null)
        {
            Contract.Requires(type != null);
            Contract.Requires(getter != null || setter != null);

            Name = name;
            Type = type;
            Getter = getter;
            Setter = setter;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public PropertyInfo(string name, TypeUsageInfo type, bool isReadOnly = false)
            : this(name, type, new PropertyInvokerInfo(), new PropertyInvokerInfo {Accessibility = isReadOnly ? AccessibilityLevels.Private : AccessibilityLevels.Public})
        {
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public PropertyInfo(string name, FieldInfo backingField, PropertyInvokerInfo getter, PropertyInvokerInfo setter = null)
            : this(name, backingField.Type, getter, setter)
        {
            BackingField = backingField;
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public PropertyInfo(string name, FieldInfo backingField, bool isReadOnly = false)
            : this(name, backingField, new PropertyInvokerInfo(), isReadOnly ? null : new PropertyInvokerInfo())
        {
            Accessibility = AccessibilityLevels.Public;
        }
        #endregion
        #region	Public properties
        public override sealed string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        public bool IsAbstract { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsOverride { get; set; }
        public bool IsExplicit
        {
            get { return ExplicitInterface != null; }
        }
        public TypeUsageInfo ExplicitInterface { get; set; }
        public TypeUsageInfo Type
        {
            get { return _type; }
            set
            {
                if (BackingField != null)
                {
                    throw new NotSupportedException("Can't change type to property with backing field");
                }
                _type = value;
            }
        }
        public FieldInfo BackingField { get; private set; }
        public bool IsAutoProperty
        {
            get
            {
                return BackingField == null
                       && HasGetter && Getter.IsDefault
                       && HasSetter && Setter.IsDefault;
            }
        }
        public override AccessibilityLevels DefaultAccessibility
        {
            get { return AccessibilityLevels.Public; }
        }
        public PropertyInvokerInfo Getter { get; private set; }
        public bool HasGetter
        {
            get { return Getter != null; }
        }
        public PropertyInvokerInfo Setter { get; private set; }
        public bool HasSetter
        {
            get { return Setter != null; }
        }
        public bool HasBackingField
        {
            get { return BackingField != null; }
        }
        #endregion
        #region	Public methods
        public override IEnumerable<string> GetUsedNamespaces(bool includeSelf = false)
        {
            return base.GetUsedNamespaces(includeSelf).Union(GetPropertyNamespaces());
        }
        private IEnumerable<string> GetPropertyNamespaces()
        {
            foreach (var ns in Type.GetUsedNamespaces())
            {
                yield return ns;
            }
            if (BackingField != null)
            {
                foreach (var ns in BackingField.GetUsedNamespaces())
                {
                    yield return ns;
                }
            }
            if (HasGetter)
            {
                foreach (var ns in Getter.GetUsedNamespaces())
                {
                    yield return ns;
                }
            }
            if (HasSetter)
            {
                foreach (var ns in Setter.GetUsedNamespaces())
                {
                    yield return ns;
                }
            }
            if (ExplicitInterface != null)
            {
                foreach (var ns in ExplicitInterface.GetUsedNamespaces(true))
                {
                    yield return ns;
                }
            }
        }
        #endregion
    }
}