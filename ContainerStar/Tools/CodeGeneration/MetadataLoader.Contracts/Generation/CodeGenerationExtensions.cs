using System;
using System.Diagnostics.Contracts;

namespace MetadataLoader.Contracts.Generation
{
    public static class CodeGenerationExtensions
    {
        public static string Postfix(this string value, string append = " ")
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return value + append;
        }
        public static string Prefix(this string value, string prepend = " ")
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return prepend + value;
        }

        public static string GenerateUniqueName(this string value, Func<string, bool> checkDuplicates)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(value));
            Contract.Requires(checkDuplicates != null);

            var index = 2;
            var result = value;
            while (checkDuplicates(result))
            {
                result = value + index;
                index++;
            }
            return result;
        }
    }
}