
using System.Diagnostics;
using System.Web.Http;

namespace Store.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {controller="DefaultApi", id = RouteParameter.Optional }
            );
        }
    }
}