namespace MetadataLoader.EntityFramework.Extractors
{
    public sealed class NavigationPropertyNameData<TTable, TColumn> : INavigationPropertyNameData<TTable, TColumn>
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public NavigationPropertyNameData(IRelationshipData<TTable, TColumn> relationship, NavigationPropertyName name)
        {
            Relationship = relationship;
            Name = name;
        }
        #endregion
        #region	Public properties
        public IRelationshipData<TTable, TColumn> Relationship { get; private set; }
        public NavigationPropertyName Name { get; private set; }
        public IRelationshipSideData<TTable, TColumn> TypeSide
        {
            get { return Name == NavigationPropertyName.To ? Relationship.To : Relationship.From; }
        }
        public IRelationshipSideData<TTable, TColumn> NameSide
        {
            get { return Name == NavigationPropertyName.To ? Relationship.From : Relationship.To; }
        }
        #endregion
    }
}