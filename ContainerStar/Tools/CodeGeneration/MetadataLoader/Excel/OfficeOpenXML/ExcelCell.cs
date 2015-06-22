using System;
using System.IO.Packaging;
using System.Text.RegularExpressions;
using System.Xml;

namespace MetadataLoader.Excel.OfficeOpenXML
{
    /// <summary>
    /// ExcelCell represents an individual worksheet cell.
    /// </summary>
    public class ExcelCell
    {
        #region Cell Private Properties
        private readonly ExcelWorksheet _xlWorksheet;
        private readonly XmlElement _cellElement;
        private readonly int _row;
        private readonly int _col;
        private string _value;
        private string _valueRef;
        private string _formula;
        private Uri _hyperlink;
        #endregion
        #region ExcelCell Constructor
        /// <summary>
        /// Creates a new instance of ExcelCell class. For internal use only!
        /// </summary>
        /// <param name="xlWorksheet">A reference to the parent worksheet</param>
        /// <param name="row">The row number in the parent worksheet</param>
        /// <param name="col">The column number in the parent worksheet</param>
        protected internal ExcelCell(ExcelWorksheet xlWorksheet, int row, int col)
        {
            if (row < 1 || col < 1)
            {
                throw new Exception("ExcelCell Constructor: Negative row and column numbers are not allowed");
            }
            if (xlWorksheet == null)
            {
                throw new Exception("ExcelCell Constructor: xlWorksheet must be set to a valid reference");
            }

            _xlWorksheet = xlWorksheet;
            _row = row;
            _col = col;

            _cellElement = GetOrCreateCellElement(xlWorksheet, row, col);
        }
        #endregion  // END Cell Constructors
        #region ExcelCell Public Properties
        /// <summary>
        /// Read-only reference to the cell's XmlNode (for internal use only)
        /// </summary>
        protected internal XmlElement Element
        {
            get { return _cellElement; }
        }

        /// <summary>
        /// Read-only reference to the cell's row number
        /// </summary>
        public int Row
        {
            get { return _row; }
        }

        /// <summary>
        /// Read-only reference to the cell's column number
        /// </summary>
        public int Column
        {
            get { return _col; }
        }

        /// <summary>
        /// Returns the current cell address in the standard Excel format (e.g. 'E5')
        /// </summary>
        public string CellAddress
        {
            get { return GetCellAddress(_row, _col); }
        }

        /// <summary>
        /// Returns true if the cell's contents are numeric.
        /// </summary>
        public bool IsNumeric
        {
            get { return (IsNumericValue(Value)); }
        }
        #region ExcelCell Value
        /// <summary>
        /// Gets/sets the value of the cell.
        /// </summary>
        public string Value
        {
            get
            {
                if (_value != null)
                {
                    return (_value);
                }
                var isNumeric = true; // default
                var valueNode = _cellElement.SelectSingleNode("./d:v", _xlWorksheet.NameSpaceManager);
                if (valueNode == null)
                {
                    _valueRef = "";
                    _value = "";
                }
                else
                {
                    _valueRef = valueNode.InnerText;
                    // check to see if we have a string value
                    var attr = _cellElement.Attributes["t"];
                    if (attr != null)
                    {
                        isNumeric = attr.Value != "s";
                    }

                    _value = isNumeric ? _valueRef : GetSharedString(Convert.ToInt32(_valueRef));
                }
                return (_value);
            }
            set
            {
                _value = value;
                // set the value of the cell
                var valueNode = _cellElement.SelectSingleNode("./d:v", _xlWorksheet.NameSpaceManager);
                if (valueNode == null)
                {
                    //  Cell with deleted value. Add a value element now.
                    valueNode = _cellElement.OwnerDocument.CreateElement("v", ExcelPackage.schemaMain);
                    _cellElement.AppendChild(valueNode);
                }
                if (IsNumericValue(value))
                {
                    _valueRef = value;
                    // ensure we remove any existing string data type flag
                    var attr = _cellElement.Attributes["t"];
                    if (attr != null)
                    {
                        _cellElement.Attributes.RemoveNamedItem("t");
                    }
                }
                else
                {
                    _valueRef = SetSharedString(_value).ToString();
                    var attr = _cellElement.Attributes["t"];
                    if (attr == null)
                    {
                        attr = _cellElement.OwnerDocument.CreateAttribute("t");
                        _cellElement.Attributes.Append(attr);
                    }
                    attr.Value = "s";
                }
                valueNode.InnerText = _valueRef;
            }
        }
        #endregion
        #region ExcelCell Style
        /// <summary>
        /// Allows you to set the cell's style using a named style
        /// </summary>
        public string Style
        {
            get { return (_xlWorksheet.GetStyleName(StyleId)); }
            set { StyleId = _xlWorksheet.GetStyleID(value); }
        }

        /// <summary>
        /// Allows you to set the cell's style using the number of the style.
        /// Useful when coping styles from one cell to another.
        /// </summary>
        public int StyleId
        {
            get
            {
                var retValue = 0;
                var sid = _cellElement.GetAttribute("s");
                if (sid != "")
                {
                    retValue = int.Parse(sid);
                }
                return retValue;
            }
            set { _cellElement.SetAttribute("s", value.ToString()); }
        }
        #endregion
        #region ExcelCell Hyperlink
        /// <summary>
        /// Allows you to set/get the cell's Hyperlink
        /// </summary>
        public Uri Hyperlink
        {
            get
            {
                if (_hyperlink != null)
                {
                    return _hyperlink;
                }
                var searchString = string.Format("//d:hyperlinks/d:hyperlink[@ref = '{0}']", CellAddress);
                var linkNode = _cellElement.OwnerDocument.SelectSingleNode(searchString,
                    _xlWorksheet.NameSpaceManager);
                
                if (linkNode == null)
                {
                    return _hyperlink;
                }
                var attr = (XmlAttribute) linkNode.Attributes.GetNamedItem("id", ExcelPackage.schemaRelationships);
                if (attr != null)
                {
                    var relId = attr.Value;
                    // now use the relID to lookup the hyperlink in the relationship table
                    var relationship = _xlWorksheet.Part.GetRelationship(relId);
                    _hyperlink = relationship.TargetUri;
                }
                return _hyperlink;
            }
            set
            {
                _hyperlink = value;
                var linkParent = _cellElement.OwnerDocument.SelectSingleNode("//d:hyperlinks",
                                                                                 _xlWorksheet.NameSpaceManager);
                if (linkParent == null)
                {
                    // create the hyperlinks node
                    linkParent = _cellElement.OwnerDocument.CreateElement("hyperlinks", ExcelPackage.schemaMain);
                    var prevNode = _cellElement.OwnerDocument.SelectSingleNode("//d:conditionalFormatting",
                                                                                   _xlWorksheet.NameSpaceManager);
                    if (prevNode == null)
                    {
                        prevNode = _cellElement.OwnerDocument.SelectSingleNode("//d:mergeCells",
                                                                               _xlWorksheet.NameSpaceManager);
                        if (prevNode == null)
                        {
                            prevNode = _cellElement.OwnerDocument.SelectSingleNode("//d:sheetData",
                                                                                   _xlWorksheet.NameSpaceManager);
                        }
                    }
                    _cellElement.OwnerDocument.DocumentElement.InsertAfter(linkParent, prevNode);
                }

                var searchString = string.Format("./d:hyperlink[@ref = '{0}']", CellAddress);
                var linkNode = (XmlElement) linkParent.SelectSingleNode(searchString, _xlWorksheet.NameSpaceManager);
                if (linkNode == null)
                {
                    linkNode = _cellElement.OwnerDocument.CreateElement("hyperlink", ExcelPackage.schemaMain);
                    // now add cell address attribute
                    linkNode.SetAttribute("ref", CellAddress);
                    linkParent.AppendChild(linkNode);
                }

                var attr = (XmlAttribute) linkNode.Attributes.GetNamedItem("id", ExcelPackage.schemaRelationships);
                if (attr == null)
                {
                    attr = _cellElement.OwnerDocument.CreateAttribute("r", "id", ExcelPackage.schemaRelationships);
                    linkNode.Attributes.Append(attr);
                }
                PackageRelationship relationship;
                var relId = attr.Value;
                if (relId == "")
                {
                    relationship = _xlWorksheet.Part.CreateRelationship(_hyperlink,
                                                                        TargetMode.External,
                                                                        @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink");
                }
                else
                {
                    relationship = _xlWorksheet.Part.GetRelationship(relId);
                    if (relationship.TargetUri != _hyperlink)
                    {
                        relationship = _xlWorksheet.Part.CreateRelationship(_hyperlink,
                                                                            TargetMode.External,
                                                                            @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink");
                    }
                }
                attr.Value = relationship.Id;

                //attr = (XmlAttribute)linkNode.Attributes.GetNamedItem("display", ExcelPackage.schemaMain);
                //if (attr == null)
                //{
                //  attr = _cellNode.OwnerDocument.CreateAttribute("display");
                //  linkNode.Attributes.Append(attr);
                //}
                //attr.Value = Display;
            }
        }
        #endregion
        #region ExcelCell Formula
        /// <summary>
        /// Provides read/write access to the cell's formula.
        /// </summary>
        public string Formula
        {
            get
            {
                if (_formula == null)
                {
                    var formulaNode = _cellElement.SelectSingleNode("./d:f", _xlWorksheet.NameSpaceManager);
                    if (formulaNode != null)
                    {
                        // first check if we have a shared formula
                        var attr = (XmlAttribute) formulaNode.Attributes.GetNamedItem("t");
                        if (attr == null)
                        {
                            // we have a standard formula
                            _formula = formulaNode.InnerText;
                        }
                        else
                        {
                            if (attr.Value == "shared")
                            {
                                // we must obtain the formula from the shared cell reference
                                var refAttr = (XmlAttribute) formulaNode.Attributes.GetNamedItem("si");
                                if (refAttr == null)
                                {
                                    throw new Exception(
                                        "ExcelCell formula marked as shared but no reference ID found (i.e. si attribute)");
                                }

                                var searchString = string.Format("//d:sheetData/d:row/d:c/d:f[@si='{0}']",
                                                                    refAttr.Value);
                                var refNode = _cellElement.OwnerDocument.SelectSingleNode(searchString,
                                                                                              _xlWorksheet.
                                                                                                  NameSpaceManager);
                                if (refNode == null)
                                {
                                    throw new Exception(
                                        "ExcelCell formula marked as shared but no reference node found");
                                }
                                _formula = refNode.InnerText;
                            }
                            else
                            {
                                _formula = formulaNode.InnerText;
                            }
                        }
                    }
                }
                return (_formula);
            }
            set
            {
                // Example cell content for formulas
                // <f>D7</f>
                // <f>SUM(D6:D8)</f>
                // <f>F6+F7+F8</f>
                _formula = value;
                // insert the formula into the cell
                var formulaElement =
                    (XmlElement) _cellElement.SelectSingleNode("./d:f", _xlWorksheet.NameSpaceManager) ??
                    AddFormulaElement();
                // we are setting the formula directly, so remove the shared attributes (if present)
                formulaElement.Attributes.RemoveNamedItem("t", ExcelPackage.schemaMain);
                formulaElement.Attributes.RemoveNamedItem("si", ExcelPackage.schemaMain);

                // set the formula
                formulaElement.InnerText = value;

                // force Excel to re-calculate the cell by removing the value
                RemoveValue();
            }
        }
        #endregion
        #region ExcelCell Comment
        /// <summary>
        /// Returns the comment as a string
        /// </summary>
        public string Comment
        {
            // TODO: implement get which will obtain the text of the comment from the comment1.xml file
            get { throw new Exception("Function not yet implemented!"); }
            // TODO: implement set which will add comments to the worksheet
            // this will require you to add entries to the Drawing.vml file to get this to work! 
        }
        #endregion
        // TODO: conditional formatting
        #endregion  // END Cell Public Properties
        #region ExcelCell Public Methods
        /// <summary>
        /// Removes the XmlNode that holds the cell's value.
        /// Useful when the cell contains a formula as this will force Excel to re-calculate the cell's content.
        /// </summary>
        public void RemoveValue()
        {
            var typeAtt = _cellElement.Attributes["t"];
            if (!ReferenceEquals(typeAtt, null))
            {
                _cellElement.RemoveAttribute("t");
            }
            var valueNode = _cellElement.SelectSingleNode("./d:v", _xlWorksheet.NameSpaceManager);

            if (valueNode != null)
            {
                _cellElement.RemoveChild(valueNode);
            }
        }

        /// <summary>
        /// Returns the cell's value as a string.
        /// </summary>
        /// <returns>The cell's value</returns>
        public override string ToString()
        {
            return Value;
        }
        #endregion  // END Cell Public Methods
        #region ExcelCell Private Methods
        #region IsNumericValue
        private static readonly Regex ObjNotIntPattern = new Regex("[^0-9,.-]", RegexOptions.Compiled);
        private static readonly Regex ObjIntPattern = new Regex("^-[0-9,.]+$|^[0-9,.]+$", RegexOptions.Compiled);

        /// <summary>
        /// Returns true if the string contains a numeric value
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNumericValue(string val)
        {
            return !ReferenceEquals(val, null) && !ObjNotIntPattern.IsMatch(val) &&
                   ObjIntPattern.IsMatch(val);
        }
        #endregion
        #region SharedString methods
        private int SetSharedString(string value)
        {
            //  Assume the string won't be found (assign it an impossible index):
            var index = -1;

            //  Check to see if the string already exists. If so, retrieve its index.
            //  This search is case-sensitive, but Excel stores differently cased
            //  strings separately within the string file.

            var encodedValue = ReferenceEquals(value, null) ? null : value.Trim();
            if (!string.IsNullOrEmpty(encodedValue))
            {
                encodedValue = XmlConvert.EncodeNmToken(encodedValue);
            }
            var stringNode =
                _xlWorksheet.xlPackage.Workbook.SharedStringsXml.SelectSingleNode(
                    string.Format("//d:si[d:t='{0}']", encodedValue), _xlWorksheet.NameSpaceManager);
            if (stringNode == null)
            {
                //  You didn't find the string in the table, so add it now.
                stringNode = _xlWorksheet.xlPackage.Workbook.SharedStringsXml.CreateElement("si",
                    ExcelPackage.schemaMain);
                var textNode = _xlWorksheet.xlPackage.Workbook.SharedStringsXml.CreateElement("t",
                    ExcelPackage.
                        schemaMain);
                textNode.InnerText = value;
                stringNode.AppendChild(textNode);
                _xlWorksheet.xlPackage.Workbook.SharedStringsXml.DocumentElement.AppendChild(stringNode);
            }
            //  Retrieve the index of the selected node.
            //  To do that, count the number of preceding
            //  nodes by retrieving a reference to those nodes.
            var nodes = stringNode.SelectNodes("preceding-sibling::d:si", _xlWorksheet.NameSpaceManager);
            index = nodes.Count;
            return (index);
        }

        private string GetSharedString(int stringId)
        {
            string retValue = null;
            var stringNodes =
                _xlWorksheet.xlPackage.Workbook.SharedStringsXml.SelectNodes(string.Format("//d:si", stringId),
                                                                             _xlWorksheet.NameSpaceManager);
            var stringNode = stringNodes[stringId];
            if (stringNode != null)
            {
                retValue = stringNode.InnerText;
            }
            return (retValue);
        }
        #endregion
        #region AddFormulaNode
        /// <summary>
        /// Adds a new formula node to the cell in the correct location
        /// </summary>
        /// <returns></returns>
        protected internal XmlElement AddFormulaElement()
        {
            var formulaElement = _cellElement.OwnerDocument.CreateElement("f", ExcelPackage.schemaMain);
            // find the right location for insersion
            var valueNode = _cellElement.SelectSingleNode("./d:v", _xlWorksheet.NameSpaceManager);
            if (valueNode == null)
            {
                _cellElement.AppendChild(formulaElement);
            }
            else
            {
                _cellElement.InsertBefore(formulaElement, valueNode);
            }
            return formulaElement;
        }
        #endregion
        #region GetOrCreateCellElement
        private XmlElement GetOrCreateCellElement(ExcelWorksheet xlWorksheet, int row, int col)
        {
            XmlElement cellNode = null;
            // this will create the row if it does not already exist
            var rowNode = xlWorksheet.Row(row).Node;
            if (rowNode != null)
            {
                cellNode =
                    (XmlElement)
                    rowNode.SelectSingleNode(
                        string.Format("./d:c[@" + ExcelWorksheet.tempColumnNumberTag + "='{0}']", col),
                        _xlWorksheet.NameSpaceManager);
                if (cellNode == null)
                {
                    //  Didn't find the cell so create the cell element
                    cellNode = xlWorksheet.WorksheetXml.CreateElement("c", ExcelPackage.schemaMain);
                    cellNode.SetAttribute(ExcelWorksheet.tempColumnNumberTag, col.ToString());
                    //Set style from row
                    {
                        var styleAtt = rowNode.Attributes["s"];
                        if (!ReferenceEquals(styleAtt,null))
                        {
                            cellNode.SetAttribute("s", styleAtt.Value);
                        }
                    }
                    //TODO:Add formatting

                    // You must insert the new cell at the correct location.
                    // Loop through the children, looking for the first cell that is
                    // beyond the cell you're trying to insert. Insert before that cell.
                    XmlNode biggerNode = null;
                    var cellNodes = rowNode.SelectNodes("./d:c", _xlWorksheet.NameSpaceManager);
                    if (cellNodes != null)
                    {
                        foreach (XmlNode node in cellNodes)
                        {
                            XmlNode colNode = node.Attributes[ExcelWorksheet.tempColumnNumberTag];
                            if (colNode != null)
                            {
                                var colFound = Convert.ToInt32(colNode.Value);
                                if (colFound > col)
                                {
                                    biggerNode = node;
                                    break;
                                }
                            }
                        }
                    }
                    if (biggerNode == null)
                    {
                        rowNode.AppendChild(cellNode);
                    }
                    else
                    {
                        rowNode.InsertBefore(cellNode, biggerNode);
                    }
                }
            }
            return (cellNode);
        }
        #endregion
        #endregion // END Cell Private Methods
        #region ExcelCell Static Cell Address Manipulation Routines
        #region GetColumnLetter
        /// <summary>
        /// Returns the character representation of the numbered column
        /// </summary>
        /// <param name="iColumnNumber">The number of the column</param>
        /// <returns>The letter representing the column</returns>
        protected internal static string GetColumnLetter(int iColumnNumber)
        {
            int iMainLetterUnicode;
            char iMainLetterChar;

            // TODO: we need to cater for columns larger than ZZ
            if (iColumnNumber > 26)
            {
                int iFirstLetterUnicode; // default
                var iFirstLetter = Convert.ToInt32(iColumnNumber/26);
                if (Convert.ToDouble(iFirstLetter) == (Convert.ToDouble(iColumnNumber)/26))
                {
                    iFirstLetterUnicode = iFirstLetter - 1 + 64;
                    iMainLetterChar = 'Z';
                }
                else
                {
                    iFirstLetterUnicode = iFirstLetter + 64;
                    iMainLetterUnicode = (iColumnNumber - (iFirstLetter*26)) + 64;
                    iMainLetterChar = (char) iMainLetterUnicode;
                }
                var iFirstLetterChar = (char) iFirstLetterUnicode;

                return (iFirstLetterChar + iMainLetterChar.ToString());
            }
            // if we get here we only have a single letter to return
            iMainLetterUnicode = 64 + iColumnNumber;
            iMainLetterChar = (char) iMainLetterUnicode;
            return (iMainLetterChar.ToString());
        }
        #endregion
        #region GetColumnNumber
        /// <summary>
        /// Returns the column number from the cellAddress
        /// e.g. D5 would return 5
        /// </summary>
        /// <param name="cellAddress">An Excel format cell addresss (e.g. D5)</param>
        /// <returns>The column number</returns>
        public static int GetColumnNumber(string cellAddress)
        {
            // find out position where characters stop and numbers begin
            var iColumnNumber = 0;
            var iPos = 0;
            var found = false;
            foreach (var chr in cellAddress)
            {
                iPos++;
                if (!char.IsNumber(chr))
                {
                    continue;
                }
                found = true;
                break;
            }

            if (!found)
            {
                return iColumnNumber;
            }
            var alphaPart = cellAddress.Substring(0, cellAddress.Length - (cellAddress.Length + 1 - iPos));

            var length = alphaPart.Length;
            var count = 0;
            foreach (var chr in alphaPart)
            {
                count++;
                var chrValue = (chr - 64);
                switch (length)
                {
                    case 1:
                        iColumnNumber = chrValue;
                        break;
                    case 2:
                        if (count == 1)
                        {
                            iColumnNumber += (chrValue*26);
                        }
                        else
                        {
                            iColumnNumber += chrValue;
                        }
                        break;
                    case 3:
                        if (count == 1)
                        {
                            iColumnNumber += (chrValue*26*26);
                        }
                        if (count == 2)
                        {
                            iColumnNumber += (chrValue*26);
                        }
                        else
                        {
                            iColumnNumber += chrValue;
                        }
                        break;
                    case 4:
                        if (count == 1)
                        {
                            iColumnNumber += (chrValue*26*26*26);
                        }
                        if (count == 2)
                        {
                            iColumnNumber += (chrValue*26*26);
                        }
                        if (count == 3)
                        {
                            iColumnNumber += (chrValue*26);
                        }
                        else
                        {
                            iColumnNumber += chrValue;
                        }
                        break;
                }
            }
            return iColumnNumber;
        }
        #endregion
        #region GetRowNumber
        /// <summary>
        /// Returns the row number from the cellAddress
        /// e.g. D5 would return 5
        /// </summary>
        /// <param name="cellAddress">An Excel format cell addresss (e.g. D5)</param>
        /// <returns>The row number</returns>
        public static int GetRowNumber(string cellAddress)
        {
            // find out position where characters stop and numbers begin
            var iPos = 0;
            var found = false;
            foreach (var chr in cellAddress)
            {
                iPos++;
                if (char.IsNumber(chr))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                return 0;
            }
            var numberPart = cellAddress.Substring(iPos - 1, cellAddress.Length - (iPos - 1));
            return IsNumericValue(numberPart) ? int.Parse(numberPart) : 0;
        }
        #endregion
        #region GetCellAddress
        /// <summary>
        /// Returns the AlphaNumeric representation that Excel expects for a Cell Address
        /// </summary>
        /// <param name="iRow">The number of the row</param>
        /// <param name="iColumn">The number of the column in the worksheet</param>
        /// <returns>The cell address in the format A1</returns>
        public static string GetCellAddress(int iRow, int iColumn)
        {
            return (GetColumnLetter(iColumn) + iRow);
        }
        #endregion
        #region IsValidCellAddress
        /// <summary>
        /// Checks that a cell address (e.g. A5) is valid.
        /// </summary>
        /// <param name="cellAddress">The alphanumeric cell address</param>
        /// <returns>True if the cell address is valid</returns>
        public static bool IsValidCellAddress(string cellAddress)
        {
            var row = GetRowNumber(cellAddress);
            var col = GetColumnNumber(cellAddress);

            if (GetCellAddress(row, col) == cellAddress)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        #endregion
        #region UpdateFormulaReferences
        /// <summary>
        /// Updates the Excel formula so that all the cellAddresses are incremented by the row and column increments
        /// if they fall after the afterRow and afterColumn.
        /// Supports inserting rows and columns into existing templates.
        /// </summary>
        /// <param name="formula">The Excel formula</param>
        /// <param name="rowIncrement">The amount to increment the cell reference by</param>
        /// <param name="colIncrement">The amount to increment the cell reference by</param>
        /// <param name="afterRow">Only change rows after this row</param>
        /// <param name="afterColumn">Only change columns after this column</param>
        /// <returns></returns>
        public static string UpdateFormulaReferences(string formula, int rowIncrement, int colIncrement, int afterRow,
                                                     int afterColumn)
        {
            var newFormula = "";

            var getAlphaNumeric = new Regex(@"[^a-zA-Z0-9]", RegexOptions.IgnoreCase);
            var getSigns = new Regex(@"[a-zA-Z0-9]", RegexOptions.IgnoreCase);

            var alphaNumeric = getAlphaNumeric.Replace(formula, " ").Replace("  ", " ");
            var signs = getSigns.Replace(formula, " ");
            var chrSigns = signs.ToCharArray();
            var count = 0;
            var length = 0;
            foreach (var cellAddress in alphaNumeric.Split(' '))
            {
                count++;
                length += cellAddress.Length;

                // if the cellAddress contains an alpha part followed by a number part, then it is a valid cellAddress
                var row = GetRowNumber(cellAddress);
                var col = GetColumnNumber(cellAddress);
                var newCellAddress = "";
                if (GetCellAddress(row, col) == cellAddress) // this checks if the cellAddress is valid
                {
                    // we have a valid cell address so change its value (if necessary)
                    if (row >= afterRow)
                    {
                        row += rowIncrement;
                    }
                    if (col >= afterColumn)
                    {
                        col += colIncrement;
                    }
                    newCellAddress = GetCellAddress(row, col);
                }
                if (newCellAddress == "")
                {
                    newFormula += cellAddress;
                }
                else
                {
                    newFormula += newCellAddress;
                }

                for (var i = length; i < signs.Length; i++)
                {
                    if (chrSigns[i] == ' ')
                    {
                        break;
                    }
                    if (chrSigns[i] != ' ')
                    {
                        length++;
                        newFormula += chrSigns[i].ToString();
                    }
                }
            }
            return (newFormula);
        }
        #endregion
        #endregion // END CellAddress Manipulation Routines
    }
}