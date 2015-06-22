using System.Xml;

namespace MetadataLoader.Excel.OfficeOpenXML
{
    /// <summary>
    /// Represents the different view states of the worksheet
    /// </summary>
    public class ExcelWorksheetView
    {
        // TODO: implement the different view states of the worksheet
        private readonly ExcelWorksheet _xlWorksheet;
        private XmlElement _sheetView;
        #region ExcelWorksheetView Constructor
        /// <summary>
        /// Creates a new ExcelWorksheetView which provides access to all the 
        /// view states of the worksheet.
        /// </summary>
        /// <param name="xlWorksheet"></param>
        protected internal ExcelWorksheetView(ExcelWorksheet xlWorksheet)
        {
            _xlWorksheet = xlWorksheet;
        }
        #endregion
        #region SheetViewElement
        /// <summary>
        /// Returns a reference to the sheetView element
        /// </summary>
        protected internal XmlElement SheetViewElement
        {
            get
            {
                if (_sheetView == null)
                {
                    _sheetView = (XmlElement) _xlWorksheet.WorksheetXml.SelectSingleNode("//d:sheetView", _xlWorksheet.NameSpaceManager);
                }
                return _sheetView;
            }
        }
        #endregion
        #region TabSelected
        /// <summary>
        /// Indicates if the worksheet is selected within the workbook
        /// </summary>
        public bool TabSelected
        {
            get
            {
                var retValue = false;
                var ret = SheetViewElement.GetAttribute("tabSelected");
                if (ret == "1")
                {
                    retValue = true;
                }
                return retValue;
            }
            set
            {
                // the sheetView node should always exist, so no need to create
                if (value)
                {
                    // ensure no other worksheet has its tabSelected attribute set to 1
                    foreach (ExcelWorksheet sheet in _xlWorksheet.xlPackage.Workbook.Worksheets)
                    {
                        sheet.View.TabSelected = false;
                    }

                    SheetViewElement.SetAttribute("tabSelected", "1");
                }
                else
                {
                    SheetViewElement.SetAttribute("tabSelected", "0");
                }
            }
        }
        #endregion
        #region PageLayoutView
        /// <summary>
        /// Sets the view mode of the worksheet to pageLayout
        /// </summary>
        public bool PageLayoutView
        {
            get
            {
                var retValue = false;
                var ret = SheetViewElement.GetAttribute("view");
                if (ret == "pageLayout")
                {
                    retValue = true;
                }
                return retValue;
            }
            set
            {
                if (value)
                {
                    SheetViewElement.SetAttribute("view", "pageLayout");
                }
                else
                {
                    SheetViewElement.RemoveAttribute("view");
                }
            }
        }
        #endregion
    }
}