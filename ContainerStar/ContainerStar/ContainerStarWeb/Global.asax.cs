using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ContainerStarWeb
{
    public class MasterDataModuleWebApplication : HttpApplication
    {
        protected void Application_Start()
        {
			AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
			GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
