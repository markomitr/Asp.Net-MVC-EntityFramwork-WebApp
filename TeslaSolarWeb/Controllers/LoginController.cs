using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TeslaSolarWeb.Infrastructure;
using TeslaSolarWeb.ViewModel;

namespace TeslaSolarWeb.Controllers
{
    public class LoginController : Controller
    {
         [SelectedTab("Logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }

        [SelectedTab("Login")]
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.returnUrl = Request.QueryString["returnUrl"];
            return View(new AuthLogin());
        }
        [SelectedTab("Login")]
        [HttpPost]
        public ActionResult Login(AuthLogin form,string returnUrl)
        {
            if(!ModelState.IsValid)
                return View(form);
			///For DEMO ONLY!
			///DEMO
			///ToDo
            if (form.Username.ToLower() == "****" && form.Password.ToLower() == "*******")
            {
                form.Message = "";         
                FormsAuthentication.SetAuthCookie(form.Username, true);
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToRoute("AdminProject");
                }
            }
            else
            {
                form.Message = "Invalid username or password!";               
            }

            return View(form);
           
        }
    }
}