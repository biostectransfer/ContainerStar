using System;

namespace MetadataLoader.Contracts.CSharp
{
    [Flags]
    internal enum TypeUsageInfoConfiguration
    {
        None = 0,
        Class = 0x1,
        ValueType = 0x2,
        Enum = 0x4 | ValueType,
        Interface = 0x8,
        TypeArgument = 0x10,
        Nullable = 0x20,
        Struct = 0x40 | ValueType
    }
}