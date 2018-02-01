using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using CatStore.BLL.Infrastructure;
using CatStore.WEB.Util;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CatStore.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule orderModule = new OrderModule();
            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            var kernel = new StandardKernel(orderModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
