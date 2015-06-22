using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CoreBase
{
    public class StringHelper
    {
        public static string GetMD5Hash(string value)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }

		public static string UniqueCode()
		{
			string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#";
			string ticks = DateTime.UtcNow.Ticks.ToString();
			var code = "";
			for (var i = 0; i < characters.Length; i += 2)
			{
				if ((i + 2) <= ticks.Length)
				{
					var number = int.Parse(ticks.Substring(i, 2));
					if (number > characters.Length - 1)
					{
						var one = double.Parse(number.ToString().Substring(0, 1));
						var two = double.Parse(number.ToString().Substring(1, 1));
						code += characters[Convert.ToInt32(one)];
						code += characters[Convert.ToInt32(two)];
					}
					else
						code += characters[number];
				}
			}
			return code;
		}

        public static string GetTextForCurrentLocale(string text, CultureInfo culture)
        {
            var description = text.Replace("DE: ", "DE:").Replace("EN: ", "EN:");

            var searchLanguage = "DE:";
            var cutLanguage = "EN:";
            if (culture.Name == "en-US")
            {
                searchLanguage = "EN:";
                cutLanguage = "DE:";
            }

            var parts = description.Split(new[] { searchLanguage }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0)
            {
                if (parts.Length == 1)
                {
                    if (!parts.ElementAt(0).Contains(cutLanguage))
                    {
                        description = parts.ElementAt(0);
                    }
                    else
                    {
                        var cutPart = parts.ElementAt(0).Split(new[] { cutLanguage }, StringSplitOptions.RemoveEmptyEntries);
                        if (cutPart.ElementAt(0).Contains(cutLanguage))
                        {
                            description = cutPart.ElementAt(1);
                        }
                        else
                        {
                            description = cutPart.ElementAt(0);
                        }
                    }
                }
                else
                {
                    if (parts.ElementAt(0).Contains(cutLanguage))
                    {
                        description = parts.ElementAt(1);
                    }
                    else
                    {
                        description = parts.ElementAt(0);
                    }
                }
            }

            return description.Replace("DE:", "").Replace("EN:", "");
        }
    }
}
