using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreBase.Json
{
	public class JsonOptionalValue
	{
		public JsonOptionalValue(object value, Func<bool> isRequiredHandler)
		{
			Value = value;
			IsRequiredHandler = isRequiredHandler;
		}

		public object Value
		{
			get;
			private set;
		}

		public bool IsRequired
		{
			get
			{
				var result = Value != null;

				if (IsRequiredHandler != null)
					result = IsRequiredHandler();

				return result;
			}
		}

		private Func<bool> IsRequiredHandler
		{
			get;
			set;
		}
	}
}