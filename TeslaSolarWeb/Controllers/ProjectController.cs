using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeslaSolarWeb.Infrastructure;
using TeslaSolarWeb.Models;

namespace TeslaSolarWeb.Controllers
{
      

    [SelectedTab("Project")]
    public class ProjectController : Controller
    {
        private MyDbContex db = new MyDbContex();

        public ActionResult ListProject()
        {
            return View(db.Projects.ToList());
        }
	}
}