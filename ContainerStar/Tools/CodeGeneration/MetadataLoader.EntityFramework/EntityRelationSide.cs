namespace MetadataLoader.EntityFramework
{
    public sealed class EntityRelationSide
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public EntityRelationSide(EntityInfo entity, NavigationPropertyEntityInfo property, NavigationType navigationType, SimplePropertyEntityInfo[] keyColumns)
        {
            Property = property;
            NavigationType = navigationType;
            KeyColumns = keyColumns;
            Entity = entity;
        }
        #endregion
        #region	Public properties
        public NavigationPropertyEntityInfo Property { get; private set; }
        public NavigationType NavigationType { get; private set; }
        public SimplePropertyEntityInfo[] KeyColumns { get; private set; }
        public bool IsUndirect
        {
            get { return Property == null; }
        }
        public EntityInfo Entity { get; private set; }
        #endregion
        #region	Public methods
        public void MakeUndirect()
        {
            Property = null;
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Entity.Name;
        }
        #endregion
    }
}