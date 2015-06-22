using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace EdmxGenerator
{
    internal sealed class DefaultsGenerator
    {
        ///<summary>
        /// Singleton instance 
        ///</summary>
        public static readonly DefaultsGenerator Instance = new DefaultsGenerator();

        private const string SelectDefaultsFromDB = @"
SELECT 
  SCHEMA_NAME(so.[schema_id]) AS [schema],
  so.name AS [table],
  sc.name AS [column],
  TYPE_NAME(sc.system_type_id) AS [type],
  sdc.[definition] 
FROM sys.default_constraints sdc 
INNER JOIN sys.objects so ON so.[object_id] = sdc.[parent_object_id]
INNER JOIN sys.columns sc ON sdc.[parent_object_id] = sc.[object_id] AND sdc.[parent_column_id] = sc.[column_id]";

        public static Regex stringRegEx = new Regex(
                "'(?<TEXT>.*)'",
                RegexOptions.Singleline
                | RegexOptions.CultureInvariant
                | RegexOptions.Compiled
                );

        public void Generate(string connString, FileInfo outputFile)
        {
            var values = ReadValues(connString);
            Generate(values, outputFile);
        }

        public string RemoveExcessBraces(string value)
        {
            while (value.StartsWith("(") && value.EndsWith(")"))
            {
                value = value.Remove(value.Length - 1, 1).Remove(0, 1);
            }
            return value;
        }


        public bool PrepareValue(string type, ref string value)
        {
            value = RemoveExcessBraces(value);
            switch (type)
            {
                    #region DateTime
                case "datetime":
                case "datetime2":
                    switch (value.ToLower())
                    {
                        case "getdate()":
                            value = "DateTime.Now";
                            break;
                        default:
                            return false;
                    }
                    break;
                    #endregion
                    #region Bit
                case "bit":
                    switch (value)
                    {
                        case "0":
                            value = "false";
                            break;
                        case "1":
                            value = "true";
                            break;
                        default:
                            return false;
                    }
                    break;
                    #endregion
                    #region Int
                case "int":
                {
                    int val;
                    if (!int.TryParse(value, out val))
                    {
                        return false;
                    }
                    value = val.ToString(CultureInfo.InvariantCulture);
                }
                    break;
                    #endregion
                    #region Short
                case "smallint":
                {
                    short val;
                    if (!short.TryParse(value, out val))
                    {
                        return false;
                    }
                    value = val.ToString(CultureInfo.InvariantCulture);
                }
                    break;
                    #endregion
                    #region Byte
                case "tinyint":
                {
                    byte val;
                    if (!byte.TryParse(value, out val))
                    {
                        return false;
                    }
                    value = val.ToString(CultureInfo.InvariantCulture);
                }
                    break;
                    #endregion
                    #region Guid
                case "uniqueidentifier":
                {
                    switch (value.ToLower())
                    {
                        case "newsequentialid()":
                            value = "Guid.NewGuid()";
                            break;
                        default:
                            return false;
                    }
                }
                    break;
                    #endregion
                    #region String
                case "nchar":
                case "nvarchar":
                case "char":
                case "varchar":
                {
                    var match = stringRegEx.Match(value);
                    if (!match.Success)
                    {
                        return false;
                    }
                    value = "\"" + match.Groups["TEXT"].Value.Replace("''", "'").Replace("\"", "\\\"") + "\"";
                }
                    break;
                    #endregion
                default:
                    return false;
            }
            return true;
        }

        public List<DefaultValue> ReadValues(string connString)
        {
            if (string.IsNullOrWhiteSpace(connString))
            {
                return new List<DefaultValue>();
            }
            var sqlEngine = new ScriptEngine(connString);
            var extracted = new List<DefaultValue>();
            try
            {
                sqlEngine.OpenConnection();
                var ds = sqlEngine.ExecuteDataAdapter(SelectDefaultsFromDB);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var type = row["type"].ToString();
                    var value = row["definition"].ToString();
                    var isReady = PrepareValue(type, ref value);

                    extracted.Add(new DefaultValue
                    {
                        Schema = row["schema"].ToString(),
                        Table = row["table"].ToString(),
                        Column = row["column"].ToString(),
                        Type = type,
                        IsRaw = !isReady,
                        Value = value
                    });
                }
            }
            finally
            {
                sqlEngine.CloseConnection();
            }
            return extracted;

        }

        public void Generate(List<DefaultValue> values, FileInfo outputFile)
        {
            var sb = new StringBuilder();
            sb.AppendLine("------------------------------------------------------------------");
            sb.AppendLine("--        Autogenerated by EdmxGenerator tool       --");
            sb.AppendLine("------------------------------------------------------------------");
            sb.AppendLine();
            foreach (var val in values)
            {
                sb.AppendFormat("{0}.{1}.{2} {3} {4} {5}", val.Schema, val.Table, val.Column, val.IsRaw ? "RAW" : "READY", val.Type, val.Value);
                sb.AppendLine();
            }
            File.WriteAllText(outputFile.FullName, sb.ToString(), Encoding.UTF8);
        }

        internal sealed class DefaultValue
        {
            public string Schema { get; set; }
            public string Table { get; set; }
            public string Column { get; set; }
            public string Type { get; set; }

            /// <summary>
            /// Is prerpared value or raw from db
            /// </summary>
            public bool IsRaw { get; set; }

            public string Value { get; set; }

            public override string ToString()
            {
                return string.Format("{0}.{1}.{2} {3},{4} {5}", Schema, Table, Column, Type, IsRaw ? " RAW" : string.Empty, Value);
            }
        }
    }
}