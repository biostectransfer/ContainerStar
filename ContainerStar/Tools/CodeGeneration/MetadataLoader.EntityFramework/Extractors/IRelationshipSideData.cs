namespace MetadataLoader.EntityFramework.Extractors
{
    public interface IRelationshipSideData<out TTable, out TColumn>
    {
        TTable Table { get; }
        TColumn[] Key { get; }
        EntityInfo Entity { get; }
        SimplePropertyEntityInfo[] GetKeyProperties();
        NavigationType Type { get; set; }
        bool IsUndirect { get; set; }
    
    }
}