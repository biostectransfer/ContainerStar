using MetadataLoader.Excel;

namespace MetadataLoader.Content
{
    public interface IExcelContentLoader
    {
        void Load(string filePath, bool hasSchema = true);
        void Load(ContentLoaderConfiguration configuration, bool hasSchema = true);
        void CreateDefaultTemplate(string filePath, ContentLoadType type, bool hasSchema = true);
    }
}