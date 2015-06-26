using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using CoreBase.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ContainerStar.Lib.Managers
{
    public partial class OrdersManager
    {
        #region Prepare Print
        
        public Stream PrepareRentOrderPrintData(int id, string path)
        {
            var result = new MemoryStream();
            try
            {
                Package pkg;
                PackagePart part;
                XmlReader xmlReader;
                XDocument xmlMainXMLDoc;
                GetXmlDoc(path, result, out pkg, out part, out xmlReader, out xmlMainXMLDoc);

                //replace fields
                var templateBody = ReplaceFields(id, PrintTypes.RentOrder, xmlMainXMLDoc);

                xmlMainXMLDoc = SaveDoc(result, pkg, part, xmlReader, xmlMainXMLDoc, templateBody);
            }
            catch
            {
            }

            return result;
        }

        private void GetXmlDoc(string path, MemoryStream result, out Package pkg, out PackagePart part, out XmlReader xmlReader, out XDocument xmlMainXMLDoc)
        {
            var fileStream = System.IO.File.Open(path, FileMode.Open);
            CopyStream(fileStream, result);
            fileStream.Close();

            pkg = Package.Open(result, FileMode.Open, FileAccess.ReadWrite);

            var uri = new Uri("/word/document.xml", UriKind.Relative);
            part = pkg.GetPart(uri);

            xmlReader = XmlReader.Create(part.GetStream(FileMode.Open, FileAccess.Read));
            xmlMainXMLDoc = XDocument.Load(xmlReader);
        }

        private XDocument SaveDoc(MemoryStream result, Package pkg, PackagePart part, XmlReader xmlReader, XDocument xmlMainXMLDoc, string templateBody)
        {
            xmlMainXMLDoc = XDocument.Parse(templateBody);

            var partWrt = new StreamWriter(part.GetStream(FileMode.Open, FileAccess.ReadWrite));
            xmlMainXMLDoc.Save(partWrt);

            partWrt.Flush();
            partWrt.Close();
            pkg.Close();

            result.Position = 0;

            xmlReader.Close();
            return xmlMainXMLDoc;
        }

        private void CopyStream(Stream source, Stream target)
        {
            source.Position = 0;

            int bytesRead;
            byte[] buffer = new byte[source.Length];

            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                target.Write(buffer, 0, bytesRead);
            }

            source.Position = 0;
        }

        #endregion

        #region Replace Fields Info

        private string ReplaceFields(int orderId, PrintTypes printType, XDocument xmlMainXMLDoc)
        {
            string result = xmlMainXMLDoc.Root.ToString();

            var order = GetById(orderId);

            if (order != null)
            {
                switch (printType)
                {
                    case PrintTypes.RentOrder:
                        result = ReplaceCommonFields(order, result);
                        result = ReplaceResultFields(order, result);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return result;
        }

        private string ReplaceCommonFields(Orders order, string xmlMainXMLDoc)
        {
            xmlMainXMLDoc = xmlMainXMLDoc.Replace("#RentOrderNumber", order.RentOrderNumber);
            xmlMainXMLDoc = xmlMainXMLDoc.Replace("#CustomerName", order.CustomerName);
            xmlMainXMLDoc = xmlMainXMLDoc.Replace("#CustomerStreet", order.Customers.Street);
            xmlMainXMLDoc = xmlMainXMLDoc.Replace("#CustomerZip", order.Customers.Zip.ToString());
            xmlMainXMLDoc = xmlMainXMLDoc.Replace("#CustomerCity", order.Customers.City);
            xmlMainXMLDoc = xmlMainXMLDoc.Replace("#DeliveryPlace", order.DeliveryPlace);
            xmlMainXMLDoc = xmlMainXMLDoc.Replace("#Street", order.Street);
            xmlMainXMLDoc = xmlMainXMLDoc.Replace("#ZIP", order.Zip.ToString());
            xmlMainXMLDoc = xmlMainXMLDoc.Replace("#City", order.City);
            xmlMainXMLDoc = xmlMainXMLDoc.Replace("#Today", ConvertToTimeString(DateTime.Now));            
                        
            return xmlMainXMLDoc;
        }

        private string ReplaceResultFields(Orders order, string xmlMainXMLDoc)
        {
            if (order.Positions != null && order.Positions.Count != 0)
            {
                var positions = order.Positions.Where(o => !o.DeleteDate.HasValue && o.ContainerId.HasValue).ToList();
                var minDate = positions.Min(o => o.FromDate);
                var maxDate = positions.Min(o => o.ToDate);

                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#FromDate", ConvertToTimeString(minDate));
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#ToDate", ConvertToTimeString(minDate));  

                var xmlDoc = XDocument.Parse(xmlMainXMLDoc);
                var temp = xmlDoc.Descendants().LastOrDefault(o => o.Value.Contains("#ContainerDescription"));
                var parentTableElement = GetParentElementByName(temp, "<w:tr ");
                
                if (parentTableElement != null)
                {
                    var prevTableElem = parentTableElement;

                    foreach (var position in positions)
                    {
                        //var positionDescription = String.Format("<w:p>*\t 1 Stück {0}</w:p><w:p>Maße {1}x{2}x{3} mm</w:p><w:p>mit folgender Ausstattung / Einrichtung:</w:p>", 
                        //    position.Containers.ContainerTypes.Name,
                        //    position.Containers.Length,
                        //    position.Containers.Width,
                        //    position.Containers.Height);

                        var positionDescription = String.Format("*\t 1 Stück {0}",
                            position.Containers.ContainerTypes.Name);

                        
                        var elem = XElement.Parse(parentTableElement.ToString());
                        var parentTextElement = GetParentElementByName(elem, "<w:p ");
                        //.
                        //    Replace("#ContainerDescription", positionDescription)
                        prevTableElem.AddAfterSelf(elem);
                        prevTableElem = elem;
                    }

                    parentTableElement.Remove(); 

                    xmlMainXMLDoc = xmlDoc.Root.ToString();
                }
            }
            else
            {
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#ContainerDescription", String.Empty);
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#FromDate", String.Empty);
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#ToDate", String.Empty);  
            }

            return xmlMainXMLDoc;
        }

        private static string ConvertToTimeString(DateTime date)
        {
            if(date != DateTime.MinValue && date != DateTime.MaxValue)
                return date.ToString("dd.MM.yyyy hh:mm");
            else
                return String.Empty;
        }

        private XElement GetParentElementByName(XElement element, string name)
        {
            XElement result = null;

            while (true)
            {
                if (element == null || element.Parent == null)
                {
                    break;
                }
                else
                {
                    if (element.Parent.ToString().StartsWith(name))
                    {
                        result = element.Parent;
                        break;
                    }

                    element = element.Parent;
                }
            }

            return result;
        }

        private XElement GetInnerElementByName(XElement element, string name)
        {
            XElement result = null;

            if (element != null && element.Elements() != null)
            {
                foreach (var item in element.Elements())
                {
                    if (item.ToString().StartsWith(name))
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
