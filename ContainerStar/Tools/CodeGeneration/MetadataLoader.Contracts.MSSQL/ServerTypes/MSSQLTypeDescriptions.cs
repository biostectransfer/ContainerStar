using System;
using System.Collections.Generic;
using MetadataLoader.Contracts.CSharp;

namespace MetadataLoader.MSSQL.Contracts.ServerTypes
{
    public static class MSSQLTypeDescriptions
    {
        private static readonly Lazy<MSSQLTypeDescDictionary> _dictionary = new Lazy<MSSQLTypeDescDictionary>(Initialize);
        public static MSSQLTypeDescDictionary Dictionary
        {
            get { return _dictionary.Value; }
        }
        private static MSSQLTypeDescDictionary Initialize()
        {
            //NOTE: For type mappings see https://msdn.microsoft.com/en-us/library/cc716729(v=vs.110).aspx
            var dictionary = new MSSQLTypeDescDictionary();

            dictionary.AddType<byte[]>(TypeIndexes.Image, "image", TypeOptions.IsLob);
            dictionary.AddType<string>(TypeIndexes.Text, "text", TypeOptions.HasCollation | TypeOptions.IsLob);
            dictionary.AddType<Guid>(TypeIndexes.UniqueIdentifier, "uniqueidentifier", TypeOptions.None);
            dictionary.AddType<byte>(TypeIndexes.TinyInt, "tinyint", TypeOptions.None);
            dictionary.AddType<short>(TypeIndexes.SmallInt, "smallint", TypeOptions.None);
            dictionary.AddType<int>(TypeIndexes.Int, "int", TypeOptions.None, "integer");
            dictionary.AddType<DateTime>(TypeIndexes.SmallDateTime, NativeTypes.SmallDateTime, "smalldatetime", TypeOptions.None);
            dictionary.AddType<float>(TypeIndexes.Real, NativeTypes.Real, "real", TypeOptions.None);
            dictionary.AddType<decimal>(TypeIndexes.Money, NativeTypes.Money, "money", TypeOptions.None);
            dictionary.AddType<DateTime>(TypeIndexes.DateTime, "datetime", TypeOptions.None);

            dictionary.AddType<double>(TypeIndexes.Float, "float", TypeOptions.HasPrecision, "Double precision");
            dictionary.AddType<string>(TypeIndexes.NText, "ntext", TypeOptions.HasCollation | TypeOptions.IsLob | TypeOptions.IsUnicode);
            dictionary.AddType<bool>(TypeIndexes.Bit, "bit", TypeOptions.None);
            dictionary.AddType<decimal>(TypeIndexes.Decimal, "decimal", TypeOptions.HasPrecision | TypeOptions.HasScale, "dec");
            dictionary.AddType<decimal>(TypeIndexes.Numeric, "numeric", TypeOptions.HasPrecision | TypeOptions.HasScale);
            dictionary.AddType<decimal>(TypeIndexes.SmallMoney, "smallmoney", TypeOptions.None);
            dictionary.AddType<long>(TypeIndexes.BigInt, "bigint", TypeOptions.None);
            dictionary.AddType<byte[]>(
                TypeIndexes.Varbinary, NativeTypes.Varbinary, "varbinary", TypeOptions.HasLength | TypeOptions.IsLobWithMax, "binary varying");
            dictionary.AddType<string>(TypeIndexes.Varchar,
                "varchar", TypeOptions.HasLength | TypeOptions.HasCollation | TypeOptions.IsLobWithMax, "char varying", "character varying");
            dictionary.AddType<byte[]>(TypeIndexes.Binary, "binary", TypeOptions.HasLength | TypeOptions.IsFixedLength);
            dictionary.AddType<string>(TypeIndexes.Char, "char", TypeOptions.HasLength | TypeOptions.HasCollation|TypeOptions.IsFixedLength);
            dictionary.AddType<byte[]>(TypeIndexes.TimeStamp, "timestamp", TypeOptions.None, "rowversion");
            dictionary.AddType<string>(
                TypeIndexes.NVarchar,
                "nvarchar",
                TypeOptions.HasLength | TypeOptions.HasCollation | TypeOptions.IsLobWithMax|TypeOptions.IsUnicode,
                "nchar varying", "ncharacter varying", "national char varying", "national character varying");
            dictionary.AddType<string>(
                TypeIndexes.NChar,
                "nchar", TypeOptions.HasLength | TypeOptions.HasCollation | TypeOptions.IsUnicode | TypeOptions.IsFixedLength, "ncharacter", "national character");

            dictionary.AddType<string>(TypeIndexes.Sysname, NativeTypes.NVarchar, "sysname", TypeOptions.None);
            dictionary.AddType<string>(TypeIndexes.Xml, "xml", TypeOptions.IsLob);
            dictionary.AddType<DateTime>(TypeIndexes.Date, "date", TypeOptions.None);
            dictionary.AddType<DateTime>(TypeIndexes.Time, "time", TypeOptions.HasScale);
            dictionary.AddType<DateTime>(TypeIndexes.DateTime2, "datetime2", TypeOptions.HasScale);
            dictionary.AddType<DateTimeOffset>(TypeIndexes.DateTimeOffset, "datetimeoffset", TypeOptions.HasScale);


            dictionary.AddType(TypeIndexes.Geometry, NativeTypes.Clr, "geometry", TypeOptions.None, TypeUsageInfo.CreateClass("DbGeometry", "System.Data.Spatial"));
            dictionary.AddType(TypeIndexes.Geography, NativeTypes.Clr, "geography", TypeOptions.None, TypeUsageInfo.CreateClass("DbGeography", "System.Data.Spatial"));
            //Check this conversation for support http://entityframework.codeplex.com/discussions/415185
            dictionary.AddType(TypeIndexes.HierarchyId, NativeTypes.Clr, "hierarchyid", TypeOptions.None, TypeUsageInfo.CreateClass("SqlHierarchyId", "Microsoft.SqlServer.Types"));

            dictionary.AddType<object>(TypeIndexes.SqlVariant, "sql_variant", TypeOptions.None);
            dictionary.AddType<object>(TypeIndexes.Table, TypeOptions.IsVirtual);
            dictionary.AddType<object>(TypeIndexes.Clr, TypeOptions.IsVirtual);

            return dictionary;
        }
        #region Private methods
        private static void AddType<T>(this ITypeDictionary dictionary, short typeIndex, TypeOptions config, params string[] aliases)
        {
            AddType<T>(dictionary, typeIndex, null, config, aliases);
        }
        private static void AddType<T>(this ITypeDictionary dictionary, short typeIndex, string searchName, TypeOptions config, params string[] aliases)
        {
            AddType<T>(dictionary, typeIndex, (NativeTypes) typeIndex, searchName, config, aliases);
        }
        private static void AddType<T>(this ITypeDictionary dictionary, short typeIndex, NativeTypes baseType, string searchName, TypeOptions config, params string[] aliases)
        {
            var info = typeof (T).ToUsageInfo();
            AddType(dictionary, typeIndex, baseType, searchName, config, info, aliases);
        }
        private static void AddType(this ITypeDictionary dictionary, short typeIndex, string searchName, TypeOptions config, TypeUsageInfo info, params string[] aliases)
        {
            AddType(dictionary, typeIndex, (NativeTypes) typeIndex, searchName, config, info, aliases);
        }
        private static void AddType(this ITypeDictionary dictionary, short typeIndex, NativeTypes baseType, string searchName, TypeOptions config, TypeUsageInfo info, params string[] aliases)
        {
            AddToDictionary(dictionary, new MSSQLTypeDesc(typeIndex, baseType, searchName, config, info), aliases);
        }

        private static void AddToDictionary(ITypeDictionary dictionary, MSSQLTypeDesc desc, params string[] aliases)
        {
            List<string> names = null;
            if (!string.IsNullOrEmpty(desc.Name))
            {
                names = new List<string>(aliases.Length + 1) {desc.Name};
                names.AddRange(aliases);
            }
            dictionary.AddDesc(desc, names);
        }
        #endregion
    }
}