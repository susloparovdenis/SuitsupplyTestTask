using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SuitsupplyTestTask.WebAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Help", action = "Index", id = UrlParameter.Optional, area = "HelpPage" },
                namespaces: new[] { "SuitsupplyTestTask.WebAPI.Areas.HelpPage.Controllers" }
            ).DataTokens.Add("area", "HelpPage"); ;
        }
    }
}
