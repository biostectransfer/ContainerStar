using System.Diagnostics.Contracts;
using MetadataLoader.Contracts.CSharp;

namespace MetadataLoader.MSSQL.Contracts.ServerTypes
{
    public class MSSQLTypeDesc : ITypeDesc

    {
        #region Private fields
        #endregion
        #region Constructor
        internal MSSQLTypeDesc(short typeIndex, NativeTypes baseType, string name, TypeOptions options,
            TypeUsageInfo typeInfo)
        {
            Contract.Requires(typeIndex > 256);
            Contract.Requires(typeInfo != null);
            
            Id = typeIndex;
            BaseType = baseType;
            IsDerived = Id != (short) BaseType;

            Name = name;
            Options = options;
            TypeInfo = typeInfo;
        }
        #endregion
        #region Public properties
        public short Id { get; private set; }
        public TypeUsageInfo TypeInfo { get; private set; }
        public string Name { get; private set; }
        public NativeTypes BaseType { get; private set; }
        public TypeOptions Options { get; private set; }
        public bool IsDerived { get; private set; }
        public bool HasLength
        {
            get { return Options.HasFlag(TypeOptions.HasLength); }
        }
        public bool HasPrecision
        {
            get { return Options.HasFlag(TypeOptions.HasPrecision); }
        }
        public bool HasScale
        {
            get { return Options.HasFlag(TypeOptions.HasScale); }
        }
        public bool HasCollation
        {
            get { return Options.HasFlag(TypeOptions.HasCollation); }
        }
        public bool IsLob
        {
            get { return Options.HasFlag(TypeOptions.IsLob); }
        }
        public bool IsLobWithMax
        {
            get { return Options.HasFlag(TypeOptions.IsLobWithMax); }
        }
        public bool IsVirtual
        {
            get { return Options.HasFlag(TypeOptions.IsVirtual); }
        }
        public bool IsUnicode
        {
            get { return Options.HasFlag(TypeOptions.IsUnicode); }
        }
        public bool IsFixedLength
        {
            get { return Options.HasFlag(TypeOptions.IsFixedLength); }
        }
        #endregion
        #region	Public methods
        public override string ToString()
        {
            return IsVirtual ? BaseType.ToString() : Name;
        }
        #endregion
    }
}