using System;
using System.Xml;

namespace MetadataLoader.Excel.OfficeOpenXML
{
    #region ExcelHeaderFooter
    /// <summary>
    /// Represents the Header and Footer on an Excel Worksheet
    /// </summary>
    public class ExcelHeaderFooter
    {
        #region Static Properties
        /// <summary>
        /// Use this to insert the page number into the header or footer of the worksheet
        /// </summary>
        public const string PageNumber = @"&P";
        /// <summary>
        /// Use this to insert the number of pages into the header or footer of the worksheet
        /// </summary>
        public const string NumberOfPages = @"&N";
        /// <summary>
        /// Use this to insert the name of the worksheet into the header or footer of the worksheet
        /// </summary>
        public const string SheetName = @"&A";
        /// <summary>
        /// Use this to insert the full path to the folder containing the workbook into the header or footer of the worksheet
        /// </summary>
        public const string FilePath = @"&Z";
        /// <summary>
        /// Use this to insert the name of the workbook file into the header or footer of the worksheet
        /// </summary>
        public const string FileName = @"&F";
        /// <summary>
        /// Use this to insert the current date into the header or footer of the worksheet
        /// </summary>
        public const string CurrentDate = @"&D";
        /// <summary>
        /// Use this to insert the current time into the header or footer of the worksheet
        /// </summary>
        public const string CurrentTime = @"&T";
        #endregion
        #region ExcelHeaderFooter Private Properties
        private readonly XmlElement _headerFooterNode;
        private ExcelHeaderFooterText _header;
        private ExcelHeaderFooterText _footer;

        private ExcelHeaderFooterText _evenHeader;
        private ExcelHeaderFooterText _evenFooter;

        private ExcelHeaderFooterText _firstHeader;
        private ExcelHeaderFooterText _firstFooter;

        private bool? _alignWithMargins;
        private bool? _differentOddEven;
        private bool? _differentFirst;
        #endregion
        #region ExcelHeaderFooter Constructor
        /// <summary>
        /// ExcelHeaderFooter Constructor
        /// For internal use only!
        /// </summary>
        /// <param name="HeaderFooterNode"></param>
        protected internal ExcelHeaderFooter(XmlElement HeaderFooterNode)
        {
            #region Check
            if (HeaderFooterNode.Name != "headerFooter")
            {
                throw new Exception("ExcelHeaderFooter Error: Passed invalid headerFooter node");
            }
            #endregion
            _headerFooterNode = HeaderFooterNode;
            // TODO: populate structure based on XML content
        }
        #endregion
        #region alignWithMargins
        /// <summary>
        /// Gets/sets the alignWithMargins attribute
        /// </summary>
        public bool AlignWithMargins
        {
            get
            {
                if (_alignWithMargins == null)
                {
                    _alignWithMargins = false;
                    var attr = (XmlAttribute) _headerFooterNode.Attributes.GetNamedItem("alignWithMargins");
                    if (attr != null)
                    {
                        if (attr.Value == "1")
                        {
                            _alignWithMargins = true;
                        }
                    }
                }
                return _alignWithMargins.Value;
            }
            set
            {
                _alignWithMargins = value;
                var attr = (XmlAttribute) _headerFooterNode.Attributes.GetNamedItem("alignWithMargins") ??
                                    _headerFooterNode.Attributes.Append(_headerFooterNode.OwnerDocument.CreateAttribute("alignWithMargins"));
                attr.Value = value ? "1" : "0";
            }
        }
        #endregion
        #region differentOddEven
        /// <summary>
        /// Gets/sets the flag that tells Excel to display different headers and footers on odd and even pages.
        /// </summary>
        public bool differentOddEven
        {
            get
            {
                if (_differentOddEven == null)
                {
                    _differentOddEven = false;
                    var attr = (XmlAttribute) _headerFooterNode.Attributes.GetNamedItem("differentOddEven");
                    if (attr != null)
                    {
                        if (attr.Value == "1")
                        {
                            _differentOddEven = true;
                        }
                    }
                }
                return _differentOddEven.Value;
            }
            set
            {
                _differentOddEven = value;
                var attr = (XmlAttribute) _headerFooterNode.Attributes.GetNamedItem("differentOddEven") ??
                                    _headerFooterNode.Attributes.Append(
                                        _headerFooterNode.OwnerDocument.CreateAttribute("differentOddEven"));
                attr.Value = value ? "1" : "0";
            }
        }
        #endregion
        #region differentFirst
        /// <summary>
        /// Gets/sets the flag that tells Excel to display different headers and footers on the first page of the worksheet.
        /// </summary>
        public bool differentFirst
        {
            get
            {
                if (_differentFirst == null)
                {
                    _differentFirst = false;
                    var attr = (XmlAttribute) _headerFooterNode.Attributes.GetNamedItem("differentFirst");
                    if (attr != null)
                    {
                        if (attr.Value == "1")
                        {
                            _differentFirst = true;
                        }
                    }
                }
                return _differentFirst.Value;
            }
            set
            {
                _differentFirst = value;
                var attr = (XmlAttribute) _headerFooterNode.Attributes.GetNamedItem("differentFirst") ??
                                    _headerFooterNode.Attributes.Append(_headerFooterNode.OwnerDocument.CreateAttribute("differentFirst"));
                attr.Value = value ? "1" : "0";
            }
        }
        #endregion
        #region ExcelHeaderFooter Public Properties
        /// <summary>
        /// Provides access to a ExcelHeaderFooterText class that allows you to set the values of the header on odd numbered pages of the document.
        /// If you want the same header on both odd and even pages, then only set values in this ExcelHeaderFooterText class.
        /// </summary>
        public ExcelHeaderFooterText Header
        {
            get
            {
                if (_header == null)
                {
                    _header = new ExcelHeaderFooterText();
                }
                return _header;
            }
        }
        /// <summary>
        /// Provides access to a ExcelHeaderFooterText class that allows you to set the values of the footer on odd numbered pages of the document.
        /// If you want the same footer on both odd and even pages, then only set values in this ExcelHeaderFooterText class.
        /// </summary>
        public ExcelHeaderFooterText Footer
        {
            get
            {
                if (_footer == null)
                {
                    _footer = new ExcelHeaderFooterText();
                }
                return _footer;
            }
        }
        // evenHeader and evenFooter set differentOddEven = true
        /// <summary>
        /// Provides access to a ExcelHeaderFooterText class that allows you to set the values of the header on even numbered pages of the document.
        /// </summary>
        public ExcelHeaderFooterText EvenHeader
        {
            get
            {
                if (_evenHeader == null)
                {
                    _evenHeader = new ExcelHeaderFooterText();
                }
                differentOddEven = true;
                return _evenHeader;
            }
        }
        /// <summary>
        /// Provides access to a ExcelHeaderFooterText class that allows you to set the values of the footer on even numbered pages of the document.
        /// </summary>
        public ExcelHeaderFooterText EvenFooter
        {
            get
            {
                if (_evenFooter == null)
                {
                    _evenFooter = new ExcelHeaderFooterText();
                }
                differentOddEven = true;
                return _evenFooter;
            }
        }
        // firstHeader and firstFooter set differentFirst = true
        /// <summary>
        /// Provides access to a ExcelHeaderFooterText class that allows you to set the values of the header on the first page of the document.
        /// </summary>
        public ExcelHeaderFooterText FirstHeader
        {
            get
            {
                if (_firstHeader == null)
                {
                    _firstHeader = new ExcelHeaderFooterText();
                }
                differentFirst = true;
                return _firstHeader;
            }
        }
        /// <summary>
        /// Provides access to a ExcelHeaderFooterText class that allows you to set the values of the footer on the first page of the document.
        /// </summary>
        public ExcelHeaderFooterText FirstFooter
        {
            get
            {
                if (_firstFooter == null)
                {
                    _firstFooter = new ExcelHeaderFooterText();
                }
                differentFirst = true;
                return _firstFooter;
            }
        }
        #endregion
        #region Save  //  ExcelHeaderFooter
        /// <summary>
        /// Saves the header and footer information to the worksheet XML
        /// </summary>
        protected internal void Save()
        {
            //  The header/footer elements must appear in this order, if they appear:
            //  <oddHeader />
            //  <oddFooter />
            //  <evenHeader />
            //  <evenFooter />
            //  <firstHeader />
            //  <firstFooter />

            XmlNode node;
            if (_header != null)
            {
                node =
                    _headerFooterNode.AppendChild(_headerFooterNode.OwnerDocument.CreateElement("oddHeader",
                                                                                                ExcelPackage.schemaMain));
                node.InnerText = Header.GetHeaderFooterText();
            }
            if (_footer != null)
            {
                node =
                    _headerFooterNode.AppendChild(_headerFooterNode.OwnerDocument.CreateElement("oddFooter",
                                                                                                ExcelPackage.schemaMain));
                node.InnerText = Footer.GetHeaderFooterText();
            }

            // only set evenHeader and evenFooter 
            if (differentOddEven)
            {
                if (_evenHeader != null)
                {
                    node =
                        _headerFooterNode.AppendChild(_headerFooterNode.OwnerDocument.CreateElement("evenHeader",
                                                                                                    ExcelPackage.
                                                                                                        schemaMain));
                    node.InnerText = EvenHeader.GetHeaderFooterText();
                }
                if (_evenFooter != null)
                {
                    node =
                        _headerFooterNode.AppendChild(_headerFooterNode.OwnerDocument.CreateElement("evenFooter",
                                                                                                    ExcelPackage.
                                                                                                        schemaMain));
                    node.InnerText = EvenFooter.GetHeaderFooterText();
                }
            }

            // only set firstHeader and firstFooter
            if (differentFirst)
            {
                if (_firstHeader != null)
                {
                    node =
                        _headerFooterNode.AppendChild(_headerFooterNode.OwnerDocument.CreateElement("firstHeader",
                                                                                                    ExcelPackage.
                                                                                                        schemaMain));
                    node.InnerText = FirstHeader.GetHeaderFooterText();
                }
                if (_firstFooter != null)
                {
                    node =
                        _headerFooterNode.AppendChild(_headerFooterNode.OwnerDocument.CreateElement("firstFooter",
                                                                                                    ExcelPackage.
                                                                                                        schemaMain));
                    node.InnerText = FirstFooter.GetHeaderFooterText();
                }
            }
        }
        #endregion
    }
    #endregion
}