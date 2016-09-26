using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Ninject;
using Ninject.Web.Common;

using SDammann.WebApi.Versioning;

using SuitsupplyTestTask.DAL;
using SuitsupplyTestTask.WebAPI;

namespace SuitsupplyTestTask
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            using (var context = new ProductsContext())
            {
                context.Database.Initialize(false);
            }
        }
    }
}
