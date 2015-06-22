using System.Diagnostics;
using System.Text;

namespace MetadataLoader.Excel.OfficeOpenXML
{
    /// <summary>
    /// Helper class for ExcelHeaderFooter - simply stores the three header or footer
    /// text strings. 
    /// </summary>
    public sealed class ExcelHeaderFooterText
    {
        #region Private fields
        /// <summary>
        /// Sets the text to appear on the left hand side of the header (or footer) on the worksheet.
        /// </summary>
        private string _leftAlignedText;
        /// <summary>
        /// Sets the text to appear in the center of the header (or footer) on the worksheet.
        /// </summary>
        private string _centeredText;
        /// <summary>
        /// Sets the text to appear on the right hand side of the header (or footer) on the worksheet.
        /// </summary>
        private string _rightAlignedText;
        #endregion
        /// <summary>
        /// Sets the text to appear on the left hand side of the header (or footer) on the worksheet.
        /// </summary>
        public string LeftAlignedText
        {
            [DebuggerStepThrough]
            get { return _leftAlignedText; }
            [DebuggerStepThrough]
            set { _leftAlignedText = value; }
        }
        /// <summary>
        /// Sets the text to appear in the center of the header (or footer) on the worksheet.
        /// </summary>
        public string CenteredText
        {
            [DebuggerStepThrough]
            get { return _centeredText; }
            [DebuggerStepThrough]
            set { _centeredText = value; }
        }
        /// <summary>
        /// Sets the text to appear on the right hand side of the header (or footer) on the worksheet.
        /// </summary>
        public string RightAlignedText
        {
            [DebuggerStepThrough]
            get { return _rightAlignedText; }
            [DebuggerStepThrough]
            set { _rightAlignedText = value; }
        }
        /// <summary>
        /// Returns xml inner text for node
        /// </summary>
        /// <returns></returns>
        internal string GetHeaderFooterText()
        {
            var retValue = new StringBuilder();
            if (!string.IsNullOrEmpty(LeftAlignedText))
            {
                retValue.AppendFormat("&L{0}", LeftAlignedText);
            }
            if (!string.IsNullOrEmpty(CenteredText))
            {
                retValue.AppendFormat("&C{0}", CenteredText);
            }
            if (!string.IsNullOrEmpty(RightAlignedText))
            {
                retValue.AppendFormat("&R{0}", RightAlignedText);
            }
            return retValue.ToString();
        }
    }
}