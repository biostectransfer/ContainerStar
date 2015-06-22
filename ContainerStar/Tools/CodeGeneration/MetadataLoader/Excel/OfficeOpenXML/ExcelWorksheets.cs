using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Xml;

namespace MetadataLoader.Excel.OfficeOpenXML
{
    /// <summary>
    /// Provides enumeration through all the worksheets in the workbook
    /// </summary>
    public class ExcelWorksheets : IEnumerable<ExcelWorksheet>
    {
        #region ExcelWorksheets Private Properties
        private readonly Dictionary<int, ExcelWorksheet> _worksheets;
        private readonly ExcelPackage _xlPackage;
        private readonly XmlNamespaceManager _nsManager;
        private readonly XmlNode _worksheetsNode;
        #endregion
        #region ExcelWorksheets Constructor
        /// <summary>
        /// Creates a new instance of the ExcelWorksheets class.
        /// For internal use only!
        /// </summary>
        /// <param name="xlPackage"></param>
        protected internal ExcelWorksheets(ExcelPackage xlPackage)
        {
            _xlPackage = xlPackage;
            //  Create a NamespaceManager to handle the default namespace, 
            //  and create a prefix for the default namespace:
            var nt = new NameTable();
            _nsManager = new XmlNamespaceManager(nt);
            _nsManager.AddNamespace("d", ExcelPackage.schemaMain);
            _nsManager.AddNamespace("r", ExcelPackage.schemaRelationships);

            // obtain container node for all worksheets
            _worksheetsNode = _xlPackage.Workbook.WorkbookXml.SelectSingleNode("//d:sheets", _nsManager);
            if (_worksheetsNode == null)
            {
                // create new node as it did not exist
                _worksheetsNode = _xlPackage.Workbook.WorkbookXml.CreateElement("sheets", ExcelPackage.schemaMain);
                _xlPackage.Workbook.WorkbookXml.DocumentElement.AppendChild(_worksheetsNode);
            }

            _worksheets = new Dictionary<int, ExcelWorksheet>();
            var positionId = 1;
            foreach (XmlNode sheetNode in _worksheetsNode.ChildNodes)
            {
                var name = sheetNode.Attributes["name"].Value;
                //  Get the relationship id attribute:
                var relId = sheetNode.Attributes["r:id"].Value;
                var sheetId = Convert.ToInt32(sheetNode.Attributes["sheetId"].Value);
                //if (sheetID != count)
                //{
                //  // renumber the sheets as they are in an odd order
                //  sheetID = count;
                //  sheetNode.Attributes["sheetId"].Value = sheetID.ToString();
                //}
                // get hidden attribute (if present)
                var hidden = false;
                XmlNode attr = sheetNode.Attributes["hidden"];
                if (attr != null)
                {
                    hidden = Convert.ToBoolean(attr.Value);
                }

                //string type = "";
                //attr = sheetNode.Attributes["type"];
                //if (attr != null)
                //  type = attr.Value;

                var sheetRelation = _xlPackage.Workbook.Part.GetRelationship(relId);
                var uriWorksheet = PackUriHelper.ResolvePartUri(_xlPackage.Workbook.WorkbookUri, sheetRelation.TargetUri);

                // add worksheet to our collection
                _worksheets.Add(positionId, new ExcelWorksheet(_xlPackage, relId, name, uriWorksheet, sheetId, hidden));
                positionId++;
            }
        }
        #endregion
        #region ExcelWorksheets Public Properties
        /// <summary>
        /// Returns the number of worksheets in the workbook
        /// </summary>
        public int Count
        {
            get { return (_worksheets.Count); }
        }
        #endregion
        #region ExcelWorksheets Public Methods
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<ExcelWorksheet> GetEnumerator()
        {
            return _worksheets.Values.GetEnumerator();
        }
        /// <summary>
        /// Returns an enumerator that allows the foreach syntax to be used to 
        /// itterate through all the worksheets
        /// </summary>
        /// <returns>An enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #region Add Worksheet
        /// <summary>
        /// Adds a blank worksheet with the desired name
        /// </summary>
        /// <param name="name"></param>
        public ExcelWorksheet Add(string name)
        {
            // first find maximum existing sheetID
            // also check the name is unique - if not throw an error
            var sheetId = 0;
            foreach (XmlNode sheet in _worksheetsNode.ChildNodes)
            {
                var attr = (XmlAttribute) sheet.Attributes.GetNamedItem("sheetId");
                if (attr != null)
                {
                    var curId = int.Parse(attr.Value);
                    if (curId > sheetId)
                    {
                        sheetId = curId;
                    }
                }
                attr = (XmlAttribute) sheet.Attributes.GetNamedItem("name");
                if (attr != null)
                {
                    if (attr.Value == name)
                    {
                        throw new Exception("Add worksheet Error: attempting to create worksheet with duplicate name");
                    }
                }
            }
            // we now have the max existing values, so add one
            sheetId++;

            // add the new worksheet to the package
            var uriWorksheet = new Uri("/xl/worksheets/sheet" + sheetId + ".xml", UriKind.Relative);
            var worksheetPart = _xlPackage.Package.CreatePart(uriWorksheet,
                                                                      @"application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml");

            // create the new, empty worksheet and save it to the package
            var streamWorksheet = new StreamWriter(worksheetPart.GetStream(FileMode.Create, FileAccess.Write));
            var worksheetXml = CreateNewWorksheet();
            worksheetXml.Save(streamWorksheet);
            streamWorksheet.Close();
            _xlPackage.Package.Flush();

            // create the relationship between the workbook and the new worksheet
            var rel = _xlPackage.Workbook.Part.CreateRelationship(uriWorksheet,
                                                                                  TargetMode.Internal,
                                                                                  ExcelPackage.schemaRelationships + "/worksheet");
            _xlPackage.Package.Flush();

            // now create the new worksheet tag and set name/SheetId attributes in the workbook.xml
            var worksheetNode = _xlPackage.Workbook.WorkbookXml.CreateElement("sheet", ExcelPackage.schemaMain);
            // create the new sheet node
            worksheetNode.SetAttribute("name", name);
            worksheetNode.SetAttribute("sheetId", sheetId.ToString());
            // set the r:id attribute
            worksheetNode.SetAttribute("id", ExcelPackage.schemaRelationships, rel.Id);
            // insert the sheet tag with all attributes set as above
            _worksheetsNode.AppendChild(worksheetNode);

            // create a reference to the new worksheet in our collection
            var worksheet = new ExcelWorksheet(_xlPackage, rel.Id, name, uriWorksheet, sheetId, false);
            var positionId = _worksheets.Count + 1;
            _worksheets.Add(positionId, worksheet);
            return worksheet;
        }

        /// <summary>
        /// Creates the XML document representing a new empty worksheet
        /// </summary>
        /// <returns></returns>
        protected internal XmlDocument CreateNewWorksheet()
        {
            // create the new worksheet
            var worksheetXml = new XmlDocument();
            // XML document does not exist so create the new worksheet XML doc
            var worksheetNode = worksheetXml.CreateElement("worksheet", ExcelPackage.schemaMain);
            worksheetNode.SetAttribute("xmlns:r", ExcelPackage.schemaRelationships);
            worksheetXml.AppendChild(worksheetNode);
            // create the sheetViews tag
            var tagSheetViews = worksheetXml.CreateElement("sheetViews", ExcelPackage.schemaMain);
            worksheetNode.AppendChild(tagSheetViews);
            // create the sheet View tag
            var tagSheetView = worksheetXml.CreateElement("sheetView", ExcelPackage.schemaMain);
            tagSheetView.SetAttribute("workbookViewId", "0");
            tagSheetViews.AppendChild(tagSheetView);
            // create the empty sheetData tag (must be present, but can be empty)
            var tagSheetData = worksheetXml.CreateElement("sheetData", ExcelPackage.schemaMain);
            worksheetNode.AppendChild(tagSheetData);
            return worksheetXml;
        }
        #endregion
        #region Delete Worksheet
        /// <summary>
        /// Delete a worksheet from the workbook package
        /// </summary>
        /// <param name="positionId">The position of the worksheet in the workbook</param>
        public void Delete(int positionId)
        {
            Delete(_worksheets[positionId]);
        }
        /// <summary>
        /// Delete a worksheet from the workbook package
        /// </summary>
        /// <param name="sheetName"></param>
        public void Delete(string sheetName)
        {
            Delete(this[sheetName]);
        }
        /// <summary>
        /// Delete a worksheet from the workbook package
        /// </summary>
        /// <param name="worksheet"></param>
        public void Delete(ExcelWorksheet worksheet)
        {
            #region Check
            if (!_worksheets.ContainsValue(worksheet))
            {
                throw new ArgumentException("Trying delete worksheet not presented in workbook");
            }
            if (_worksheets.Count == 1)
            {
                throw new Exception(
                        "Error: You are attempting to delete the last worksheet in the workbook.  One worksheet MUST be present in the workbook!");
            }
            #endregion
            // delete the worksheet from the package 
            _xlPackage.Package.DeletePart(worksheet.WorksheetUri);

            // delete the relationship from the package 
            _xlPackage.Workbook.Part.DeleteRelationship(worksheet.RelationshipID);

            // delete worksheet from the workbook XML
            var sheetsNode = _xlPackage.Workbook.WorkbookXml.SelectSingleNode("//d:workbook/d:sheets", _nsManager);
            if (sheetsNode != null)
            {
                var sheetNode = sheetsNode.SelectSingleNode(string.Format("./d:sheet[@sheetId={0}]", worksheet.SheetID), _nsManager);
                if (sheetNode != null)
                {
                    sheetsNode.RemoveChild(sheetNode);
                }
            }
            // delete worksheet from the Dictionary object
            foreach (var excelWorksheet in _worksheets)
            {
                if (ReferenceEquals(excelWorksheet.Value, worksheet))
                {
                    _worksheets.Remove(excelWorksheet.Key);
                    break;
                }
            }
        }
        #endregion
        /// <summary>
        /// Returns the worksheet at the specified position.  
        /// </summary>
        /// <param name="positionId">The position of the worksheet. 1-base</param>
        /// <returns></returns>
        public ExcelWorksheet this[int positionId]
        {
            get { return (_worksheets[positionId]); }
        }

        /// <summary>
        /// Returns the worksheet matching the specified name
        /// </summary>
        /// <param name="name">The name of the worksheet</param>
        /// <returns></returns>
        public ExcelWorksheet this[string name]
        {
            get
            {
                ExcelWorksheet xlWorksheet = null;
                foreach (var worksheet in _worksheets.Values)
                {
                    if (worksheet.Name == name)
                    {
                        xlWorksheet = worksheet;
                    }
                }
                return (xlWorksheet);
                //throw new Exception(string.Format("ExcelWorksheets Error: Worksheet '{0}' not found!",Name));
            }
        }

        /// <summary>
        /// Copies the named worksheet and creates a new worksheet in the same workbook
        /// </summary>
        /// <param name="name">The name of the existing worksheet</param>
        /// <param name="newName">The name of the new worksheet to create</param>
        /// <returns></returns>
        public ExcelWorksheet Copy(string name, string newName)
        {
            // TODO: implement copy worksheet
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion
    } // end class Worksheets
}