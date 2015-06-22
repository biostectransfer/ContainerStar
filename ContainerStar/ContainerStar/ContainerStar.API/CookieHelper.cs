using System;
using System.Web;

namespace ContainerStar.API
{
	public static class CookieHelper
    {
		public static void AddCookie(string cookieName, string value)
		{
			var cookie = new HttpCookie(cookieName, value);
			cookie.HttpOnly = true;
			HttpContext.Current.Response.Cookies.Add(cookie);
		}

		public static void RemoveCookie(string cookieName)
		{
			var cookie = new HttpCookie(cookieName, null);
			cookie.HttpOnly = true;
			cookie.Expires = DateTime.Now.AddDays(-1d);
			HttpContext.Current.Response.Cookies.Add(cookie);
		}

		public static int? GetIntValue(string cookieName)
		{
			int? result = null;

			if(HttpContext.Current.Request.Cookies[cookieName] != null)
			{
				int temp;
				if (int.TryParse(HttpContext.Current.Request.Cookies[cookieName].Value, out temp))
					result = temp;
			}

			return result;
		}
    }
}
