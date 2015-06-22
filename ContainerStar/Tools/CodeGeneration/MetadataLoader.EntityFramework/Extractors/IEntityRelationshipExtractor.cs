using System;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.EntityFramework.Extractors
{
    public interface IEntityRelationshipExtractor<in TTable, in TTableContent, in TColumn, in TColumnContent>
        where TTable : ITable<TTableContent, TColumnContent>
        where TColumn : IColumn<TColumnContent>
    {
        bool HandleAndApproveRelationship(IRelationshipData<TTable, TColumn> relationship);
        string GetNavigationPropertyName(INavigationPropertyNameData<TTable, TColumn> data, Func<string, bool> checkDuplicates);
    }
}