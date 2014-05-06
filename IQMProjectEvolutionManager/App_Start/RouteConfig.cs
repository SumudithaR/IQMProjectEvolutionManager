using System.Web.Mvc;
using System.Web.Routing;

namespace IQMProjectEvolutionManager
{
    /// <summary>
    /// Route Configuration
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Project", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "IQMProjectEvolutionManager.Controllers" });
        }
    }
}