namespace MetadataLoader.Contracts.Database
{
    public interface IBaseObject
    {
        string Name { get; }
        string Description { get; set; }
        IContent Content { get; }
    }

    public interface IBaseObject<out T> : IBaseObject
    {
        new T Content { get; }
    }
}