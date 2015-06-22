using System;

namespace MetadataLoader.Contracts.CSharp
{
    public class InterfaceInfo : AdvTypeInfo
    {
        #region Constructor
        public InterfaceInfo(params TypeUsageInfo[] typeArguments)
            : base(typeArguments)
        {
        }
        #endregion
        #region	Public properties
        public override AccessibilityLevels DefaultAccessibility
        {
            get { return AccessibilityLevels.None; }
        }
        internal override TypeUsageInfoConfiguration TypeConfiguration
        {
            get { return TypeUsageInfoConfiguration.Interface; }
        }
        protected override void CheckInherits(TypeUsageInfo info)
        {
            if (info.IsClass)
            {
                throw new ArgumentOutOfRangeException("info", "Trying inherits from class");
            }
        }
        #endregion

        
    }
}