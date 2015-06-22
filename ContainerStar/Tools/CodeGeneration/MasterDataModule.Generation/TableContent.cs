using MetadataLoader.Contracts.Database;
using MetadataLoader.Database;

namespace MasterDataModule.Generation
{
    public class TableContent : BaseContent
    {
        public bool IsRelated { get; set; }
        public string PrimaryObject { get; set; }
        public string Group { get; set; }
        public string ViewCollections { get; set; }
        public string DeName { get; set; }
        public string EnName { get; set; }
    }
}