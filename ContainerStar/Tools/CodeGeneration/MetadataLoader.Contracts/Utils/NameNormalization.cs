using System.Linq;
using System.Text;
using Microsoft.CSharp;

namespace MetadataLoader.Contracts.Utils
{
    public static class NameNormalization
    {
        #region	Private fields
        private static readonly CSharpCodeProvider Code = new CSharpCodeProvider();
        #endregion
        #region	Public methods
        public static string Direct(this string name)
        {
            return Code.CreateValidIdentifier(name);
        }

        /// <summary>
        ///     Camel case started from capital char
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            var parts = name.Trim(' ', '_', '-').Split(' ', '_', '-');
            var sbuilder = new StringBuilder();
            foreach (var part in parts)
            {
                var current = part;
                if (part.All(char.IsUpper))
                {
                    current = string.Empty;
                    current += char.ToUpperInvariant(part[0]);
                    current += part.ToLowerInvariant().Substring(1);
                }
                sbuilder.Append(current);
            }
            return Code.CreateValidIdentifier(sbuilder.ToString());
        }
        /// <summary>
        ///     Camel case started from lower char
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string name)
        {
            name = ToPascalCase(name);
            if (!string.IsNullOrEmpty(name))
            {
                return char.ToLower(name[0]) + name.Remove(0, 1);
            }
            return name;
        }
        #endregion
    }
}