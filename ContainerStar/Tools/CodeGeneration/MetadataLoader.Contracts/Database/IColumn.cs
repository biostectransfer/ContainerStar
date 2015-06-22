using MetadataLoader.Contracts.CSharp;

namespace MetadataLoader.Contracts.Database
{
    public interface IColumn : IBaseObject
    {
        bool IsKeyColumn { get; }
        int? KeyOrder { get; set; }
        TypeUsageInfo TypeInfo { get; }
        bool IsRequired { get; set; }
    }

    public interface IColumn<out T> : IColumn, IBaseObject<T>
    {
    }
}