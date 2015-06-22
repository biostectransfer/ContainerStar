using MetadataLoader.Contracts.Database;
using MetadataLoader.EntityFramework;
using MetadataLoader.EntityFramework.Extractors;

namespace ContainerStar.Generation
{
    public sealed class EntityPropertyExtractor : EntityPropertyExtractor<ITable<TableContent, ColumnContent>, TableContent, IColumn<ColumnContent>, ColumnContent>
    {
    }
}