using System;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.EntityFramework.Extractors
{
    public class EntityRelationshipExtractor<TTable, TTableContent, TColumn, TColumnContent>
        : IEntityRelationshipExtractor<TTable, TTableContent, TColumn, TColumnContent>
        where TTable : ITable<TTableContent, TColumnContent>
        where TColumn : IColumn<TColumnContent>
        where TTableContent : IContent
        where TColumnContent : IContent
    {
        public bool HandleAndApproveRelationship(IRelationshipData<TTable, TColumn> relationship)
        {
            return true;
        }
        public virtual string GetNavigationPropertyName(INavigationPropertyNameData<TTable, TColumn> data, Func<string, bool> checkDuplicates)
        {
            return data.TypeSide.Type == NavigationType.Many ? data.NameSide.Table.Content.CodeNamePlural : data.NameSide.Table.Content.CodeName;
        }
    }
}