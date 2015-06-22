namespace MetadataLoader.Content
{
    public interface IContentMapper<in T>
    {
        void Map(T content, string[] data);
    }
}