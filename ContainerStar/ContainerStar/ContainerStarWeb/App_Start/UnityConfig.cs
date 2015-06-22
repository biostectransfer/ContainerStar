using Microsoft.Practices.Unity;
using System.Web.Http;
using ContainerStar.Configuration;
using Unity.WebApi;

namespace ContainerStarWeb
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            UnityConfiguration.ConfigureContainer(container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}