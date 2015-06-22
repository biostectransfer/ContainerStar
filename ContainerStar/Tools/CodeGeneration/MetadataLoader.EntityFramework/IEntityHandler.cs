namespace MetadataLoader.EntityFramework
{
    public interface IEntityHandler<in TIn, in T>
    {
        void Run(TIn input, T obj);
    }
}