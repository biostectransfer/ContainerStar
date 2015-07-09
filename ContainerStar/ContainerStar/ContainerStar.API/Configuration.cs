using System.Configuration;
using System.Text;
namespace ContainerStar.API
{
    public static class Configuration
    {
        public static string RentOrderFileName
        {
            get
            {
                return ConfigurationManager.AppSettings["RentOrderFileName"];
            }
        }

        public static string OfferFileName
        {
            get
            {
                return ConfigurationManager.AppSettings["OfferFileName"];
            }
        }

        public static string InvoiceFileName
        {
            get
            {
                return ConfigurationManager.AppSettings["InvoiceFileName"];
            }
        }

        public static string ReminderFileName
        {
            get
            {
                return ConfigurationManager.AppSettings["ReminderFileName"];
            }
        }

        public static string RentOrderPreffix
        {
            get { return ConfigurationManager.AppSettings["RentOrderNumberPreffix"]; }
        }

        public static string CombineUrl(params string[] parts)
        {
            if (parts == null)
                return string.Empty;

            var builder = new StringBuilder();
            foreach (var part in parts)
            {
                builder.Append(part.Trim('/'));
                builder.Append("/");
            }

            var result = builder.ToString();
            if (!result.StartsWith("/"))
                result = "/" + result;

            return result;
        }
    }
}