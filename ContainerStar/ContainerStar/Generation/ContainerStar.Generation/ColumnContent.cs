using MetadataLoader.Contracts.Database;
using MetadataLoader.Database;

namespace ContainerStar.Generation
{
    public class ColumnContent : BaseMasterDataContent
    {
        public bool InModel { get; set; }
        public bool ShowInGrid { get; set; }
        public bool ShowInAddView { get; set; }
        public bool IsModelRequired { get; set; }
        
        public string JsModelType { get; set; }
        public bool JsSkipStandardValidation { get; set; }
        public string JsExtraValidation { get; set; }

        public string ViewCollection { get; set; }
        public string CustomView { get; set; }
        public string CustomViewBindingProperty { get; set; }
        public string CustomDataBindingProperty { get; set; }
        public bool IsBusinessKey { get; set; }

        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public int GridColumnWidth { get; set; }
        public int AddViewColumnWidth { get; set; }
    }
}