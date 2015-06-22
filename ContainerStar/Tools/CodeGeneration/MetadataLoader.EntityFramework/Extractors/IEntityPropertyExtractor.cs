using System;
using MetadataLoader.Contracts.CSharp;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.EntityFramework.Extractors
{
    public interface IEntityPropertyExtractor<in TTable, in TTableContent, in TColumn, in TColumnContent>
        where TTable : ITable<TTableContent, TColumnContent>
        where TColumn : IColumn<TColumnContent>
    {
        string GetSimplePropertyName(IPropertyMetadata<TTable, TTableContent, TColumn, TColumnContent> data, Func<string, bool> checkDuplicates);
        TypeUsageInfo GetSimplePropertyTypeInfo(IPropertyMetadata<TTable, TTableContent, TColumn, TColumnContent> data);
    }
}