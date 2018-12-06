
using Newtonsoft.Json.Serialization;
using Store.Controllers;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Store.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{img}",
                defaults: new { controller="AccountApi", action="UploadImage", img = RouteParameter.Optional }
            );
            //JsonMediaTypeFormatter jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
        }
    }
}