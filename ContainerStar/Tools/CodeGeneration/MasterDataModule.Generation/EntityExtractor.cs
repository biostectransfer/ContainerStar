using MetadataLoader.Contracts.Database;
using MetadataLoader.EntityFramework.Extractors;

namespace MasterDataModule.Generation
{
    public sealed class EntityExtractor : EntityExtractor<IBaseObject<TableContent>, TableContent>
    {
    }
}