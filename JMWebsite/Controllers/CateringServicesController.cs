using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JMWebsite.DAL.JMEntities;
using JMWebsite.Models;
using JMWebsite.Infrastructure;

namespace JMWebsite.Controllers
{
    [Authorize]
    public class CateringServicesController : Controller
    {
        private JohnMEntities db = new JohnMEntities();

        // GET: CateringServices
        public ActionResult Index()
        {
            var cateringServices = db.CateringServices.Include(c => c._event);
            return View(cateringServices.ToList());
        }

        // GET: CateringServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CateringService cateringService = db.CateringServices.Where(s => s.EventID == id).SingleOrDefault();
            if (cateringService == null)
            {
                return HttpNotFound();
            }
            return View(cateringService);
        }

        // GET: CateringServices/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events.Include(s => s.cateringService).Where(s => s.cateringService == null), "ID", "Name");
            return View();
        }

        // POST: CateringServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,Description")] CateringService cateringService)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CateringServices.Add(cateringService);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex)
            {
                if (dex.InnerException.InnerException.Message.Contains("IX_"))
                {
                    ModelState.AddModelError("Name", "Unable to save changes. Remember, you cannot upload the same file twice or another file using the same file name.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "Name", cateringService.EventID);
            return View(cateringService);
        }

        // GET: CateringServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CateringService cateringService = db.CateringServices.Where(s => s.EventID == id).SingleOrDefault();
            if (cateringService == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "Name", cateringService.EventID);
            return View(cateringService);
        }

        // POST: CateringServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,Description")] CateringService cateringService)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cateringService).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex)
            {
                if (dex.InnerException.InnerException.Message.Contains("IX_"))
                {
                    ModelState.AddModelError("Name", "Unable to save changes due to constraint.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "Name", cateringService.EventID);
            return View(cateringService);
        }

        // GET: CateringServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CateringService cateringService = db.CateringServices.Where(s => s.EventID == id).SingleOrDefault();
            if (cateringService == null)
            {
                return HttpNotFound();
            }
            return View(cateringService);
        }

        // POST: CateringServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CateringService cateringService = db.CateringServices.Where(s => s.EventID == id).SingleOrDefault();
            try
            {
                db.CateringServices.Remove(cateringService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to delete schedule. Try again, and if the problem persists see your system administrator.");
            }
            return View(cateringService);
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
