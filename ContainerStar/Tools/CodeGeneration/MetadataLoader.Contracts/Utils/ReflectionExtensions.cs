using System;
using System.Reflection;
using System.Text;

namespace MetadataLoader.Contracts.Utils
{
    /// <summary>
    ///     Contains common methods for work with reflection
    /// </summary>
    public static class ReflectionExtensions
    {
        public static TAtt GetAttribute<TAtt>(this MemberInfo memberInfo)
            where TAtt : Attribute
        {
            var array = memberInfo.GetCustomAttributes(typeof (TAtt), true);
            if (array.Length != 0)
            {
                return (TAtt) array[0];
            }
            return null;
        }
        public static bool HasAttribute<TAtt>(this MemberInfo memberInfo)
            where TAtt : Attribute
        {
            var array = memberInfo.GetCustomAttributes(typeof (TAtt), true);
            return array.Length != 0;
        }

        public static object GetProperty(this object obj, string name)
        {
            var propertyInfo = obj.GetType().GetProperty(name,
                BindingFlags.GetProperty |
                BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null);
        }

        public static object GetField(this object obj, string name)
        {
            var fieldInfo = obj.GetType().GetField(name,
                BindingFlags.GetProperty |
                BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);
            return fieldInfo == null ? null : fieldInfo.GetValue(obj);
        }

        public static string GetTypeFullCodeName(this Type type)
        {
            #region Check
            if (ReferenceEquals(type, null))
            {
                throw new ArgumentNullException("type");
            }
            #endregion
            var builder = new StringBuilder();
            type.GetTypeFullCodeName(builder);
            return builder.ToString();
        }
        private static void GetTypeFullCodeName(this Type type, StringBuilder builder)
        {
            if (type.IsGenericType)
            {
                var fullName = type.GetGenericTypeDefinition().FullName;
                var apostropheIndex = fullName.IndexOf('`');
                builder.Append(fullName.Remove(apostropheIndex, fullName.Length - apostropheIndex));
                builder.Append("<");
                var genericTypes = type.GetGenericArguments();
                foreach (var genericType in genericTypes)
                {
                    genericType.GetTypeFullCodeName(builder);
                    builder.Append(", ");
                }
                builder.Remove(builder.Length - 2, 2);
                builder.Append(">");
            }
            else
            {
                builder.Append(type.FullName);
            }
        }

        public static string GetTypeCodeName(this Type type)
        {
            #region Check
            if (ReferenceEquals(type, null))
            {
                throw new ArgumentNullException("type");
            }
            #endregion
            var builder = new StringBuilder();
            type.GetTypeCodeName(builder);
            return builder.ToString();
        }
        private static void GetTypeCodeName(this Type type, StringBuilder builder)
        {
            if (type.IsGenericType)
            {
                var name = type.GetGenericTypeDefinition().Name;
                var apostropheIndex = name.IndexOf('`');
                builder.Append(name.Remove(apostropheIndex, name.Length - apostropheIndex));
                builder.Append("<");
                var genericTypes = type.GetGenericArguments();
                foreach (var genericType in genericTypes)
                {
                    genericType.GetTypeCodeName(builder);
                    builder.Append(", ");
                }
                builder.Remove(builder.Length - 2, 2);
                builder.Append(">");
            }
            else
            {
                builder.Append(type.Name);
            }
        }
    }
}