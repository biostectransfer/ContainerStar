namespace MetadataLoader.Content
{
    public sealed class ContentLoaderConfiguration
    {
        #region Constructor
        public ContentLoaderConfiguration(string filePath)
        {
            FilePath = filePath;
        }
        #endregion
        #region	Public properties
        public string FilePath { get; private set; }
        #endregion
    }
}