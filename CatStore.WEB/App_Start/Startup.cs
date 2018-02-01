using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using CatStore.BLL.Services;
using Microsoft.AspNet.Identity;
using CatStore.BLL.Interfaces;

[assembly: OwinStartup(typeof(CatStore.WEB.App_Start.Startup))]

namespace CatStore.WEB.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IService CreateUserService()
        {
            var sc = new CatService();
            return sc.CreateService("DefaultConnection");
        }
    }
}