using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ContainerStar.Lib.Managers
{
    public partial class OrdersManager
    {
        #region Prepare Print

        public Stream PrepareRentOrderPrintData(int id, string path)
        {
            return PrepareCommonOrderPrintData(id, path, PrintTypes.RentOrder);
        }

        public Stream PrepareOfferPrintData(int id, string path)
        {
            return PrepareCommonOrderPrintData(id, path, PrintTypes.Offer);
        }

        private Stream PrepareCommonOrderPrintData(int id, string path, PrintTypes type)
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
                var templateBody = ReplaceFields(id, type, xmlMainXMLDoc);

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
                        result = ReplaceRentPositions(order, result);
                        result = ReplaceRentAdditionalCostPositions(order, result);
                        break;
                    case PrintTypes.Offer:
                      
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
            xmlMainXMLDoc = xmlMainXMLDoc.Replace("#Today", DateTime.Now.ToShortDateString());            
                        
            return xmlMainXMLDoc;
        }

        private string ReplaceRentPositions(Orders order, string xmlMainXMLDoc)
        {
            if (order.Positions != null && order.Positions.Count != 0)
            {
                var positions = order.Positions.Where(o => !o.DeleteDate.HasValue && o.ContainerId.HasValue).ToList();
                var minDate = positions.Min(o => o.FromDate);
                var maxDate = positions.Max(o => o.ToDate);

                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#FromDate", minDate.ToShortDateString());
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#ToDate", maxDate.ToShortDateString());


                xmlMainXMLDoc = ReplacePositionWithDescription(positions, xmlMainXMLDoc);

                xmlMainXMLDoc = ReplaceShortPositionDescription(positions, xmlMainXMLDoc);
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#ContainerDescription", String.Empty);
            }
            else
            {
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#ContainerDescription", String.Empty);
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#FromDate", String.Empty);
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#ToDate", String.Empty);  
            }

            return xmlMainXMLDoc;
        }

        private string ReplaceRentAdditionalCostPositions(Orders order, string xmlMainXMLDoc)
        {
            if (order.Positions != null && order.Positions.Count != 0)
            {
                var positions = order.Positions.Where(o => !o.DeleteDate.HasValue && o.AdditionalCostId.HasValue).ToList();
                var xmlDoc = XDocument.Parse(xmlMainXMLDoc);
                var temp = xmlDoc.Descendants().LastOrDefault(o => o.Value.Contains("#AdditionalCostDescription"));
                var parentTableElement = GetParentElementByName(temp, "<w:tr ");
                var prevElement = parentTableElement;

                if (parentTableElement != null)
                {
                    foreach (var position in positions)
                    {
                        var textElem = XElement.Parse(parentTableElement.ToString().
                            Replace("#AdditionalCostDescription", position.AdditionalCosts.Description).
                            Replace("#AdditionalCostPrice", Math.Round(position.Price * position.Amount, 2).ToString()));
                        prevElement.AddAfterSelf(textElem);
                        prevElement = textElem;
                    }

                    parentTableElement.Remove();
                }

                xmlMainXMLDoc = xmlDoc.Root.ToString();
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#AdditionalCostDescription", String.Empty);
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#AdditionalCostPrice", String.Empty);
            }
            else
            {
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#AdditionalCostDescription", String.Empty);
                xmlMainXMLDoc = xmlMainXMLDoc.Replace("#AdditionalCostPrice", String.Empty);
            }

            return xmlMainXMLDoc;
        }

        private string ReplaceShortPositionDescription(List<Positions> positions, string xmlMainXMLDoc)
        {
            var xmlDoc = XDocument.Parse(xmlMainXMLDoc);
            var temp = xmlDoc.Descendants().LastOrDefault(o => o.Value.Contains("#RentPositionDescription"));
            var parentTableElement = GetParentElementByName(temp, "<w:tr ");
            var prevElement = parentTableElement;

            if (parentTableElement != null)
            {
                foreach (var position in positions)
                {
                    var textElem = XElement.Parse(parentTableElement.ToString().
                        Replace("#RentPositionDescription", position.Containers.ContainerTypes.Name).
                        Replace("#RentPrice", Math.Round(position.Price / (double)30, 2).ToString()));
                    prevElement.AddAfterSelf(textElem);
                    prevElement = textElem;
                }

                parentTableElement.Remove();
            }

            return xmlDoc.Root.ToString();
        }
        
        private string ReplacePositionWithDescription(List<Positions> positions, string xmlMainXMLDoc)
        {
            var xmlDoc = XDocument.Parse(xmlMainXMLDoc);
            var temp = xmlDoc.Descendants().LastOrDefault(o => o.Value.Contains("#ContainerDescription"));
            var parentTableElement = GetParentElementByName(temp, "<w:tr ");

            if (parentTableElement != null)
            {
                var prevTableElem = parentTableElement;

                foreach (var position in positions)
                {
                    var rowElem = XElement.Parse(parentTableElement.ToString());
                    prevTableElem.AddAfterSelf(rowElem);
                    prevTableElem = rowElem;

                    var temp2 = prevTableElem.Descendants().LastOrDefault(o => o.Value.Contains("#ContainerDescription"));
                    var textElem = GetParentElementByName(temp2, "<w:p ");
                    var prevTextElem = textElem;

                    var elem = XElement.Parse(textElem.ToString().
                        Replace("#ContainerDescription", String.Format("*\t 1 Stück {0}",
                        position.Containers.ContainerTypes.Name)));

                    prevTextElem.AddAfterSelf(elem);
                    prevTextElem = elem;

                    elem = XElement.Parse(textElem.ToString().
                        Replace("#ContainerDescription", String.Format("Maße {0}x{1}x{2} mm",
                        position.Containers.Length,
                        position.Containers.Width,
                        position.Containers.Height)));

                    prevTextElem.AddAfterSelf(elem);
                    prevTextElem = elem;

                    elem = XElement.Parse(textElem.ToString().
                        Replace("#ContainerDescription", "mit folgender Ausstattung / Einrichtung:"));

                    prevTextElem.AddAfterSelf(elem);
                    prevTextElem = elem;

                    foreach (var equipment in position.Containers.ContainerEquipmentRsps)
                    {
                        elem = XElement.Parse(textElem.ToString().
                            Replace("#ContainerDescription",
                            String.Format("{0} x {1}", equipment.Amount, equipment.Equipments.Description)));

                        prevTextElem.AddAfterSelf(elem);
                        prevTextElem = elem;
                    }

                    textElem.Remove();
                }

                parentTableElement.Remove();
            }

            return xmlDoc.Root.ToString();
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
