namespace MetadataLoader.Content
{
    public interface IPropertyMapper<in TContent>
    {
        void Map(TContent content, string value);
    }
}