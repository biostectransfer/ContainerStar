using System;
using System.Collections.Generic;
using System.Reflection;

namespace MetadataLoader.Content
{
    public class ContentMapper<TContent> : IContentMapper<TContent>
    {
        private readonly IPropertyMapper<TContent>[] _propertyMappers;
        private static Func<string, object> GetConverter(Type type)
        {
            #region Nullable
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))
            {
                type = type.GetGenericArguments()[0];
            }
            #endregion
            #region Enum
            if (type.IsEnum)
            {
                return s => Enum.Parse(type, s, true);
            }
            #endregion
            #region Bool
            if (type == typeof (bool))
            {
                return s =>
                {
                    switch (s.Trim().ToLower())
                    {
                        case "0":
                        case "no":
                        case "false":
                            return false;
                        default:
                            return true;
                    }
                };
            }
            #endregion
            return s => Convert.ChangeType(s, type);
        }
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public ContentMapper(IReadOnlyCollection<string> names)
        {
            var type = typeof (TContent);
            _propertyMappers = new IPropertyMapper<TContent>[names.Count];
            var i = 0;

            foreach (var name in names)
            {
                var propMapper = PropertyMapper<TContent>.Empty;
                var info = type.GetProperty(name, BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);
                if (info != null)
                {
                    propMapper = new PropertyMapper<TContent>(info, GetConverter(info.PropertyType));
                }
                _propertyMappers[i++] = propMapper;
            }
        }
        #endregion
        #region Public methods
        public void Map(TContent content, string[] data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                var mapper = _propertyMappers[i];
                var value = data[i];
                mapper.Map(content, value);
            }
        }
        #endregion
    }
}