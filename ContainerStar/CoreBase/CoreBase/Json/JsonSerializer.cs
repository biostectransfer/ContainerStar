using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoreBase.Json
{
	public static class JsonSerializer
	{
		/// <summary>
		/// Конвертация значения в значение json параметра.
		/// </summary>
		private static string ConvertValueToJson(object value)
		{
			var formatProvider = CultureInfo.InvariantCulture.NumberFormat;

			var result = new StringBuilder();

			if (value == null)
			{
				result.Append("null");
			}
			else if (value is bool)
			{
				result.Append(value.ToString().ToLowerInvariant());
			}
			else if (value is byte)
			{
				result.Append(((byte)value).ToString(formatProvider));
			}
			else if (value is int)
			{
				result.Append(((int)value).ToString(formatProvider));
			}
			else if (value is double)
			{
				result.Append(((double)value).ToString(formatProvider));
			}
			else if (value is decimal)
			{
				result.Append(((decimal)value).ToString(formatProvider));
			}
			else if (value is string || value is char || value.GetType().IsEnum)
			{
				var stringValue = value.ToString().Replace("\"", "\\\"");
				result.AppendFormat("\"{0}\"", stringValue);
			}
			else if (value is DateTime)
			{
				result.AppendFormat("\"{0}\"", value);
			}
			else if (value is IDictionary)
			{
				result.Append(ToJson((IDictionary)value));
			}
			else if (value is IEnumerable)
			{
				result.Append(ToJson((IEnumerable)value));
			}			
			else if (value is JsonRawValue)
			{
				var rawValue = (JsonRawValue)value;
				result.Append(rawValue.Value);
			}
			else if (value.GetType().IsClass)
			{
				result.Append(ToJson(value));
			}
			else
				result.Append(value);

			return result.ToString();
		}

		public static string ToJson(IEnumerable objCollection)
		{
			if (objCollection == null)
				return ToJson((object)objCollection);

			if (objCollection is IDictionary)
				return ToJson((IDictionary)objCollection);

			var result = new StringBuilder();

			foreach (var obj in objCollection)
			{
				if (result.Length != 0)
					result.Append(",");

				result.Append(ConvertValueToJson(obj));
			}

			result.Insert(0, "[");
			result.Append("]");

			return result.ToString();
		}

		public static string ToJson(object obj)
		{
			if (obj == null || obj.GetType().IsValueType || obj.GetType().IsEnum || obj.GetType().IsPrimitive || 
				obj is JsonRawValue || obj is IEnumerable || obj is IDictionary || obj is string)
				return ConvertValueToJson(obj);

			var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var mapping = properties.ToDictionary(propertyInfo => propertyInfo.Name, propertyInfo => propertyInfo.Name);

			return ToJson(obj, mapping);
		}

		public static string ToJson(IEnumerable sourceCollection, Dictionary<string, string> mapping)
		{
			var result = new StringBuilder();

			if (sourceCollection != null)
			{
				foreach (var item in sourceCollection)
				{
					if (result.Length != 0)
						result.Append(",");

					result.Append(ToJson(item, mapping));
				}

				result.Insert(0, "[");
				result.Append("]");
			}
			else
			{
				result.Append("null");
			}

			return result.ToString();
		}

		public static string ToJson(object obj, Dictionary<string, string> mapping)
		{
			var objDescription = new Dictionary<string, object>();

			foreach (var propertyMapping in mapping)
			{
				var jsFieldName = propertyMapping.Key;
				var propertyValue = GetPropertyValue(obj, propertyMapping.Value);

				objDescription.Add(jsFieldName, propertyValue);
			}

			return ToJson(objDescription);
		}

		public static string ToJson(IDictionary dictionary)
		{
			var result = new StringBuilder();

			foreach (DictionaryEntry entry in dictionary)
			{
				var isRequired = true;
				var value = entry.Value;

				if (value is JsonOptionalValue)
				{
					var optionalValue = (JsonOptionalValue)value;

					isRequired = optionalValue.IsRequired;
					value = optionalValue.Value;
				}

				if (isRequired)
				{
					if (result.Length != 0)
						result.Append(", ");
					result.AppendFormat("\"{0}\": {1}", entry.Key, ConvertValueToJson(value));
				}
			}

			result.Insert(0, "{");
			result.Append("}");

			return result.ToString();
		}


		/// <summary>
		/// Преобразование enum типа в json формат.
		/// </summary>
		public static string ToJsonEnum<TEnum>()
		{
			if (!typeof(TEnum).IsEnum)
				throw new InvalidCastException(string.Format("Type is not enum."));

			var result = new StringBuilder();

			var values = Enum.GetValues(typeof(TEnum));
			foreach (var enmObject in values)
			{
				if(result.Length != 0)
					result.Append(", ");

				result.Append("{");
				result.AppendFormat("id: {0},", enmObject.GetHashCode());
				result.AppendFormat("name: '{0}'", enmObject);
				result.Append("}");
			}

			result.Insert(0, "[");
			result.Append("]");

			return result.ToString();
		}

		/// <summary>
		/// Получить значение свойства из объекта.
		/// Если свойство не задано и объект - CBizObject, то берется значение Id.
		/// </summary>
		private static object GetPropertyValue(object dataItem, string fieldName)
		{
			if (String.IsNullOrWhiteSpace(fieldName))
				throw new ArgumentException("Parameter cannot be empty or null.", "fieldName");

			var type = dataItem.GetType();
			var propertyInfo = type.GetProperty(fieldName);

			return propertyInfo.GetValue(dataItem, null);
		}
	}
}