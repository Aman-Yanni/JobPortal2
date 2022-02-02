using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPortal2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace JobPortal2.Controllers
{
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jobs
        public ActionResult Index()
        {
            ApplicationUser applicationUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var currentUser = db.Users.Find(applicationUser.Id);
            ViewBag.applicants = db.Applications.Where(row=>row.userId.Id==currentUser.Id).ToList();
            return View(db.Job.Where(row=>row.UserID.Id!= currentUser.Id && row.Active).ToList());
        }

        public ActionResult MyJobs()
        {
            ApplicationUser applicationUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var currentUser = db.Users.Find(applicationUser.Id);
            return View(db.Job.Where(row => row.UserID.Id== currentUser.Id).ToList());
        }

        public ActionResult MyApplications()
        {
            ApplicationUser applicationUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var currentUser = db.Users.Find(applicationUser.Id);
            ViewBag.jobs = db.Job.ToList();
            return View(db.Applications.Where(row => row.userId.Id == currentUser.Id).ToList());
        }
        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            ApplicationUser applicationUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var currentUser = db.Users.Find(applicationUser.Id);
            ViewBag.applicants = db.Applications.Where(row => row.jobId.JobID == id).ToList();
            ViewBag.user = currentUser;
            ViewBag.users = db.Users.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Job.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Job job)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                var currentUser = db.Users.Find(applicationUser.Id);
                bool updated = currentUser.degree != 0 && currentUser.experience != 0 && currentUser.field != 0;
                if (updated)
                {
                    job.UserID = currentUser;
                    job.Active = true;
                    db.Job.Add(job);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Edit", "Manage");
                }
            }

            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Job.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Job.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Job.Find(id);
            db.Job.Remove(job);
            db.SaveChanges();
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

        
        public ActionResult Apply(int id)
        {

            ApplicationUser applicationUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var currentUser = db.Users.Find(applicationUser.Id);
            bool updated = currentUser.degree!=0 && currentUser.experience != 0 && currentUser.field != 0;
            if (updated)
            {
                Applicants apply = new Applicants();
                apply.date = DateTime.Now;
                apply.jobId = db.Job.Find(id);
                apply.userId = currentUser;
                apply.status = Status.Pending;
                db.Applications.Add(apply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit", "Manage");
            }

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Job job = db.Job.Find(id);
            //if (job == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(job);
        }



        public ActionResult Accept(int id)
        {
            Applicants application = db.Applications.Find(id);
            application.status = Status.Accepted;
            db.SaveChanges();
            return RedirectToAction("Details", new {id=application.jobId.JobID});
        }
        public ActionResult AcceptAndClose(int id) 
        {
            Applicants application = db.Applications.Find(id);
            application.status = Status.Accepted;
            Job job = db.Job.Find(application.jobId.JobID);
            job.Active = false;
            db.SaveChanges();
            return RedirectToAction("MyJobs");
        }
        public ActionResult Reject(int id)
        {
            Applicants application = db.Applications.Find(id);
            application.status = Status.Rejected;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = application.jobId.JobID });
        }
    }
}
