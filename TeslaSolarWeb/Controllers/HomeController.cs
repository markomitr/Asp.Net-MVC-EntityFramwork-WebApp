using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TeslaSolarWeb.Infrastructure;
using TeslaSolarWeb.ViewModel;

namespace TeslaSolarWeb.Controllers
{
    
    public class HomeController : Controller
    {
        [SelectedTab("Home")]
        public ActionResult Index()
        {
            return View();
        }
        [SelectedTab("About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [SelectedTab("Contact")]
        [HttpGet]
        public ActionResult Contact(string EmailSent)
        {
            ViewBag.Message = "Your contact page.";

            if(!String.IsNullOrWhiteSpace(EmailSent))
            {
                if(EmailSent=="1")
                {

                    return View(new EmailMsg { IsSent = true, Poraka = "The message is sent. Thank you." });
                }
            }

            return View(new EmailMsg());
        }
        [SelectedTab("Contact")]
        [HttpPost]
        public ActionResult Contact(EmailMsg msg)
        {
            ViewBag.Message = "Your email page.";
            if(ModelState.IsValid)
            {
                msg.IsSent = EmailService.SendEmail(ref msg);
                if (msg.IsSent)
                {                   
                    msg = new EmailMsg();
                    msg.Poraka = "The message is sent. Thank you.";
                    msg.IsSent = true;
                    return RedirectToRoute("Contact", new { EmailSent=1 });
                }
                else
                {
                    msg.Poraka = " Ooops, we have a problem.Message NOT sent. Try again.";
                }
            }
            return View(msg);
        }
       
    }
}