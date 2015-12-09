using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Assignment
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(    
                name: "caughtNotification",
                url: "Notification/caughtNotification/{notificationID}",
                defaults: new { controller = "Notification", action = "caughtNotification" }
            );

            routes.MapRoute(
                name: "Accept",
                url: "Nomination/Accept/{noticeId}",
                defaults: new { controller = "Nomination", action = "Accept" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
