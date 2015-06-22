using System.Globalization;
using System.Text.RegularExpressions;
using MetadataLoader.MSSQL.Contracts.ServerTypes;

namespace MetadataLoader.MSSQL
{
    public static class DbDefaultToCSharpConverter
    {
        public static Regex StringRegEx = new Regex(
            "'(?<TEXT>.*)'",
            RegexOptions.Singleline
            | RegexOptions.CultureInvariant
            | RegexOptions.Compiled
            );

        public static string RemoveExcessBraces(string value)
        {
            while (value.StartsWith("(") && value.EndsWith(")"))
            {
                value = value.Remove(value.Length - 1, 1).Remove(0, 1);
            }
            return value;
        }
        public static string ConvertDefault(NativeTypes type, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            value = RemoveExcessBraces(value);
            switch (type)
            {
                    #region DateTime
                case NativeTypes.SmallDateTime:
                case NativeTypes.DateTime:
                case NativeTypes.DateTime2:
                    switch (value.ToLower())
                    {
                        case "sysdatetime()":
                        case "sysdatetimeoffset()":
                        case "sysutcdatetime()":
                        case "current_timestamp":
                        case "getdate()":
                        case "getutcdate()":
                            return "DateTime.Now";
                        default:
                            return null;
                    }
                    #endregion
                    #region Bit
                case NativeTypes.Bit:
                    switch (value)
                    {
                        case "0":
                            return "false";
                        case "1":
                            return "true";
                        default:
                            return null;
                    }
                    #endregion
                    #region Int
                case NativeTypes.Int:
                {
                    int val;
                    return !int.TryParse(value, out val) ? null : val.ToString(CultureInfo.InvariantCulture);
                }
                    #endregion
                    #region Decimal
                case NativeTypes.Decimal:
                {
                    decimal val;
                    return !decimal.TryParse(value, out val) ? null : val.ToString(CultureInfo.InvariantCulture);
                }
                    #endregion
                    #region Real
                case NativeTypes.Real:
                {
                    float val;
                    return !float.TryParse(value, out val) ? null : val.ToString(CultureInfo.InvariantCulture);
                }
                    #endregion
                    #region Float
                case NativeTypes.Float:
                {
                    double val;
                    return !double.TryParse(value, out val) ? null : val.ToString(CultureInfo.InvariantCulture);
                }
                    #endregion
                    #region Short
                case NativeTypes.SmallInt:
                {
                    short val;
                    return !short.TryParse(value, out val) ? null : val.ToString(CultureInfo.InvariantCulture);
                }
                    #endregion
                    #region Byte
                case NativeTypes.TinyInt:
                {
                    byte val;
                    return !byte.TryParse(value, out val) ? null : val.ToString(CultureInfo.InvariantCulture);
                }
                    #endregion
                    #region Guid
                case NativeTypes.UniqueIdentifier:
                {
                    switch (value.ToLower())
                    {
                        case "newsequentialid()":
                        case "newid()":
                            return "Guid.NewGuid()";
                        default:
                            return null;
                    }
                }
                    #endregion
                    #region String
                case NativeTypes.NChar:
                case NativeTypes.NVarchar:
                case NativeTypes.Char:
                case NativeTypes.Varchar:
                {
                    var match = StringRegEx.Match(value);
                    if (!match.Success)
                    {
                        return null;
                    }
                    return "\"" + match.Groups["TEXT"].Value.Replace("''", "'").Replace("\"", "\\\"") + "\"";
                }
                    #endregion
            }
            return null;
        }
    }
}