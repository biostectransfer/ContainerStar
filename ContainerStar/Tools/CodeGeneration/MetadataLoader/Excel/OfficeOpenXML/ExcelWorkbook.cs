using System;
using System.IO;
using System.IO.Packaging;
using System.Xml;

namespace MetadataLoader.Excel.OfficeOpenXML
{
    #region Public Enum ExcelCalcMode
    /// <summary>
    /// Represents the possible workbook calculation modes
    /// </summary>
    public enum ExcelCalcMode
    {
        /// <summary>
        /// Set the calculation mode to Automatic
        /// </summary>
        Automatic,
        /// <summary>
        /// Set the calculation mode to AutomaticNoTable
        /// </summary>
        AutomaticNoTable,
        /// <summary>
        /// Set the calculation mode to Manual
        /// </summary>
        Manual
    }
    #endregion
    /// <summary>
    /// Represents the Excel workbook and provides access to all the 
    /// document properties and worksheets within the workbook.
    /// </summary>
    public class ExcelWorkbook
    {
        #region Private Properties
        private readonly ExcelPackage _xlPackage;
        // we have to hard code these uris as we need them to create a workbook from scratch
        private static readonly Uri _uriWorkbook = new Uri("/xl/workbook.xml", UriKind.Relative);
        private static readonly Uri _uriSharedStrings = new Uri("/xl/sharedStrings.xml", UriKind.Relative);
        private static readonly Uri _uriStyles = new Uri("/xl/styles.xml", UriKind.Relative);
        private static readonly Uri _uriCalcChain = new Uri("/xl/calcChain.xml", UriKind.Relative);

        private XmlDocument _xmlWorkbook;
        private XmlDocument _xmlSharedStrings;
        private XmlDocument _xmlStyles;

        private ExcelWorksheets _worksheets;
        private readonly XmlNamespaceManager _nsManager;
        private OfficeProperties _properties;
        #endregion
        #region ExcelWorkbook Constructor
        /// <summary>
        /// Creates a new instance of the ExcelWorkbook class.  For internal use only!
        /// </summary>
        /// <param name="xlPackage">The parent package</param>
        protected internal ExcelWorkbook(ExcelPackage xlPackage)
        {
            _xlPackage = xlPackage;
            //  Create a NamespaceManager to handle the default namespace, 
            //  and create a prefix for the default namespace:
            var nt = new NameTable();
            _nsManager = new XmlNamespaceManager(nt);
            _nsManager.AddNamespace("d", ExcelPackage.schemaMain);
        }
        #endregion
        #region Worksheets
        /// <summary>
        /// Provides access to all the worksheets in the workbook.
        /// </summary>
        public ExcelWorksheets Worksheets
        {
            get
            {
                if (_worksheets == null)
                {
                    _worksheets = new ExcelWorksheets(_xlPackage);
                }
                return _worksheets;
            }
        }
        #endregion
        #region Workbook Properties
        /// <summary>
        /// The Uri to the workbook in the package
        /// </summary>
        protected internal Uri WorkbookUri
        {
            get { return (_uriWorkbook); }
        }
        /// <summary>
        /// The Uri to the styles.xml in the package
        /// </summary>
        protected internal Uri StylesUri
        {
            get { return (_uriStyles); }
        }
        /// <summary>
        /// The Uri to the shared strings file
        /// </summary>
        protected internal Uri SharedStringsUri
        {
            get { return (_uriSharedStrings); }
        }
        /// <summary>
        /// Returns a reference to the workbook's part within the package
        /// </summary>
        protected internal PackagePart Part
        {
            get { return (_xlPackage.Package.GetPart(WorkbookUri)); }
        }
        #region WorkbookXml
        /// <summary>
        /// Provides access to the XML data representing the workbook in the package.
        /// </summary>
        public XmlDocument WorkbookXml
        {
            get
            {
                if (_xmlWorkbook == null)
                {
                    if (_xlPackage.Package.PartExists(WorkbookUri))
                    {
                        _xmlWorkbook = _xlPackage.GetXmlFromUri(WorkbookUri);
                    }
                    else
                    {
                        // create a new workbook part and add to the package
                        var partWorkbook = _xlPackage.Package.CreatePart(WorkbookUri,
                                                                                 @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml");

                        // create the workbook
                        _xmlWorkbook = new XmlDocument();
                        // create the workbook tag
                        var tagWorkbook = _xmlWorkbook.CreateElement("workbook", ExcelPackage.schemaMain);
                        // Add the relationships namespace
                        ExcelPackage.AddSchemaAttribute(tagWorkbook, ExcelPackage.schemaRelationships, "r");
                        _xmlWorkbook.AppendChild(tagWorkbook);

                        //// create the bookViews tag
                        var bookViews = _xmlWorkbook.CreateElement("bookViews", ExcelPackage.schemaMain);
                        tagWorkbook.AppendChild(bookViews);
                        var workbookView = _xmlWorkbook.CreateElement("workbookView", ExcelPackage.schemaMain);
                        bookViews.AppendChild(workbookView);

                        // create the sheets tag
                        var tagSheets = _xmlWorkbook.CreateElement("sheets", ExcelPackage.schemaMain);
                        tagWorkbook.AppendChild(tagSheets);

                        // save it to the package
                        var streamWorkbook = new StreamWriter(partWorkbook.GetStream(FileMode.Create, FileAccess.Write));
                        _xmlWorkbook.Save(streamWorkbook);
                        streamWorkbook.Close();
                        _xlPackage.Package.Flush();
                    }
                }
                return (_xmlWorkbook);
            }
        }
        #endregion
        #region SharedStrings
        /// <summary>
        /// Provides access to the XML data representing the shared strings in the package.
        /// For internal use only!
        /// </summary>
        protected internal XmlDocument SharedStringsXml
        {
            get
            {
                if (_xmlSharedStrings == null)
                {
                    if (_xlPackage.Package.PartExists(SharedStringsUri))
                    {
                        _xmlSharedStrings = _xlPackage.GetXmlFromUri(SharedStringsUri);
                    }
                    else
                    {
                        // create a new sharedStrings part and add to the package
                        var partStrings = _xlPackage.Package.CreatePart(SharedStringsUri,
                                                                                @"application/vnd.openxmlformats-officedocument.spreadsheetml.sharedStrings+xml");

                        // create the shared strings xml doc (with no entries in it)
                        _xmlSharedStrings = new XmlDocument();
                        var tagSst = _xmlSharedStrings.CreateElement("sst", ExcelPackage.schemaMain);
                        tagSst.SetAttribute("count", "0");
                        tagSst.SetAttribute("uniqueCount", "0");
                        _xmlSharedStrings.AppendChild(tagSst);

                        // save it to the package
                        var streamStrings = new StreamWriter(partStrings.GetStream(FileMode.Create, FileAccess.Write));
                        _xmlSharedStrings.Save(streamStrings);
                        streamStrings.Close();
                        _xlPackage.Package.Flush();

                        // create the relationship between the workbook and the new shared strings part
                        _xlPackage.Workbook.Part.CreateRelationship(SharedStringsUri,
                                                                    TargetMode.Internal,
                                                                    ExcelPackage.schemaRelationships + "/sharedStrings");
                        _xlPackage.Package.Flush();
                    }
                }
                return (_xmlSharedStrings);
            }
        }
        #endregion
        #region StylesXml
        /// <summary>
        /// Provides access to the XML data representing the styles in the package. 
        /// </summary>
        public XmlDocument StylesXml
        {
            get
            {
                if (_xmlStyles == null)
                {
                    if (_xlPackage.Package.PartExists(StylesUri))
                    {
                        _xmlStyles = _xlPackage.GetXmlFromUri(StylesUri);
                    }
                    else
                    {
                        // create a new styles part and add to the package
                        var partSyles = _xlPackage.Package.CreatePart(StylesUri,
                                                                              @"application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml");

                        // create the style sheet
                        _xmlStyles = new XmlDocument();
                        var tagStylesheet = _xmlStyles.CreateElement("styleSheet", ExcelPackage.schemaMain);
                        _xmlStyles.AppendChild(tagStylesheet);
                        // create the fonts tag
                        var tagFonts = _xmlStyles.CreateElement("fonts", ExcelPackage.schemaMain);
                        tagFonts.SetAttribute("count", "1");
                        tagStylesheet.AppendChild(tagFonts);
                        // create the font tag
                        var tagFont = _xmlStyles.CreateElement("font", ExcelPackage.schemaMain);
                        tagFonts.AppendChild(tagFont);
                        // create the sz tag
                        var tagSz = _xmlStyles.CreateElement("sz", ExcelPackage.schemaMain);
                        tagSz.SetAttribute("val", "11");
                        tagFont.AppendChild(tagSz);
                        // create the name tag
                        var tagName = _xmlStyles.CreateElement("name", ExcelPackage.schemaMain);
                        tagName.SetAttribute("val", "Calibri");
                        tagFont.AppendChild(tagName);
                        // create the cellStyleXfs tag
                        var tagCellStyleXfs = _xmlStyles.CreateElement("cellStyleXfs", ExcelPackage.schemaMain);
                        tagCellStyleXfs.SetAttribute("count", "1");
                        tagStylesheet.AppendChild(tagCellStyleXfs);
                        // create the xf tag
                        var tagXf = _xmlStyles.CreateElement("xf", ExcelPackage.schemaMain);
                        tagXf.SetAttribute("numFmtId", "0");
                        tagXf.SetAttribute("fontId", "0");
                        tagCellStyleXfs.AppendChild(tagXf);
                        // create the cellXfs tag
                        var tagCellXfs = _xmlStyles.CreateElement("cellXfs", ExcelPackage.schemaMain);
                        tagCellXfs.SetAttribute("count", "1");
                        tagStylesheet.AppendChild(tagCellXfs);
                        // create the xf tag
                        var tagXf2 = _xmlStyles.CreateElement("xf", ExcelPackage.schemaMain);
                        tagXf2.SetAttribute("numFmtId", "0");
                        tagXf2.SetAttribute("fontId", "0");
                        tagXf2.SetAttribute("xfId", "0");
                        tagCellXfs.AppendChild(tagXf2);

                        // save it to the package
                        var streamStyles = new StreamWriter(partSyles.GetStream(FileMode.Create, FileAccess.Write));
                        _xmlStyles.Save(streamStyles);
                        streamStyles.Close();
                        _xlPackage.Package.Flush();

                        // create the relationship between the workbook and the new shared strings part
                        _xlPackage.Workbook.Part.CreateRelationship(StylesUri,
                                                                    TargetMode.Internal,
                                                                    ExcelPackage.schemaRelationships + "/styles");
                        _xlPackage.Package.Flush();
                    }
                }
                return (_xmlStyles);
            }
            set { _xmlStyles = value; }
        }
        #endregion
        #region Office Document Properties
        /// <summary>
        /// Provides access to the office document properties
        /// </summary>
        public OfficeProperties Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new OfficeProperties(_xlPackage);
                }
                return _properties;
            }
        }
        #endregion
        #region CalcMode
        /// <summary>
        /// Allows you to set the calculation mode for the workbook.
        /// </summary>
        public ExcelCalcMode CalcMode
        {
            get
            {
                var retValue = ExcelCalcMode.Automatic;
                //  Retrieve the calcMode attribute in the calcPr element.
                var node = WorkbookXml.SelectSingleNode("//d:calcPr", _nsManager);
                if (node != null)
                {
                    var attr = node.Attributes["calcMode"];
                    if (attr != null)
                    {
                        switch (attr.Value)
                        {
                            case "auto":
                                retValue = ExcelCalcMode.Automatic;
                                break;
                            case "autoNoTable":
                                retValue = ExcelCalcMode.AutomaticNoTable;
                                break;
                            case "manual":
                                retValue = ExcelCalcMode.Manual;
                                break;
                        }
                    }
                }
                return (retValue);
            }
            set
            {
                var element = (XmlElement) WorkbookXml.SelectSingleNode("//d:calcPr", _nsManager);
                //if (element == null)
                //{
                //  // create the element
                //  element = WorkbookXml.CreateElement(
                //}
                var actualValue = "auto"; // default
                //  Set the value of the attribute:
                switch (value)
                {
                    case ExcelCalcMode.Automatic:
                        actualValue = "auto";
                        break;
                    case ExcelCalcMode.AutomaticNoTable:
                        actualValue = "autoNoTable";
                        break;
                    case ExcelCalcMode.Manual:
                        actualValue = "manual";
                        break;
                }
                element.SetAttribute("calcMode", actualValue);
            }
            #endregion
        }
        #endregion
        #region Workbook Private Methods
        #region Save // Workbook Save
        /// <summary>
        /// Saves the workbook and all its components to the package.
        /// For internal use only!
        /// </summary>
        protected internal void Save() // Workbook Save
        {
            // ensure we have at least one worksheet
            if (Worksheets.Count == 0)
            {
                throw new Exception("Workbook Save Error: the workbook must contain at least one worksheet!");
            }
            #region Delete calcChain component
            // if the calcChain component exists, we should delete it to force Excel to recreate it
            // when the spreadsheet is next opened
            if (_xlPackage.Package.PartExists(_uriCalcChain))
            {
                //  there will be a relationship with the workbook, so first delete the relationship
                var calcChain = new Uri("calcChain.xml", UriKind.Relative);
                foreach (var relationship in _xlPackage.Workbook.Part.GetRelationships())
                {
                    if (relationship.TargetUri == calcChain)
                    {
                        _xlPackage.Workbook.Part.DeleteRelationship(relationship.Id);
                        break;
                    }
                }
                // delete the calcChain component
                _xlPackage.Package.DeletePart(_uriCalcChain);
            }
            #endregion
            // save the workbook
            if (_xmlWorkbook != null)
            {
                _xlPackage.SavePart(WorkbookUri, _xmlWorkbook);
                _xlPackage.WriteDebugFile(_xmlWorkbook, "xl", "workbook.xml");
            }

            // save the properties of the workbook
            if (_properties != null)
            {
                _properties.Save();
            }

            // save the style sheet
            if (_xmlStyles != null)
            {
                _xlPackage.SavePart(StylesUri, _xmlStyles);
                _xlPackage.WriteDebugFile(_xmlStyles, "xl", "styles.xml");
            }

            // save the shared strings
            if (_xmlSharedStrings != null)
            {
                _xlPackage.SavePart(SharedStringsUri, _xmlSharedStrings);
                _xlPackage.WriteDebugFile(_xmlSharedStrings, "xl", "sharedstrings.xml");
            }

            // save all the open worksheets
            foreach (ExcelWorksheet worksheet in Worksheets)
            {
                worksheet.Save();
            }
        }
        #endregion
        #endregion
    } // end Workbook
}