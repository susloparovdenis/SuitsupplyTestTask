using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;

using SDammann.WebApi.Versioning;

using SuitsupplyTestTask.DAL;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SuitsupplyTestTask.WebAPI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SuitsupplyTestTask.WebAPI.App_Start.NinjectWebCommon), "Stop")]

namespace SuitsupplyTestTask.WebAPI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<IHttpControllerSelector>().ToConstant(new RouteVersionedControllerSelector(GlobalConfiguration.Configuration)).InSingletonScope();
            kernel.Bind<IApiExplorer>().ToConstant(new VersionedApiExplorer(GlobalConfiguration.Configuration)).InSingletonScope();
        }
    }
}
