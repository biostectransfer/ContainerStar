using System;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.EntityFramework.Extractors
{
    internal sealed class RelationshipSideData<TTable, TTableContent, TColumn, TColumnContent> : IRelationshipSideData<TTable, TColumn>
        where TTable : ITable<TTableContent, TColumnContent>
        where TColumn : IColumn<TColumnContent>
    {
        private NavigationType _type;
        #region Static
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public RelationshipSideData(TTable table, TColumn[] key, EntityInfo entity, NavigationType type)
        {
            Table = table;
            Key = key;
            Entity = entity;
            _type = type;
        }
        #endregion
        #region	Public properties
        public TTable Table { get; private set; }
        public TColumn[] Key { get; private set; }
        public EntityInfo Entity { get; private set; }
        public NavigationType Type
        {
            get { return _type; }
            set
            {
                if (_type == value)
                {
                    return;
                }
                switch (_type)
                {
                    case NavigationType.Many:
                    case NavigationType.Required:
                        throw new ArgumentOutOfRangeException("value");
                }
                _type = value;
            }
        }
        public bool IsUndirect { get; set; }
        #endregion
        #region	Public methods
        public SimplePropertyEntityInfo[] GetKeyProperties()
        {
            return Entity.GetKeyProperties(Key);
        }
        #endregion
    }
}