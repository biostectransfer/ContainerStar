using System;

namespace MetadataLoader.MSSQL.Contracts.ServerTypes
{
    /// <summary>Configurations of <see cref="MSSQLTypeDesc"/> object  </summary>
    [Flags]
    public enum TypeOptions
    {
        None = 0x00,
        HasLength = 0x01,
        HasPrecision = 0x02,
        HasScale = 0x04,
        HasCollation = 0x08,
        IsLob = 0x10,
        IsLobWithMax = 0x20,
        IsVirtual = 0x40,
        IsUnicode = 0x80,
        IsFixedLength = 0x100
    }
}