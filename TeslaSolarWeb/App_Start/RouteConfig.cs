using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TeslaSolarWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("AdminProject", "Admin/Projects", new { controller = "Projects", action = "Index" });
            routes.MapRoute("Logout", "Logout", new { controller = "Login", action = "Logout" });
            routes.MapRoute("Login", "Login", new { controller = "Login", action = "Login" });
            routes.MapRoute("Email", "Email", new { controller = "Home", action = "Email" });
            routes.MapRoute("ProjectsList", "ProjectList", new { controller = "Project", action = "ListProject" });
            routes.MapRoute("Projects", "Project", new { controller = "Project", action = "ListProject" });
            routes.MapRoute("About","About",new { controller = "Home", action = "About"});
            routes.MapRoute("Contact","Contact",new { controller = "Home", action = "Contact"});
            routes.MapRoute("Home", "", new { controller = "Home", action = "Index" });
        }
    }
}
