using System;
using System.Xml;

namespace MetadataLoader.Excel.OfficeOpenXML
{
    /// <summary>
    /// Represents an individual row in the spreadsheet.
    /// </summary>
    public class ExcelRow
    {
        private readonly ExcelWorksheet _xlWorksheet;
        private readonly XmlElement _rowElement;
        #region ExcelRow Constructor
        /// <summary>
        /// Creates a new instance of the ExcelRow class. 
        /// For internal use only!
        /// </summary>
        /// <param name="Worksheet">The parent worksheet</param>
        /// <param name="row">The row number</param>
        protected internal ExcelRow(ExcelWorksheet Worksheet, int row)
        {
            _xlWorksheet = Worksheet;

            //  Search for the existing row
            _rowElement =
                (XmlElement)
                Worksheet.WorksheetXml.SelectSingleNode(string.Format("//d:sheetData/d:row[@r='{0}']", row), _xlWorksheet.NameSpaceManager);
            if (_rowElement == null)
            {
                // We didn't find the row, so add a new row element.
                // HOWEVER we MUST insert new row in the correct position - otherwise Excel 2007 will complain!!!
                _rowElement = Worksheet.WorksheetXml.CreateElement("row", ExcelPackage.schemaMain);
                _rowElement.SetAttribute("r", row.ToString());

                // now work out where to insert the new row
                var sheetDataNode = Worksheet.WorksheetXml.SelectSingleNode("//d:sheetData", _xlWorksheet.NameSpaceManager);
                if (sheetDataNode != null)
                {
                    XmlNode followingRow = null;
                    foreach (XmlNode currentRow in Worksheet.WorksheetXml.SelectNodes("//d:sheetData/d:row", _xlWorksheet.NameSpaceManager))
                    {
                        var rowFound = Convert.ToInt32(currentRow.Attributes.GetNamedItem("r").Value);
                        if (rowFound > row)
                        {
                            followingRow = currentRow;
                            break;
                        }
                    }
                    if (followingRow == null)
                    {
                        // no data rows exist, so just add row
                        sheetDataNode.AppendChild(_rowElement);
                    }
                    else
                    {
                        sheetDataNode.InsertBefore(_rowElement, followingRow);
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// Provides access to the node representing the row.
        /// For internal use only!
        /// </summary>
        protected internal XmlNode Node
        {
            get { return (_rowElement); }
        }
        #region ExcelRow Hidden
        /// <summary>
        /// Allows the row to be hidden in the worksheet
        /// </summary>
        public bool Hidden
        {
            get
            {
                var retValue = false;
                var hidden = _rowElement.GetAttribute("hidden", "1");
                if (hidden == "1")
                {
                    retValue = true;
                }
                return (retValue);
            }
            set
            {
                if (value)
                {
                    _rowElement.SetAttribute("hidden", "1");
                }
                else
                {
                    _rowElement.SetAttribute("hidden", "0");
                }
            }
        }
        #endregion
        #region ExcelRow Height
        /// <summary>
        /// Sets the height of the row
        /// </summary>
        public double Height
        {
            get
            {
                double retValue = 10; // default row height
                var ht = _rowElement.GetAttribute("ht");
                if (ht != "")
                {
                    retValue = double.Parse(ht);
                }
                return (retValue);
            }
            set
            {
                _rowElement.SetAttribute("ht", value.ToString());
                // we must set customHeight="1" for the height setting to take effect
                _rowElement.SetAttribute("customHeight", "1");
            }
        }
        #endregion
        #region ExcelRow Style
        /// <summary>
        /// Gets/sets the style name based on the StyleID
        /// </summary>
        public string Style
        {
            get { return _xlWorksheet.GetStyleName(StyleID); }
            set { StyleID = _xlWorksheet.GetStyleID(value); }
        }

        /// <summary>
        /// Sets the style for the entire row using the style ID.  
        /// </summary>
        public int StyleID
        {
            get
            {
                var retValue = 0;
                var sid = _rowElement.GetAttribute("s");
                if (sid != "")
                {
                    retValue = int.Parse(sid);
                }
                return retValue;
            }
            set
            {
                _rowElement.SetAttribute("s", value.ToString());
                // to get Excel to apply this style we need to set customFormat="1"
                _rowElement.SetAttribute("customFormat", "1");
            }
        }
        #endregion
    }
}