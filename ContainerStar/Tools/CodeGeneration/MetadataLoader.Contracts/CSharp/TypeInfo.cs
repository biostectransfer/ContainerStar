namespace MetadataLoader.Contracts.CSharp
{
    public abstract class TypeInfo : MemberInfo
    {
        #region	Internal properties
        internal abstract TypeUsageInfoConfiguration TypeConfiguration { get; }
        #endregion
        #region	Public methods
        public TypeUsageInfo GetTypeUsage(params TypeUsageInfo[] typeArguments)
        {
            return TypeUsageInfo.Create(Name, Namespace, TypeConfiguration, typeArguments: typeArguments);
        }
        #endregion
    }

    
}