using System;

namespace MetadataLoader.Contracts.CSharp
{
    [Flags]
    public enum TypeArgumentConditionModifier
    {
        None = 0x0,
        Class = 0x1,
        Struct = 0x2,
        DefaultConstructor = 0x4
    }
}