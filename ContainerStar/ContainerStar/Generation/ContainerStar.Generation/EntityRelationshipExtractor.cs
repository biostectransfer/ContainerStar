using MetadataLoader.Contracts.Database;
using MetadataLoader.EntityFramework.Extractors;

namespace ContainerStar.Generation
{
    public sealed class EntityRelationshipExtractor : EntityRelationshipExtractor<ITable<TableContent, ColumnContent>, TableContent, IColumn<ColumnContent>, ColumnContent>
    {
    }
}