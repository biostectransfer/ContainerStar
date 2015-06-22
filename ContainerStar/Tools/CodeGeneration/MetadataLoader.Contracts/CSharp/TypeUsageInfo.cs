using System.Collections.Generic;
using System.Linq;

namespace MetadataLoader.Contracts.CSharp
{
    public sealed class TypeUsageInfo : UsageInfo
    {
        #region Static
        public static TypeUsageInfo CreateTypeArgument(string name, TypeArgumentModifier modifier = TypeArgumentModifier.None,
            TypeArgumentConditionModifier conditionModifier = TypeArgumentConditionModifier.None,
            params TypeUsageInfo[] conditionTypes)
        {
            return CreateTypeArgument(name, new TypeArgumentConfiguration(modifier, conditionModifier, conditionTypes));
        }
        public static TypeUsageInfo CreateTypeArgument(string name, TypeArgumentConfiguration configuration)
        {
            return new TypeUsageInfo(name, null, null, TypeUsageInfoConfiguration.TypeArgument, null, configuration);
        }
        public static TypeUsageInfo CreateClass(string name, string typeNamespace, TypeUsageInfo[] typeParameters = null)
        {
            return new TypeUsageInfo(name, typeNamespace, null, TypeUsageInfoConfiguration.Class, typeParameters);
        }
        public static TypeUsageInfo CreateInterface(string name, string typeNamespace, TypeUsageInfo[] typeParameters = null)
        {
            return new TypeUsageInfo(name, typeNamespace, null, TypeUsageInfoConfiguration.Interface, typeParameters);
        }
        public static TypeUsageInfo CreateArray(TypeUsageInfo elementType, int arrayRank)
        {
            return new TypeUsageInfo(elementType.Name, elementType.Namespace, elementType.ShortName,
                elementType._configuration, new[] {elementType}) {ArrayRank = arrayRank};
        }
        public static TypeUsageInfo CreateNullable(TypeUsageInfo elementType)
        {
            if (!elementType.IsValueType)
            {
                return elementType;
            }
            return new TypeUsageInfo(elementType.Name, elementType.Namespace, elementType.ShortName,
                elementType._configuration | TypeUsageInfoConfiguration.Nullable,
                new[] {elementType});
        }
        public static TypeUsageInfo CreateEnum(string name, string typeNamespace)
        {
            return new TypeUsageInfo(name, typeNamespace, null, TypeUsageInfoConfiguration.Enum);
        }
        public static TypeUsageInfo CreateValueType(string name, string typeNamespace, TypeUsageInfo[] typeArguments = null)
        {
            return new TypeUsageInfo(name, typeNamespace, null, TypeUsageInfoConfiguration.ValueType, typeArguments);
        }
        internal static TypeUsageInfo Create(string name, string typeNamespace, TypeUsageInfoConfiguration configuration, string shortName = null, TypeUsageInfo[] typeArguments = null)
        {
            return new TypeUsageInfo(name, typeNamespace, shortName, configuration, typeArguments);
        }
        #endregion
        #region	Private fields
        private readonly TypeUsageInfoConfiguration _configuration;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        private TypeUsageInfo(string name, string typeNamespace, string shortName,
            TypeUsageInfoConfiguration configuration = TypeUsageInfoConfiguration.Class,
            TypeUsageInfo[] typeParameters = null,
            TypeArgumentConfiguration typeArgumentConfiguration = null)
            : base(name, typeNamespace)
        {
            _configuration = configuration;
            Namespace = typeNamespace;
            ShortName = shortName;
            TypeArgumentConfiguration = typeArgumentConfiguration;
            TypeArguments = typeParameters ?? new TypeUsageInfo[0];
        }
        #endregion
        #region	Public properties
        public string ShortName { get; private set; }
        public string Namespace { get; private set; }
        public bool IsTypeArgument
        {
            get { return _configuration.HasFlag(TypeUsageInfoConfiguration.TypeArgument); }
        }
        public bool IsValueType
        {
            get { return _configuration.HasFlag(TypeUsageInfoConfiguration.ValueType); }
        }
        public bool IsEnum
        {
            get { return _configuration.HasFlag(TypeUsageInfoConfiguration.Enum); }
        }
        public bool IsInterface
        {
            get { return _configuration.HasFlag(TypeUsageInfoConfiguration.Interface); }
        }
        public bool IsClass
        {
            get { return _configuration.HasFlag(TypeUsageInfoConfiguration.Class); }
        }
        public bool IsNullable
        {
            get { return _configuration.HasFlag(TypeUsageInfoConfiguration.Nullable); }
        }
        public int ArrayRank { get; private set; }
        public bool IsArray
        {
            get { return ArrayRank > 0; }
        }
        public TypeUsageInfo ArrayElement
        {
            get { return IsArray ? TypeArguments[0] : null; }
        }
        public bool IsGeneric
        {
            get { return TypeArguments.Length != 0; }
        }
        public TypeArgumentConfiguration TypeArgumentConfiguration { get; private set; }
        /// <summary>
        ///     Type parameters or ElementType of arrays
        /// </summary>
        public TypeUsageInfo[] TypeArguments { get; private set; }
        public string FullCodeName()
        {
            return string.Format("{0}.{1}", Namespace, CodeName(false));
        }
        public string CodeName(bool useShortName = true)
        {
            var name = useShortName && !string.IsNullOrEmpty(ShortName) ? ShortName : Name;
            if (IsNullable)
            {
                return name + "?";
            }
            if (IsArray)
            {
                return string.Format("{0}[{1}]", name, ArrayRank == 1 ? string.Empty : new string(',', ArrayRank - 1));
            }
            if (TypeArguments.Length != 0)
            {
                return string.Format("{0}<{1}>", name, string.Join(", ", TypeArguments.Select(t => t.CodeName(useShortName))));
            }
            return name;
        }
        #endregion
        #region	Public methods
        public override IEnumerable<string> GetUsedNamespaces(bool includeSelf = false)
        {
            return base.GetUsedNamespaces(includeSelf).Union(TypeArguments.SelectMany(info => info.GetUsedNamespaces(true)));
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return CodeName();
        }
        #endregion
    }
}