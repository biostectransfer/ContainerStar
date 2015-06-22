using System.Collections.Generic;
using MetadataLoader.MSSQL.Contracts.Database;
using MetadataLoader.EntityFramework;
using MetadataLoader.MSSQL.EntityFramework;

namespace MasterDataModule.Generation
{
    public sealed class EntitiesManager
    {
        public static EntitiesManager Create()
        {
            return new EntitiesManager();
        }
        #region	Private fields
        private readonly IEntityLoader<MSSQLTable<TableContent, ColumnContent>> _entityLoader;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public EntitiesManager()
        {
            _entityLoader = MSSQLManager<TableContent, ColumnContent>.Instance.GetEntityLoader(
                new EntityExtractor(),
                new EntityPropertyExtractor(),
                new EntityRelationshipExtractor());
        }
        #endregion
        #region	Public methods
        public List<EntityInfo> Load(IEnumerable<MSSQLTable<TableContent, ColumnContent>> tables)
        {
            return _entityLoader.Load(tables);
        }
        #endregion
    }
}