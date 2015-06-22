using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using MetadataLoader.Excel.OfficeOpenXML;

namespace MetadataLoader.Excel
{
    public static class DataSheetManager
    {
        public static DataSet GenerateStab(params TableDescriptor[] descriptors)
        {
            #region Check
            foreach (var descriptor in descriptors.Where(d => !d.CanWrite))
            {
                throw new ArgumentException(string.Format("Descriptor '{0}' doesn't support write", descriptor.SheetName), "descriptors");
            }
            #endregion
            var set = new DataSet();
            foreach (var descriptor in descriptors)
            {
                var table = new DataTable(descriptor.SheetName);
                #region Init table
                for (var columnIndex = 0; columnIndex < descriptor.ColumnCount; columnIndex++)
                {
                    table.Columns.Add(descriptor.ColumnNames[columnIndex], typeof (string));
                }
                #endregion
                set.Tables.Add(table);
            }
            return set;
        }

        public static DataSet Read(string filePath, params TableDescriptor[] descriptors)
        {
            return ReadOpenXml(filePath, descriptors);
        }

        public static DataSet ReadOpenXml(string filePath, params TableDescriptor[] descriptors)
        {
            #region Check
            foreach (var descriptor in descriptors)
            {
                if (!descriptor.CanRead)
                {
                    throw new ArgumentException(string.Format("Descriptor '{0}' doesn't support read", descriptor.SheetName), "descriptors");
                }
            }
            #endregion
            var set = new DataSet();
            using (var package = ExcelPackage.Open(filePath))
            {
                foreach (var descriptor in descriptors)
                {
                    var worksheet = package.Workbook.Worksheets[descriptor.SheetName];
                    var table = new DataTable(descriptor.SheetName);
                    #region Init table
                    var columnCount = descriptor.ColumnCount;
                    if (descriptor.DynamicColumnCount)
                    {
                        columnCount = 1;
                        while (!string.IsNullOrEmpty(worksheet.Cell(descriptor.HeaderRow, columnCount).Value))
                        {
                            columnCount++;
                        }
                        columnCount--;
                    }

                    for (var columnIndex = 1; columnIndex <= columnCount; columnIndex++)
                    {
                        var cell = worksheet.Cell(descriptor.HeaderRow, columnIndex);
                        table.Columns.Add(cell.Value, typeof (string));
                    }
                    set.Tables.Add(table);
                    #endregion
                    var rowIndex = descriptor.BeginRow;
                    do
                    {
                        #region Check end
                        foreach (var keyColumn in descriptor.Key.Items)
                        {
                            var keyCell = worksheet.Cell(rowIndex, keyColumn);
                            var value = keyCell.Value;
                            if (!string.IsNullOrEmpty(value))
                            {
                                goto NEXT;
                            }
                        }
                        break;
                        NEXT:
                        #endregion
                        var row = table.NewRow();
                        for (var columnIndex = 1; columnIndex <= columnCount; columnIndex++)
                        {
                            var cell = worksheet.Cell(rowIndex, columnIndex);
                            row[columnIndex - 1] = cell.Value;
                        }
                        table.Rows.Add(row);
                        rowIndex++;
                    } while (true);
                }
            }
            return set;
        }


        /// <summary>
        ///     Write only to prepared empty workbook
        /// </summary>
        /// <param name="filePath"> </param>
        /// <param name="set"> </param>
        /// <param name="descriptors"> </param>
        public static void Write(string filePath, DataSet set, params TableDescriptor[] descriptors)
        {
            #region Check
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            if (ReferenceEquals(set, null))
            {
                throw new ArgumentNullException("set");
            }
            if (set.Tables.Count != descriptors.Length)
            {
                throw new ArgumentException("Tables and descritors have different count", "descriptors");
            }
            foreach (var descriptor in descriptors)
            {
                if (!descriptor.CanWrite)
                {
                    throw new ArgumentException(string.Format("Descriptor '{0}' doesn't support write", descriptor.SheetName), "descriptors");
                }
            }
            #endregion
            using (var package = ExcelPackage.Open(filePath))
            {
                foreach (var descriptor in descriptors)
                {
                    var worksheet = package.Workbook.Worksheets[descriptor.SheetName];
                    var table = set.Tables[descriptor.SheetName];
                    var rowIndex = descriptor.BeginRow;
                    foreach (DataRow row in table.Rows)
                    {
                        for (var columnIndex = 1; columnIndex <= descriptor.ColumnCount; columnIndex++)
                        {
                            var cell = worksheet.Cell(rowIndex, columnIndex);
                            cell.Value = Convert.ToString(row[columnIndex - 1]);
                        }
                        rowIndex++;
                    }
                }
                package.Save();
            }
        }
        /// <summary>
        ///     Write only to prepared empty workbook
        /// </summary>
        /// <param name="filePath"> </param>
        /// <param name="elements"> </param>
        /// <param name="descriptor"> </param>
        public static void Write<T>(string filePath, IEnumerable<T> elements, TableDescriptor descriptor)
        {
            #region Check
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            if (ReferenceEquals(elements, null))
            {
                throw new ArgumentNullException("elements");
            }
            if (ReferenceEquals(descriptor, null))
            {
                throw new ArgumentNullException("descriptor");
            }
            if (!descriptor.CanWrite)
            {
                throw new ArgumentException("Descriptor doesn't support write", "descriptor");
            }
            #endregion
            var propertyInfos = new Dictionary<string, PropertyInfo>();
            foreach (var info in typeof (T).GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public))
            {
                propertyInfos.Add(info.Name, info);
            }

            using (var package = ExcelPackage.Open(filePath))
            {
                var worksheet = package.Workbook.Worksheets[descriptor.SheetName];
                var rowIndex = descriptor.BeginRow;
                foreach (var element in elements)
                {
                    for (var columnIndex = 0; columnIndex < descriptor.ColumnNames.Length; columnIndex++)
                    {
                        var columnName = descriptor.ColumnNames[columnIndex];
                        var cell = worksheet.Cell(rowIndex, columnIndex + 1);
                        cell.Value = Convert.ToString(propertyInfos[columnName].GetValue(element, new object[0]));
                    }
                    rowIndex++;
                }
                package.Save();
            }
        }
    }
}