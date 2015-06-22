using System;
using MetadataLoader.Contracts.CSharp;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.EntityFramework.Extractors
{
    public class EntityPropertyExtractor<TTable, TTableContent, TColumn, TColumnContent> : IEntityPropertyExtractor<TTable, TTableContent, TColumn, TColumnContent>
        where TTable : ITable<TTableContent, TColumnContent>
        where TColumn : IColumn<TColumnContent>
        where TTableContent : IContent
        where TColumnContent : IContent
    {
        public virtual string GetSimplePropertyName(IPropertyMetadata<TTable, TTableContent, TColumn, TColumnContent> data, Func<string, bool> checkDuplicates)
        {
            return data.Column.Content.CodeName;
        }
        public virtual TypeUsageInfo GetSimplePropertyTypeInfo(IPropertyMetadata<TTable, TTableContent, TColumn, TColumnContent> data)
        {
            var column = data.Column;
            if (column.TypeInfo.IsValueType && !column.IsRequired)
            {
                return TypeUsageInfo.CreateNullable(column.TypeInfo);
            }
            return column.TypeInfo;
        }
    }
}