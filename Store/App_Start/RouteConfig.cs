﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Store
{
    public class RouteConfig
    {
        

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Account",
                url: "{controller}/{action}/{id}",
                namespaces: new[] { "Store.Controllers" },
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Store",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Store", action = "GetFilters", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "AllProducts",
            url: "{controller}/{action}/{NameCategory}/{Page}",
            defaults: new { controller = "Store", action = "ProductView", Page = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "_Navigation",
            url: "{controller}/{action}/{MaxPages}/{SelectedPage}",
            defaults: new { controller = "Store", action = "_NavigationView",  SelectedPage = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "_FilterView",
            url: "{controller}/{action}/{SerializeFilters}",
            defaults: new { controller = "Store", action = "_NavigationView", SerializeFilters = UrlParameter.Optional }
            );
        }
    }
}
//SerializeFilters