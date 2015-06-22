using System.Text;
namespace ContainerStar.API
{
    public static class Configuration
    {
        public static string RentOrderFileName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RentOrderFileName"];
            }
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