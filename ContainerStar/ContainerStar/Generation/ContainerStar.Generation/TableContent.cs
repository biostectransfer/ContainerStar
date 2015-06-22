using MetadataLoader.Contracts.Database;
using MetadataLoader.Database;

namespace ContainerStar.Generation
{
    public class TableContent : BaseMasterDataContent
    {
        public bool IsRelated { get; set; }
        public string RelationFieldName { get; set; }
        public string PrimaryObject { get; set; }
        public string Group { get; set; }
        public string SubGroup { get; set; }
        public string ViewCollections { get; set; }
        public bool IsSystem { get; set; }
        public bool SkipManagerConstructor { get; set; }
        public bool SkipControllerConstructor { get; set; }
        public bool SkipController { get; set; }
        /// <summary>
        /// Add execution of ExtraEntityToModel implemented in partial custom part of controller
        /// </summary>
        public bool ExtraEntityToModel { get; set; }
        /// <summary>
        /// Add execution of ExtraModelToEntity implemented in partial custom part of controller
        /// </summary>
        public bool ExtraModelToEntity { get; set; }

        public bool ShowInMenu { get; set; }
        public string RelationShips { get; set; }
        public string RelationShipsSelectors { get; set; }
        public string RelationShipsDe { get; set; }
        public string RelationShipsEn { get; set; }
        public string TitleFieldName { get; set; }
        public bool GenerateViewCollection { get; set; }
        public bool ShowExtraFields { get; set; }
        public bool CustomEvents { get; set; }
        public bool AddNewItemInline { get; set; }
        public bool ShowAddButton { get; set; }
        public bool ShowEditButton { get; set; }
        public bool ShowDeleteButton { get; set; }
        public int PermissionId { get; set; }
        public string PermissionDescription { get; set; }

        public bool ExcelExport { get; set; }
        public bool GenerateFilter { get; set; }
    }
}