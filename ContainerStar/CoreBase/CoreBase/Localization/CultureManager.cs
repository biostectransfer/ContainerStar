using System;
using System.Globalization;
using System.Threading;
using System.Web;

namespace CoreBase.Localization
{
	public class CultureManager
	{
		private static CultureManager _current;
		public static CultureManager Current
		{
			get
			{
				if (_current == null)
					_current = new CultureManager();

				return _current;
			}
		}

		private CultureManager()
		{
		}

		private const string DEFAULT_LOCALE_DE = "de-DE";
		private const string DEFAULT_LOCALE_EN = "en-US";

		public Languages CurrentLanguage 
		{
			get
			{
				return OnLanguageRequest();
			}
		}

		public CultureInfo CurrentCulture
		{
			get
			{
				return new CultureInfo(ReturnCultureNameForLanguage(CurrentLanguage));
			}
		}

		public CultureInfo GetCulture(Languages language)
		{
			return new CultureInfo(ReturnCultureNameForLanguage(language));
		}

		public void ChangeLanguage(Languages language)
		{
			OnLanguageChanged(language);
		}

		private string ReturnCultureNameForLanguage(Languages languages)
		{
            //return languages == Languages.German ? DEFAULT_LOCALE_DE : DEFAULT_LOCALE_EN;
            return languages == Languages.German ? DEFAULT_LOCALE_DE : DEFAULT_LOCALE_DE;
		}

		public delegate Languages LanguageRequestEventHandler();
		public event LanguageRequestEventHandler LanguageRequest;

		protected Languages OnLanguageRequest()
		{
			var result = Languages.German;

			if (LanguageRequest != null)
				result = LanguageRequest();

			return result;
		}

		public delegate void LanguageChangedEventHandler(Languages language);
		public event LanguageChangedEventHandler LanguageChanged;

		protected void OnLanguageChanged(Languages language)
		{
			if (LanguageChanged != null)
				LanguageChanged(language);

		}
	}
}