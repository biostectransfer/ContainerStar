using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace MetadataLoader.Contracts.CSharp
{
    public static class TypeUsageInfoExtensions
    {
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, TypeUsageInfo> Dictionary;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        static TypeUsageInfoExtensions()
        {
            var list = new List<KeyValuePair<RuntimeTypeHandle, TypeUsageInfo>>
            {
                typeof (char).Generate("char"),
                typeof (string).Generate("string"),
                typeof (bool).Generate("bool"),
                typeof (byte).Generate("byte"),
                typeof (short).Generate("short"),
                typeof (int).Generate("int"),
                typeof (long).Generate("long"),
                typeof (sbyte).Generate("sbyte"),
                typeof (ushort).Generate("ushort"),
                typeof (uint).Generate("uint"),
                typeof (ulong).Generate("ulong"),
                typeof (decimal).Generate("decimal"),
                typeof (float).Generate("float"),
                typeof (double).Generate("double"),
                typeof (object).Generate("object")
            };
            Dictionary = new ConcurrentDictionary<RuntimeTypeHandle, TypeUsageInfo>(list);
        }
        public static TypeUsageInfo ToUsageInfo(this Type type)
        {
            return ToUsageInfo(type, null);
        }
        private static TypeUsageInfo ToUsageInfo(this Type type, string shortName)
        {
            return Dictionary.GetOrAdd(type.TypeHandle, handle => GenerateTypeUsageInfo(type, shortName));
        }
        public static TypeUsageInfo ToGenericUsageInfo(this Type type, params TypeUsageInfo[] typeArguments)
        {
            Contract.Requires(typeArguments != null);
            Contract.Requires(typeArguments.Length != 0);

            if (!type.IsGenericTypeDefinition || type.GetGenericArguments().Length != typeArguments.Length)
            {
                throw new ApplicationException("Wrong usage of method. Use generic type definition with one type argument");
            }
            return CreateTypeUsageInfo(type, typeArguments);
        }
        private static KeyValuePair<RuntimeTypeHandle, TypeUsageInfo> Generate(this Type type, string shortName)
        {
            return new KeyValuePair<RuntimeTypeHandle, TypeUsageInfo>(type.TypeHandle, GenerateTypeUsageInfo(type, shortName));
        }
        private static TypeUsageInfo GenerateTypeUsageInfo(this Type type, string shortName)
        {
            Contract.Requires(type != null);
            Contract.Requires(!type.IsGenericTypeDefinition);
            #region Array
            if (type.IsArray)
            {
                var arrayRank = type.GetArrayRank();
                type = type.GetElementType();
                return TypeUsageInfo.CreateArray(type.ToUsageInfo(), arrayRank);
            }
            #endregion
            #region Nullable
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))
            {
                type = type.GetGenericArguments()[0];
                return TypeUsageInfo.CreateNullable(type.ToUsageInfo());
            }
            #endregion
            var typeParameters = type.IsGenericType ? type.GetGenericArguments().Select(ToUsageInfo).ToArray() : null;
            return CreateTypeUsageInfo(type.IsGenericType ? type.GetGenericTypeDefinition() : type, typeParameters, shortName);
        }
        private static TypeUsageInfo CreateTypeUsageInfo(Type type, TypeUsageInfo[] typeArguments = null, string shortName = null)
        {
            var name = type.Name;
            if (type.IsGenericTypeDefinition)
            {
                var index= name.IndexOf('`');
                if (index != -1)
                {
                    name = name.Substring(0, index);
                }
            }
            var typeNamespace = type.Namespace;

            if (type.IsEnum)
            {
                return TypeUsageInfo.CreateEnum(name, typeNamespace);
            }

            if (type.IsGenericTypeDefinition && (typeArguments == null ||
                                                 type.GetGenericArguments().Length != typeArguments.Length))
            {
                throw new ArgumentOutOfRangeException("typeArguments");
            }

            if (type.IsClass)
            {
                return TypeUsageInfo.Create(name, typeNamespace, TypeUsageInfoConfiguration.Class, shortName, typeArguments);
            }
            if (type.IsInterface)
            {
                return TypeUsageInfo.Create(name, typeNamespace, TypeUsageInfoConfiguration.Interface, shortName, typeArguments);
            }
            if (type.IsValueType)
            {
                return TypeUsageInfo.Create(name, typeNamespace, TypeUsageInfoConfiguration.ValueType, shortName, typeArguments);
            }
            throw new NotSupportedException(string.Format("Can't convert type '{0}' to TypeUsageInfo", type.FullName));
        }
    }
}