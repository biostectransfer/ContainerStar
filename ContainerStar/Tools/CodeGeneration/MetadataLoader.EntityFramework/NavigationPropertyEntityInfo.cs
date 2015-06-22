using System.Collections.Generic;
using MetadataLoader.Contracts.CSharp;

namespace MetadataLoader.EntityFramework
{
    public sealed class NavigationPropertyEntityInfo : PropertyInfo
    {
        #region Static
        public static NavigationPropertyEntityInfo One(string name, EntityInfo otherSideEntity)
        {
            var type = otherSideEntity.GetTypeUsage();
            return new NavigationPropertyEntityInfo(name, type, null);
        }

        public static NavigationPropertyEntityInfo Many(string name, EntityInfo otherSideEntity)
        {
            var itemType = otherSideEntity.GetTypeUsage();
            var type = typeof (ICollection<>).ToGenericUsageInfo(itemType);
            var initializationType = typeof (HashSet<>).ToGenericUsageInfo(itemType);

            return new NavigationPropertyEntityInfo(name, type, initializationType);
        }
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        private NavigationPropertyEntityInfo(string name, TypeUsageInfo type, TypeUsageInfo initializeType)
            : base(name, type, false)
        {
            IsVirtual = true;
            InitializeType = initializeType;
        }
        #endregion
        #region	Public properties
        public TypeUsageInfo InitializeType { get; private set; }
        public bool HasInitialization
        {
            get { return InitializeType != null; }
        }
        #endregion
    }
}