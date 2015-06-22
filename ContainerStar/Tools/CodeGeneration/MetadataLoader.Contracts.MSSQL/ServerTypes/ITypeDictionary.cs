using System.Collections.Generic;

namespace MetadataLoader.MSSQL.Contracts.ServerTypes
{
    internal interface ITypeDictionary
    {
        void AddDesc(MSSQLTypeDesc desc, List<string> names);
    }
}