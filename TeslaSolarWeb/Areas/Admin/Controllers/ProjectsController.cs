using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using TeslaSolarWeb.Infrastructure;
using TeslaSolarWeb.Models;
using TeslaSolarWeb.ViewModel;

namespace TeslaSolarWeb.Areas.Admin.Controllers
{
    [SelectedTab("AdminProject")]
    [Authorize(Roles = "admin")]
    public class ProjectsController : Controller
    {
        private MyDbContex db = new MyDbContex();

        // GET: Admin/Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Admin/Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Admin/Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Caption,Description,ImageUrl")] Project project, HttpPostedFileBase projectPic)
        {
            try
            {
                if (ModelState.IsValid && (projectPic != null && projectPic.ContentLength > 0) && PictureService.DaliEslikaFormat(projectPic))
                {
                    if (String.IsNullOrEmpty(project.Caption)){ project.Caption = "Solar Project";}
                    if (String.IsNullOrEmpty(project.Description)) { project.Description = "Solar Project"; }
                    string slikaIme = "";
                    PictureService.ZacuvajSlikaPlusMala(projectPic, Server.MapPath("~/Pictures/Projects"), "Chicago_Solar_Tesla",ref slikaIme);
                    project.ImageUrl = @"~/Pictures/Projects/" +slikaIme;
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }else
                {
                    ModelState.AddModelError("ChoosePicture", "Plese choose picture for the project!");
                }
            }
            catch (Exception ex)
            {
                return Content("Greska:" + ex.Message); //privremeno
            }
            return View(project);
        }

        // GET: Admin/Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            PrictureEdit pom = new PrictureEdit();
            pom.Id = project.Id;
            pom.Caption = project.Caption;
            pom.Description = project.Description;
            pom.ImageUrl = project.ImageUrl;
            pom.ImageUrlSmall = project.ImageUrlSmall;
            return View(pom);
        }

        // POST: Admin/Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PrictureEdit project)
        {
            
            if (ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(project.Caption)) { project.Caption = "Solar Project"; }
                if (String.IsNullOrEmpty(project.Description)) { project.Description = "Solar Project"; }
                var pro = db.Projects.Find(project.Id);
                pro.Caption = project.Caption;
                pro.Description = project.Description;

                db.Entry(pro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Admin/Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Admin/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            PictureService.IzbrisiSlika(Server.MapPath("~/Pictures/Projects"), project.ImageUrl, project.ImageUrlSmall);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
