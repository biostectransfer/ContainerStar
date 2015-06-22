using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Xml;

namespace MetadataLoader.Excel.OfficeOpenXML
{
    /// <summary>
    /// Represents an Excel worksheet and provides access to its properties and methods
    /// </summary>
    public sealed class ExcelWorksheet
    {
        #region Worksheet Private Properties
        /// <summary>
        /// Temporary tag for all column numbers in the worksheet XML
        /// For internal use only!
        /// </summary>
        internal const string tempColumnNumberTag = "colNumber";
        /// <summary>
        /// Reference to the parent package
        /// For internal use only!
        /// </summary>
        internal ExcelPackage xlPackage;
        private readonly Uri _worksheetUri;
        private readonly int _sheetID;
        private readonly string _relationshipID;
        private string _name;

        private bool _hidden;

        private XmlDocument _worksheetXml;
        private ExcelWorksheetView _sheetView;
        private ExcelHeaderFooter _headerFooter;
        private XmlNamespaceManager _nsManager;
        #endregion  // END Worksheet Private Properties
        #region Constructor
        /// <summary>
        /// Creates a new instance of ExcelWorksheet class. 
        /// For internal use only!
        /// </summary>
        /// <param name="ParentXlPackage">Parent ExcelPackage object</param>
        /// <param name="RelationshipID">Package relationship ID</param>
        /// <param name="sheetName">Name of the new worksheet</param>
        /// <param name="uriWorksheet">Uri of the worksheet in the package</param>
        /// <param name="SheetID">The worksheet's ID in the workbook XML</param>
        /// <param name="Hide">Indicates if the worksheet is hidden</param>
        internal ExcelWorksheet(ExcelPackage ParentXlPackage,
                                string RelationshipID,
                                string sheetName,
                                Uri uriWorksheet,
                                int SheetID,
                                bool Hide)
        {
            xlPackage = ParentXlPackage;
            _relationshipID = RelationshipID;
            _worksheetUri = uriWorksheet;
            _name = sheetName;
            //_type = Type;
            _sheetID = SheetID;
            Hidden = Hide;
        }
        #endregion
        #region Worksheet Public Properties
        /// <summary>
        /// Read-only: the Uri to the worksheet within the package
        /// </summary>
        internal Uri WorksheetUri
        {
            get { return (_worksheetUri); }
        }
        /// <summary>
        /// Read-only: a reference to the PackagePart for the worksheet within the package
        /// </summary>
        internal PackagePart Part
        {
            get { return (xlPackage.Package.GetPart(WorksheetUri)); }
        }
        /// <summary>
        /// Read-only: the ID for the worksheet's relationship with the workbook in the package
        /// </summary>
        internal string RelationshipID
        {
            get { return (_relationshipID); }
        }
        /// <summary>
        /// The unique identifier for the worksheet.  Note that these can be random, so not
        /// too useful in code!
        /// </summary>
        internal int SheetID
        {
            get { return (_sheetID); }
        }
        /// <summary>
        /// Provides access to a namespace manager instance to allow XPath searching
        /// </summary>
        public XmlNamespaceManager NameSpaceManager
        {
            get
            {
                if (_nsManager == null)
                {
                    var nt = new NameTable();
                    _nsManager = new XmlNamespaceManager(nt);
                    _nsManager.AddNamespace("d", ExcelPackage.schemaMain);
                }
                return (_nsManager);
            }
        }
        /// <summary>
        /// Returns a ExcelWorksheetView object that allows you to
        /// set the view state properties of the worksheet
        /// </summary>
        public ExcelWorksheetView View
        {
            get
            {
                if (_sheetView == null)
                {
                    _sheetView = new ExcelWorksheetView(this);
                }
                return (_sheetView);
            }
        }
        #region Name // Worksheet Name
        /// <summary>
        /// The worksheet's name as it appears on the tab
        /// </summary>
        public string Name
        {
            get { return (_name); }
            set
            {
                var sheetNode =
                    xlPackage.Workbook.WorkbookXml.SelectSingleNode(string.Format("//d:sheet[@sheetId={0}]", _sheetID),
                                                                    NameSpaceManager);
                if (sheetNode != null)
                {
                    var nameAttr = (XmlAttribute) sheetNode.Attributes.GetNamedItem("name");
                    if (nameAttr != null)
                    {
                        nameAttr.Value = value;
                    }
                }
                _name = value;
            }
        }
        #endregion // END Worksheet Name
        #region Hidden
        /// <summary>
        /// Indicates if the worksheet is hidden in the workbook
        /// </summary>
        public bool Hidden
        {
            get { return (_hidden); }
            set
            {
                var sheetNode =
                    xlPackage.Workbook.WorkbookXml.SelectSingleNode(string.Format("//d:sheet[@sheetId={0}]", _sheetID),
                                                                    NameSpaceManager);
                if (sheetNode != null)
                {
                    var nameAttr = (XmlAttribute) sheetNode.Attributes.GetNamedItem("hidden");
                    if (nameAttr != null)
                    {
                        nameAttr.Value = value.ToString();
                    }
                }
                _hidden = value;
            }
        }
        #endregion
        #region defaultRowHeight
        /// <summary>
        /// Allows you to get/set the default height of all rows in the worksheet
        /// </summary>
        public int defaultRowHeight
        {
            get
            {
                var retValue = 15; // Excel's default height
                var sheetFormat =
                    (XmlElement) WorksheetXml.SelectSingleNode("//d:sheetFormatPr", NameSpaceManager);
                if (sheetFormat != null)
                {
                    var ret = sheetFormat.GetAttribute("defaultRowHeight");
                    if (ret != "")
                    {
                        retValue = int.Parse(ret);
                    }
                }
                return retValue;
            }
            set
            {
                var sheetFormat =
                    (XmlElement) WorksheetXml.SelectSingleNode("//d:sheetFormatPr", NameSpaceManager);
                if (sheetFormat == null)
                {
                    // create the node as it does not exist
                    sheetFormat = WorksheetXml.CreateElement("sheetFormatPr", ExcelPackage.schemaMain);
                    // find location to insert new element
                    var sheetViews = WorksheetXml.SelectSingleNode("//d:sheetViews", NameSpaceManager);
                    // insert the new node
                    WorksheetXml.DocumentElement.InsertAfter(sheetFormat, sheetViews);
                }
                sheetFormat.SetAttribute("defaultRowHeight", value.ToString());
            }
        }
        #endregion
        #region WorksheetXml
        /// <summary>
        /// The XML document holding all the worksheet data.
        /// </summary>
        public XmlDocument WorksheetXml
        {
            get
            {
                if (_worksheetXml == null)
                {
                    _worksheetXml = new XmlDocument();
                    var packPart = xlPackage.Package.GetPart(WorksheetUri);
                    _worksheetXml.Load(packPart.GetStream());
                    // convert worksheet into the type of XML we like dealing with
                    AddNumericCellIDs(_worksheetXml);
                }
                return (_worksheetXml);
            }
        }
        #endregion
        #region HeaderFooter
        /// <summary>
        /// A reference to the header and footer class which allows you to 
        /// set the header and footer for all odd, even and first pages of the worksheet
        /// </summary>
        public ExcelHeaderFooter HeaderFooter
        {
            get
            {
                if (_headerFooter == null)
                {
                    var headerFooterNode = WorksheetXml.SelectSingleNode("//d:headerFooter", NameSpaceManager) ??
                                               WorksheetXml.DocumentElement.AppendChild(WorksheetXml.CreateElement("headerFooter",ExcelPackage.schemaMain));
                    _headerFooter = new ExcelHeaderFooter((XmlElement) headerFooterNode);
                }
                return (_headerFooter);
            }
        }
        #endregion
        // TODO: implement freeze pane. 
        // TODO: implement page margin properties
        #endregion // END Worksheet Public Properties
        #region Worksheet Public Methods
        /// <summary>
        /// Provides access to an individual cell within the worksheet.
        /// </summary>
        /// <param name="row">The row number in the worksheet</param>
        /// <param name="col">The column number in the worksheet</param>
        /// <returns></returns>
        public ExcelCell Cell(int row, int col)
        {
            return (new ExcelCell(this, row, col));
        }

        /// <summary>
        /// Provides access to an individual row within the worksheet so you can set its properties.
        /// </summary>
        /// <param name="row">The row number in the worksheet</param>
        /// <returns></returns>
        public ExcelRow Row(int row)
        {
            return (new ExcelRow(this, row));
        }

        /// <summary>
        /// Provides access to an individual column within the worksheet so you can set its properties.
        /// </summary>
        /// <param name="col">The column number in the worksheet</param>
        /// <returns></returns>
        public ExcelColumn Column(int col)
        {
            return (new ExcelColumn(this, col));
        }
        #region CreateSharedFormula
        /// <summary>
        /// Creates a shared formula based on the formula already in startCell
        /// Essentially this supports the formula attributes such as t="shared" ref="B2:B4" si="0"
        /// as per Brian Jones: Open XML Formats blog. See
        /// http://blogs.msdn.com/brian_jones/archive/2006/11/15/simple-spreadsheetml-file-part-2-of-3.aspx
        /// </summary>
        /// <param name="startCell">The cell containing the formula</param>
        /// <param name="endCell">The end cell (i.e. end of the range)</param>
        public void CreateSharedFormula(ExcelCell startCell, ExcelCell endCell)
        {
            XmlElement formulaElement;
            var formula = startCell.Formula;
            if (formula == "")
            {
                throw new Exception("CreateSharedFormula Error: startCell does not contain a formula!");
            }

            // find or create a shared formula ID
            var sharedID = -1;
            foreach (XmlNode node in WorksheetXml.SelectNodes("//d:sheetData/d:row/d:c/d:f/@si", NameSpaceManager))
            {
                var curID = int.Parse(node.Value);
                if (curID > sharedID)
                {
                    sharedID = curID;
                }
            }
            sharedID++; // first value must be zero

            for (var row = startCell.Row; row <= endCell.Row; row++)
            {
                for (var col = startCell.Column; col <= endCell.Column; col++)
                {
                    var cell = Cell(row, col);

                    // to force Excel to re-calculate the formula, we must remove the value
                    cell.RemoveValue();

                    formulaElement = (XmlElement) cell.Element.SelectSingleNode("./d:f", NameSpaceManager);
                    if (formulaElement == null)
                    {
                        formulaElement = cell.AddFormulaElement();
                    }
                    formulaElement.SetAttribute("t", "shared");
                    formulaElement.SetAttribute("si", sharedID.ToString());
                }
            }

            // finally add the shared cell range to the startCell
            formulaElement = (XmlElement) startCell.Element.SelectSingleNode("./d:f", NameSpaceManager);
            formulaElement.SetAttribute("ref", string.Format("{0}:{1}", startCell.CellAddress, endCell.CellAddress));
        }
        #endregion
        /// <summary>
        /// Inserts conditional formatting for the cell range.
        /// Currently only supports the dataBar style.
        /// </summary>
        /// <param name="startCell"></param>
        /// <param name="endCell"></param>
        /// <param name="color"></param>
        public void CreateConditionalFormatting(ExcelCell startCell, ExcelCell endCell, string color)
        {
            var formatNode = WorksheetXml.SelectSingleNode("//d:conditionalFormatting", NameSpaceManager);
            if (formatNode == null)
            {
                formatNode = WorksheetXml.CreateElement("conditionalFormatting", ExcelPackage.schemaMain);
                var prevNode = WorksheetXml.SelectSingleNode("//d:mergeCells", NameSpaceManager) ??
                                   WorksheetXml.SelectSingleNode("//d:sheetData", NameSpaceManager);

                WorksheetXml.DocumentElement.InsertAfter(formatNode, prevNode);
            }
            var attr = formatNode.Attributes["sqref"];
            if (attr == null)
            {
                attr = WorksheetXml.CreateAttribute("sqref");
                formatNode.Attributes.Append(attr);
            }
            attr.Value = string.Format("{0}:{1}", startCell.CellAddress, endCell.CellAddress);

            var node = formatNode.SelectSingleNode("./d:cfRule", NameSpaceManager);
            if (node == null)
            {
                node = WorksheetXml.CreateElement("cfRule", ExcelPackage.schemaMain);
                formatNode.AppendChild(node);
            }

            attr = node.Attributes["type"];
            if (attr == null)
            {
                attr = WorksheetXml.CreateAttribute("type");
                node.Attributes.Append(attr);
            }
            attr.Value = "dataBar";

            attr = node.Attributes["priority"];
            if (attr == null)
            {
                attr = WorksheetXml.CreateAttribute("priority");
                node.Attributes.Append(attr);
            }
            attr.Value = "1";

            // the following is poor code, but just an example!!!
            XmlNode databar = WorksheetXml.CreateElement("databar", ExcelPackage.schemaMain);
            node.AppendChild(databar);

            XmlNode child = WorksheetXml.CreateElement("cfvo", ExcelPackage.schemaMain);
            databar.AppendChild(child);
            attr = WorksheetXml.CreateAttribute("type");
            child.Attributes.Append(attr);
            attr.Value = "min";
            attr = WorksheetXml.CreateAttribute("val");
            child.Attributes.Append(attr);
            attr.Value = "0";

            child = WorksheetXml.CreateElement("cfvo", ExcelPackage.schemaMain);
            databar.AppendChild(child);
            attr = WorksheetXml.CreateAttribute("type");
            child.Attributes.Append(attr);
            attr.Value = "max";
            attr = WorksheetXml.CreateAttribute("val");
            child.Attributes.Append(attr);
            attr.Value = "0";

            child = WorksheetXml.CreateElement("color", ExcelPackage.schemaMain);
            databar.AppendChild(child);
            attr = WorksheetXml.CreateAttribute("rgb");
            child.Attributes.Append(attr);
            attr.Value = color;
        }
        #region InsertRow
        /// <summary>
        /// Inserts a new row into the spreadsheet.  Existing rows below the insersion position are 
        /// shifted down.  All formula are updated to take account of the new row.
        /// </summary>
        /// <param name="position">The position of the new row</param>
        public void InsertRow(int position)
        {
            // create the new row element
            var rowElement = WorksheetXml.CreateElement("row", ExcelPackage.schemaMain);
            rowElement.Attributes.Append(WorksheetXml.CreateAttribute("r"));
            rowElement.Attributes["r"].Value = position.ToString();

            var sheetDataNode = WorksheetXml.SelectSingleNode("//d:sheetData", NameSpaceManager);
            if (sheetDataNode != null)
            {
                var renumberFrom = 1;
                var nodes = sheetDataNode.ChildNodes;
                var nodeCount = nodes.Count;
                XmlNode insertAfterRowNode = null;
                var insertAfterRowNodeID = 0;
                for (var i = 0; i < nodeCount; i++)
                {
                    var currentRowID = int.Parse(nodes[i].Attributes["r"].Value);
                    if (currentRowID < position)
                    {
                        insertAfterRowNode = nodes[i];
                        insertAfterRowNodeID = i;
                    }
                    if (currentRowID >= position)
                    {
                        renumberFrom = currentRowID;
                        break;
                    }
                }

                // update the existing row ids
                for (var i = insertAfterRowNodeID + 1; i < nodeCount; i++)
                {
                    var currentRowID = int.Parse(nodes[i].Attributes["r"].Value);
                    if (currentRowID >= renumberFrom)
                    {
                        nodes[i].Attributes["r"].Value = Convert.ToString(currentRowID + 1);

                        // now update any formula that are in the row 
                        var formulaNodes = nodes[i].SelectNodes("./d:c/d:f", NameSpaceManager);
                        foreach (XmlNode formulaNode in formulaNodes)
                        {
                            formulaNode.InnerText = ExcelCell.UpdateFormulaReferences(formulaNode.InnerText,
                                                                                      1,
                                                                                      0,
                                                                                      position,
                                                                                      0);
                        }
                    }
                }

                // now insert the new row
                if (insertAfterRowNode != null)
                {
                    sheetDataNode.InsertAfter(rowElement, insertAfterRowNode);
                }
            }
        }
        #endregion
        #region DeleteRow
        /// <summary>
        /// Deletes the specified row from the worksheet.
        /// If shiftOtherRowsUp=true then all formula are updated to take account of the deleted row.
        /// </summary>
        /// <param name="rowToDelete">The number of the row to be deleted</param>
        /// <param name="shiftOtherRowsUp">Set to true if you want the other rows renumbered so they all move up</param>
        public void DeleteRow(int rowToDelete, bool shiftOtherRowsUp)
        {
            var sheetDataNode = WorksheetXml.SelectSingleNode("//d:sheetData", NameSpaceManager);
            if (sheetDataNode != null)
            {
                var nodes = sheetDataNode.ChildNodes;
                var nodeCount = nodes.Count;
                var rowNodeID = 0;
                XmlNode rowNode = null;
                for (var i = 0; i < nodeCount; i++)
                {
                    var currentRowID = int.Parse(nodes[i].Attributes["r"].Value);
                    if (currentRowID == rowToDelete)
                    {
                        rowNodeID = i;
                        rowNode = nodes[i];
                    }
                }

                if (shiftOtherRowsUp)
                {
                    // update the existing row ids
                    for (var i = rowNodeID + 1; i < nodeCount; i++)
                    {
                        var currentRowID = int.Parse(nodes[i].Attributes["r"].Value);
                        if (currentRowID > rowToDelete)
                        {
                            nodes[i].Attributes["r"].Value = Convert.ToString(currentRowID - 1);

                            // now update any formula that are in the row 
                            var formulaNodes = nodes[i].SelectNodes("./d:c/d:f", NameSpaceManager);
                            foreach (XmlNode formulaNode in formulaNodes)
                            {
                                formulaNode.InnerText = ExcelCell.UpdateFormulaReferences(formulaNode.InnerText,
                                                                                          -1,
                                                                                          0,
                                                                                          rowToDelete,
                                                                                          0);
                            }
                        }
                    }
                }
                // delete the row
                if (rowNode != null)
                {
                    sheetDataNode.RemoveChild(rowNode);
                }
            }
        }
        #endregion
        #endregion // END Worksheet Public Methods
        #region Worksheet Private Methods
        #region Worksheet Save
        /// <summary>
        /// Saves the worksheet to the package.  For internal use only.
        /// </summary>
        internal void Save() // Worksheet Save
        {
            #region Delete the printer settings component (if it exists)
            // we also need to delete the relationship from the pageSetup tag
            var pageSetup = WorksheetXml.SelectSingleNode("//d:pageSetup", NameSpaceManager);
            if (pageSetup != null)
            {
                var attr =
                    (XmlAttribute) pageSetup.Attributes.GetNamedItem("id", ExcelPackage.schemaRelationships);
                if (attr != null)
                {
                    var relID = attr.Value;
                    // first delete the attribute from the XML
                    pageSetup.Attributes.Remove(attr);

                    // get the URI
                    var relPrinterSettings = Part.GetRelationship(relID);
                    var printerSettingsUri = new Uri("/xl" + relPrinterSettings.TargetUri.ToString().Replace("..", ""),
                                                     UriKind.Relative);

                    // now delete the relationship
                    Part.DeleteRelationship(relPrinterSettings.Id);

                    // now delete the part from the package
                    xlPackage.Package.DeletePart(printerSettingsUri);
                }
            }
            #endregion
            // save the header & footer (if defined)
            if (_headerFooter != null)
            {
                HeaderFooter.Save();
            }
            // replace the numeric Cell IDs we inserted with AddNumericCellIDs()
            ReplaceNumericCellIDs();

            // save worksheet to package
            var partPack = xlPackage.Package.GetPart(WorksheetUri);
            WorksheetXml.Save(partPack.GetStream(FileMode.Create, FileAccess.Write));
            xlPackage.WriteDebugFile(WorksheetXml, @"xl\worksheets", "sheet" + SheetID + ".xml");

            //clear field _worksheetXml becouse need rebuild column number tag in columns
            _worksheetXml = null;
        }
        #endregion
        #region AddNumericCellIDs
        /// <summary>
        /// Adds numeric cell identifiers so that it is easier to work out position of cells
        /// Private method, for internal use only!
        /// </summary>
        private void AddNumericCellIDs(XmlDocument worksheetDoc)
        {
            // process each row
            foreach (XmlNode rowNode in worksheetDoc.SelectNodes("//d:sheetData/d:row", NameSpaceManager))
            {
                // remove the spans attribute.  Excel simply recreates it when the file is opened.
                var attr = (XmlAttribute) rowNode.Attributes.GetNamedItem("spans");
                //if (attr != null)
                //{
                //    rowNode.Attributes.Remove(attr);
                //}

                //int row = Convert.ToInt32(rowNode.Attributes.GetNamedItem("r").Value);
                // process each cell in current row
                foreach (XmlNode colNode in rowNode.SelectNodes("./d:c", NameSpaceManager))
                {
                    var cellAddressAttr = (XmlAttribute) colNode.Attributes.GetNamedItem("r");
                    if (cellAddressAttr != null)
                    {
                        var cellAddress = cellAddressAttr.Value;

                        var col = ExcelCell.GetColumnNumber(cellAddress);
                        attr = worksheetDoc.CreateAttribute(tempColumnNumberTag);
                        if (attr != null)
                        {
                            attr.Value = col.ToString();
                            colNode.Attributes.Append(attr);
                            // remove all cell Addresses like A1, A2, A3 etc.
                            colNode.Attributes.Remove(cellAddressAttr);
                        }
                    }
                }
            }
        }
        #endregion
        #region ReplaceNumericCellIDs
        /// <summary>
        /// Replaces the numeric cell identifiers we inserted with AddNumericCellIDs with the traditional 
        /// A1, A2 cell identifiers that Excel understands.
        /// Private method, for internal use only!
        /// </summary>
        private void ReplaceNumericCellIDs()
        {
            var maxColumn = 0;
            // process each row
            foreach (XmlNode rowNode in WorksheetXml.SelectNodes("//d:sheetData/d:row", NameSpaceManager))
            {
                var row = Convert.ToInt32(rowNode.Attributes.GetNamedItem("r").Value);
                var count = 0;
                // process each cell in current row
                foreach (XmlNode colNode in rowNode.SelectNodes("./d:c", NameSpaceManager))
                {
                    var colNumber = (XmlAttribute) colNode.Attributes.GetNamedItem(tempColumnNumberTag);
                    if (colNumber != null)
                    {
                        count++;
                        if (count > maxColumn)
                        {
                            maxColumn = count;
                        }
                        var col = Convert.ToInt32(colNumber.Value);
                        var cellAddress = ExcelCell.GetColumnLetter(col) + row;
                        var attr = WorksheetXml.CreateAttribute("r");
                        if (attr != null)
                        {
                            attr.Value = cellAddress;
                            // the cellAddress needs to be the first attribute, otherwise Excel complains
                            if (colNode.Attributes.Count == 0)
                            {
                                colNode.Attributes.Append(attr);
                            }
                            else
                            {
                                colNode.Attributes.InsertBefore(attr, (XmlAttribute) colNode.Attributes.Item(0));
                            }
                        }
                        // remove all numeric cell addresses added by AddNumericCellIDs
                        colNode.Attributes.Remove(colNumber);
                    }
                }
            }

            // process each row and add the spans attribute
            // TODO: Need to add proper spans handling.
            //foreach (XmlNode rowNode in XmlDoc.SelectNodes("//d:sheetData/d:row", NameSpaceManager))
            //{
            //  // we must add or update the "spans" attribute of each row
            //  XmlAttribute spans = (XmlAttribute)rowNode.Attributes.GetNamedItem("spans");
            //  if (spans == null)
            //  {
            //    spans = XmlDoc.CreateAttribute("spans");
            //    rowNode.Attributes.Append(spans);
            //  }
            //  spans.Value = "1:" + maxColumn.ToString();
            //}
        }
        #endregion
        #region Get Style Information
        /// <summary>
        /// Returns the name of the style using its xfId
        /// </summary>
        /// <param name="StyleID">The xfId of the style</param>
        /// <returns>The name of the style</returns>
        internal string GetStyleName(int StyleID)
        {
            string retValue = null;
            XmlNode styleNode = null;
            var count = 0;
            foreach (XmlNode node in xlPackage.Workbook.StylesXml.SelectNodes("//d:cellXfs/d:xf", NameSpaceManager))
            {
                if (count == StyleID)
                {
                    styleNode = node;
                    break;
                }
                count++;
            }

            if (styleNode != null)
            {
                var searchString = string.Format("//d:cellStyle[@xfId = '{0}']", styleNode.Attributes["xfId"].Value);
                var styleNameNode = xlPackage.Workbook.StylesXml.SelectSingleNode(searchString, NameSpaceManager);
                if (styleNameNode != null)
                {
                    retValue = styleNameNode.Attributes["name"].Value;
                }
            }

            return retValue;
        }

        /// <summary>
        /// Returns the style ID given a style name.  
        /// The style ID will be created if not found, but only if the style name exists!
        /// </summary>
        /// <param name="StyleName"></param>
        /// <returns></returns>
        internal int GetStyleID(string StyleName)
        {
            var styleID = 0;
            // find the named style in the style sheet
            var searchString = string.Format("//d:cellStyle[@name = '{0}']", StyleName);
            var styleNameNode = xlPackage.Workbook.StylesXml.SelectSingleNode(searchString, NameSpaceManager);
            if (styleNameNode != null)
            {
                var xfId = styleNameNode.Attributes["xfId"].Value;
                // look up position of style in the cellXfs 
                searchString = string.Format("//d:cellXfs/d:xf[@xfId = '{0}']", xfId);
                var styleNode = xlPackage.Workbook.StylesXml.SelectSingleNode(searchString, NameSpaceManager);
                if (styleNode != null)
                {
                    var nodes = styleNode.SelectNodes("preceding-sibling::d:xf", NameSpaceManager);
                    if (nodes != null)
                    {
                        styleID = nodes.Count;
                    }
                }
            }
            return styleID;
        }
        #endregion
        #endregion  // END Worksheet Private Methods
        #region Extra methods //Added by VFofanov
        public IEnumerable<ExcelCell> GetUsedCells()
        {
            // process each row
            foreach (XmlNode rowNode in WorksheetXml.SelectNodes("//d:sheetData/d:row", NameSpaceManager))
            {
                var row = Convert.ToInt32(rowNode.Attributes.GetNamedItem("r").Value);

                // process each cell in current row
                foreach (XmlNode colNode in rowNode.SelectNodes("./d:c", NameSpaceManager))
                {
                    var column = Convert.ToInt32(colNode.Attributes[tempColumnNumberTag].Value);
                    yield return new ExcelCell(this, row, column);
                }
            }
        }
        #endregion
    } // END class Worksheet
}