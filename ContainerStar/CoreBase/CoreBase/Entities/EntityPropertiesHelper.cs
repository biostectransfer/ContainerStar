using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace CoreBase.Entities
{
    /// <summary>
    /// Entity roperty helper
    /// </summary>
    public sealed class EntityPropertiesHelper
    {
        /// <summary>
        /// Extract property
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static Dictionary<string, string> Extract(params Expression<Func<string>>[] fields)
        {
            var result = new Dictionary<string, string>(fields.Length);
            foreach (var expression in fields)
            {
                if (expression.Body.NodeType != ExpressionType.MemberAccess)
                {
                    throw new ApplicationException("Not supported expression");
                }
                var memberInfo = ExtractMember(expression);
                result.Add(memberInfo.Name, (string)((FieldInfo)memberInfo).GetValue(null));
            }
            return result;
        }
        /// <summary>
        /// Extract property
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string ExtractPropertyName(Expression<Func<string>> expression)
        {
            var memberInfo = ExtractMember(expression);
            return memberInfo.Name;
        }

        private static MemberInfo ExtractMember(Expression<Func<string>> expression)
        {
            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ApplicationException("Not supported expression");
            }
            var body = (MemberExpression) expression.Body;
            if (body.Member.MemberType != MemberTypes.Field)
            {
                throw new ApplicationException("Not supported expression");
            }
            return body.Member;
        }
    }
}