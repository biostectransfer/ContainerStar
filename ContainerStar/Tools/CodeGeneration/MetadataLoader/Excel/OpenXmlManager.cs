using System;
using System.Collections.Generic;
using System.Linq;
using MetadataLoader.Excel.OfficeOpenXML;

namespace MetadataLoader.Excel
{
    public static class OpenXmlManager
    {
        public static IEnumerable<string[]> Read(ExcelPackage package, TableDescriptor descriptor)
        {
            #region Check
            if (!descriptor.CanRead)
            {
                throw new ArgumentException(string.Format("Descriptor '{0}' doesn't support read", descriptor.SheetName), "descriptors");
            }
            #endregion
            {
                {
                    var worksheet = package.Workbook.Worksheets[descriptor.SheetName];
                    var columnCount = GetColumnCount(descriptor, worksheet);

                    var rowIndex = descriptor.BeginRow;
                    do
                    {
                        #region Check end
                        if (descriptor.Key.Items.Any(keyCol => !string.IsNullOrEmpty(worksheet.Cell(rowIndex, keyCol).Value)))
                        {
                            goto NEXT;
                        }
                        break;
                        NEXT:
                        #endregion
                        var array = new string[columnCount];
                        for (var columnIndex = 1; columnIndex <= columnCount; columnIndex++)
                        {
                            var cell = worksheet.Cell(rowIndex, columnIndex);
                            array[columnIndex - 1] = cell.Value;
                        }
                        yield return array;
                        rowIndex++;
                    } while (true);
                }
            }
        }

        public static string[] ReadColumnsNames(ExcelPackage package, TableDescriptor descriptor)
        {
            var worksheet = package.Workbook.Worksheets[descriptor.SheetName];
            var columnCount = GetColumnCount(descriptor, worksheet);

            var columns = new List<string>(columnCount);
            for (var columnIndex = 1; columnIndex <= columnCount; columnIndex++)
            {
                var cell = worksheet.Cell(descriptor.HeaderRow, columnIndex);
                columns.Add(cell.Value);
            }
            return columns.ToArray();
        }

        private static int GetColumnCount(TableDescriptor descriptor, ExcelWorksheet worksheet)
        {
            var columnCount = descriptor.ColumnCount;
            if (!descriptor.DynamicColumnCount)
            {
                return columnCount;
            }
            columnCount = 1;
            while (!string.IsNullOrEmpty(worksheet.Cell(descriptor.HeaderRow, columnCount).Value))
            {
                columnCount++;
            }
            columnCount--;
            return columnCount;
        }
    }
}