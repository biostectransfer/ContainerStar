namespace MetadataLoader.EntityFramework.Extractors
{
    public interface INavigationPropertyNameData<out TTable, out TColumn>
    {
        IRelationshipData<TTable, TColumn> Relationship { get; }
        NavigationPropertyName Name { get; }

        IRelationshipSideData<TTable, TColumn> TypeSide { get; }
        IRelationshipSideData<TTable, TColumn> NameSide { get; }
    }
}