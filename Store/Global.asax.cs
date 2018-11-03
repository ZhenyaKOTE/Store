using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Store.Models;
using Store.Util;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Store
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var UserInfo = Request.Cookies[FormsAuthentication.FormsCookieName];
                
            if (UserInfo != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(UserInfo.Value);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);

                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                newUser.Name = serializeModel.Name;
                newUser.Email = serializeModel.Email;

                Debug.Write(newUser.Name);

                HttpContext.Current.User = newUser;
            }
        }

    }
}
