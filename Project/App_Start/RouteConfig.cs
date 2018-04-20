using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HandsOn
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "HandsOn.Controllers" }
            );

            routes.MapRoute(
                name: "Error",
                url: "{controller}/{action}/{message}",
                defaults: new { controller = "Home", action = "Error", message = UrlParameter.Optional }
            );
        }
    }
}
