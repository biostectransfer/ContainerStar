using System.Collections.Generic;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.MSSQL.Contracts.Database
{
    public interface IExtendedPropertyHolder : IBaseObject
    {
        List<MSSQLExtendedProperty> ExtendedProperties { get; }
    }
}