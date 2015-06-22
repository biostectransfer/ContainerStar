using System.Xml;

namespace MetadataLoader.Excel.OfficeOpenXML
{
    /// <summary>
    /// Represents an individual column within the worksheet
    /// </summary>
    public class ExcelColumn
    {
        private readonly ExcelWorksheet _xlWorksheet;
        private readonly XmlElement _colElement;
        private readonly XmlNamespaceManager _nsManager;
        #region ExcelColumn Constructor
        /// <summary>
        /// Creates a new instance of the ExcelColumn class.  
        /// For internal use only!
        /// </summary>
        /// <param name="Worksheet"></param>
        /// <param name="col"></param>
        protected internal ExcelColumn(ExcelWorksheet Worksheet, int col)
        {
            var nt = new NameTable();
            _nsManager = new XmlNamespaceManager(nt);
            _nsManager.AddNamespace("d", ExcelPackage.schemaMain);

            _xlWorksheet = Worksheet;
            var parent = Worksheet.WorksheetXml.SelectSingleNode("//d:cols", _nsManager);
            if (parent == null)
            {
                parent = Worksheet.WorksheetXml.CreateElement("cols", ExcelPackage.schemaMain);
                var refChild = Worksheet.WorksheetXml.SelectSingleNode("//d:sheetData", _nsManager);
                parent = Worksheet.WorksheetXml.DocumentElement.InsertBefore(parent, refChild);
            }
            XmlAttribute minAttr;
            XmlAttribute maxAttr;
            XmlNode insertBefore = null;
            // the column definitions cover a range of columns, so find the one we want
            var insertBeforeFound = false;
            foreach (XmlNode colNode in parent.ChildNodes)
            {
                var min = 1;
                var max = 1;
                minAttr = (XmlAttribute) colNode.Attributes.GetNamedItem("min");
                if (minAttr != null)
                {
                    min = int.Parse(minAttr.Value);
                }
                maxAttr = (XmlAttribute) colNode.Attributes.GetNamedItem("max");
                if (maxAttr != null)
                {
                    max = int.Parse(maxAttr.Value);
                }
                if (!insertBeforeFound && (col <= min || col <= max))
                {
                    insertBeforeFound = true;
                    insertBefore = colNode;
                }
                if (col >= min && col <= max)
                {
                    _colElement = (XmlElement) colNode;
                    break;
                }
            }
            if (_colElement == null)
            {
                // create the new column definition
                _colElement = Worksheet.WorksheetXml.CreateElement("col", ExcelPackage.schemaMain);
                _colElement.SetAttribute("min", col.ToString());
                _colElement.SetAttribute("max", col.ToString());

                if (insertBefore != null)
                {
                    parent.InsertBefore(_colElement, insertBefore);
                }
                else
                {
                    parent.AppendChild(_colElement);
                }
            }
        }
        #endregion
        /// <summary>
        /// Returns a reference to the Element that represents the column.
        /// For internal use only!
        /// </summary>
        protected internal XmlElement Element
        {
            get { return (_colElement); }
        }

        /// <summary>
        /// Sets the first column the definition refers to.
        /// </summary>
        public int ColumnMin
        {
            get { return (int.Parse(_colElement.GetAttribute("min"))); }
            set { _colElement.SetAttribute("min", value.ToString()); }
        }

        /// <summary>
        /// Sets the last column the definition refers to.
        /// </summary>
        public int ColumnMax
        {
            get { return (int.Parse(_colElement.GetAttribute("max"))); }
            set { _colElement.SetAttribute("max", value.ToString()); }
        }
        #region ExcelColumn Hidden
        /// <summary>
        /// Allows the column to be hidden in the worksheet
        /// </summary>
        public bool Hidden
        {
            get
            {
                var retValue = false;
                var hidden = _colElement.GetAttribute("hidden", "1");
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
                    _colElement.SetAttribute("hidden", "1");
                }
                else
                {
                    _colElement.SetAttribute("hidden", "0");
                }
            }
        }
        #endregion
        #region ExcelColumn Width
        /// <summary>
        /// Sets the width of the column in the worksheet
        /// </summary>
        public double Width
        {
            get
            {
                double retValue = 10; // default column size
                var width = _colElement.GetAttribute("width");
                if (width != "")
                {
                    retValue = int.Parse(width);
                }
                return retValue;
            }
            set { _colElement.SetAttribute("width", value.ToString()); }
        }
        #endregion
        #region ExcelColumn Style
        /// <summary>
        /// Sets the style for the entire column using a style name.
        /// </summary>
        public string Style
        {
            get { return _xlWorksheet.GetStyleName(StyleID); }
            set
            {
                // TODO: implement correctly.  The current code causes Excel to throw a fit!
                StyleID = _xlWorksheet.GetStyleID(value);
            }
        }
        /// <summary>
        /// Sets the style for the entire column using the style ID.  
        /// </summary>
        public int StyleID
        {
            get
            {
                var retValue = 0;
                var sid = _colElement.GetAttribute("s");
                if (sid != "")
                {
                    retValue = int.Parse(sid);
                }
                return retValue;
            }
            set { _colElement.SetAttribute("s", value.ToString()); }
        }
        #endregion
        /// <summary>
        /// Returns the range of columns covered by the column definition.
        /// </summary>
        /// <returns>A string describing the range of columns covered by the column definition.</returns>
        public override string ToString()
        {
            return string.Format("Column Range: {0} to {1}", _colElement.GetAttribute("min"), _colElement.GetAttribute("min"));
        }
    }
}