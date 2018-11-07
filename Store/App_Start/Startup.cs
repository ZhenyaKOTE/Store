using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Store.BLL.Services;
using Microsoft.AspNet.Identity;
using Store.BLL.Interfaces;
using System.Web.Http;

[assembly: OwinStartup(typeof(Store.App_Start.Startup))]

namespace Store.App_Start
{
    public class Startup
    {
        Creator serviceCreator = new ServiceCreator();
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
            return serviceCreator.CreateUserService("entityFramework");
        }



    }
}