using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DTTest
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("ReturnPeopleJson",
                    "people/getJsondata",
                    new { controller = "Home", action = "ReturnPeopleJson" },
                    new[] { "DTTest.Controllers" });

            routes.MapRoute(
                null, "", // Only matches the empty URL (i.e. /) 
                new
                {
                    controller = "Home",
                    action = "Index",
                    race = 1,
                    page = 1
                }
             );

            routes.MapRoute(
                null,
                "Page{page}", // Matches /Page2, /Page123, but not /PageXYZ 
                new { controller = "Home", action = "Index", race = 1 },
                new { page = @"\d+" } // Constraints: page must be numerical 
            );

            routes.MapRoute(null,
                "{race}", // Matches /race
                new { controller = "Home", action = "Index", page = 1 }
            );

            routes.MapRoute(null,
                "{race}/Page{page}", // Matches /race/Page567 
                new { controller = "Home", action = "Index" }, // Defaults 
                new { page = @"\d+" } // Constraints: page must be numerical 
            );

            routes.MapRoute(null, "{controller}/{action}");             
        }
    }
}