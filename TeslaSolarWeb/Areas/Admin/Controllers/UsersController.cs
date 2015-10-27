using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeslaSolarWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
       
        public ActionResult Index()
        {
            return Content("Users");
        }
    }
}