using System.Configuration;

namespace CoreBase
{
    public static class ConfigHelper
    {
        public static string RentOrderPreffix
        {
            get { return ConfigurationManager.AppSettings["RentOrderNumberPreffix"]; }
        }
    }
}