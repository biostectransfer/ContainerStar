namespace MetadataLoader.Contracts.CSharp
{
    public interface IClassMember
    {
        bool IsAbstract { get; }
        bool IsVirtual { get; }
        bool IsOverride { get; }
    }
}