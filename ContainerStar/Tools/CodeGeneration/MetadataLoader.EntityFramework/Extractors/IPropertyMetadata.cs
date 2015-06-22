using MetadataLoader.Contracts.Database;

namespace MetadataLoader.EntityFramework.Extractors
{
    public interface IPropertyMetadata<out TTable, out TTableContent, out TColumn, out TColumnContent>
        where TTable : ITable<TTableContent, TColumnContent>
        where TColumn : IColumn<TColumnContent>
    {
        TTable Table { get; }
        TColumn Column { get; }
    }
}