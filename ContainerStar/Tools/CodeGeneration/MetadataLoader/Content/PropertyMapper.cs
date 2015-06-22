using System;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace MetadataLoader.Content
{
    public class PropertyMapper<TContent> : IPropertyMapper<TContent>
    {
        public static readonly IPropertyMapper<TContent> Empty = new EmptyPropertyMapper();
        #region	Private fields
        private readonly PropertyInfo _info;
        private readonly Func<string, object> _converter;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public PropertyMapper(PropertyInfo info, Func<string, object> converter)
        {
            Contract.Requires(info != null);
            Contract.Requires(converter != null);

            _info = info;
            _converter = converter;
        }
        #endregion
        #region	Public methods
        public void Map(TContent content, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var val = _converter(value);
            _info.SetValue(content, val);
        }
        #endregion
        #region Nested type: EmptyPropertyMapper
        private sealed class EmptyPropertyMapper : IPropertyMapper<TContent>
        {
            public void Map(TContent content, string value)
            {
            }
        }
        #endregion
    }
}