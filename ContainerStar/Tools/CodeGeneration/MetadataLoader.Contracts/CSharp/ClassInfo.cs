using System;
using System.Collections.Generic;
using System.Linq;

namespace MetadataLoader.Contracts.CSharp
{
    public class ClassInfo : AdvTypeInfo
    {
        #region	Private fields
        private readonly List<FieldInfo> _fields;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public ClassInfo()
        {
            _fields = new List<FieldInfo>();
        }
        #endregion
        #region	Public properties
        public override AccessibilityLevels DefaultAccessibility
        {
            get { return AccessibilityLevels.None; }
        }
        public bool IsStatic { get; set; }
        public bool IsSealed { get; set; }
        public bool IsAbstract { get; set; }
        #endregion
        #region	Internal properties
        internal override TypeUsageInfoConfiguration TypeConfiguration
        {
            get { return TypeUsageInfoConfiguration.Class; }
        }
        #endregion
        #region	Public methods
        public IEnumerable<FieldInfo> GetFields()
        {
            return _fields.Union(Properties.Where(info => info.HasBackingField).Select(info => info.BackingField));
        }
        public override IEnumerable<string> GetUsedNamespaces(bool includeSelf = false)
        {
            return base.GetUsedNamespaces(includeSelf).Union(GetFields().SelectMany(f => f.GetUsedNamespaces(true)));
        }

        public virtual void AddField(FieldInfo field)
        {
            if (GetFields().Any(f => f.Name == field.Name))
            {
                throw new ArgumentOutOfRangeException("field", "Field with same name is already exists");
            }
            _fields.Add(field);
        }

        public override void AddProperty(PropertyInfo property)
        {
            if (property.HasBackingField && GetFields().Any(f => f.Name == property.BackingField.Name))
            {
                throw new ArgumentOutOfRangeException("property", "Field with same name is already exists");
            }
            base.AddProperty(property);
        }
        #endregion
        protected override void CheckInherits(TypeUsageInfo info)
        {
            if (info.IsClass && Inherits.Any(t => t.IsClass))
            {
                throw new ArgumentOutOfRangeException("info", "Trying inherits from multiply classes");
            }
        }
    }
}