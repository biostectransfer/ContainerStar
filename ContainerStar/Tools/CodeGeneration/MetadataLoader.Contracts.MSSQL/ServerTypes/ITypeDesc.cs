using MetadataLoader.Contracts.CSharp;

namespace MetadataLoader.MSSQL.Contracts.ServerTypes
{
    public interface ITypeDesc
    {
        short Id { get; }
        TypeUsageInfo TypeInfo { get; }
        NativeTypes BaseType { get; }
        TypeOptions Options { get; }
        bool HasLength { get; }
        bool HasPrecision { get; }
        bool HasScale { get; }
        string Name { get; }
        bool HasCollation { get; }
        bool IsLob { get; }
        bool IsLobWithMax { get; }
        bool IsVirtual { get; }
    }
}