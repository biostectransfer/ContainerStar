namespace MetadataLoader.EntityFramework.Extractors
{
    public interface IRelationshipData<out TTable, out TColumn>
    {
        IRelationshipSideData<TTable, TColumn> From { get; }
        IRelationshipSideData<TTable, TColumn> To { get; }
        bool WillCascadeOnDelete { get; set; }
    }
}