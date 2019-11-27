using JMWebsite.DAL.JMEntities;
using JMWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JMWebsite.Controllers
{
    public class CateringServiceController : Controller
    {
        private JohnMEntities db = new JohnMEntities();

        //GET: Catering
        public ActionResult Index()
        {
            return View();
        }

        //GET: Create/Catering
        public ActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }

        //POST: Create/Catering
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID, Name, Time")] CateringService cateringService)
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
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateDropDownList(cateringService);
            return View(cateringService);
        }

        //GET: Edit/Catering
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CateringService cateringService = db.CateringServices.Find(id);
            if (cateringService == null)
            {
                return HttpNotFound();
            }
            PopulateDropDownList(cateringService);
            return View(cateringService);
        }

        //POST: Edit/Catering
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cateringToUpdate = db.CateringServices.Find(id);
            if (TryUpdateModel(cateringToUpdate, "", new string[] { "EventID", "Name", "Time" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            PopulateDropDownList(cateringToUpdate);
            return View(cateringToUpdate);
        }

        //GET: Delete/Catering
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CateringService cateringService = db.CateringServices.Find(id);
            if (cateringService == null)
            {
                return HttpNotFound();
            }
            return View(cateringService);
        }

        //POST: Delete/Catering
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CateringService cateringService = db.CateringServices.Find(id);
            try
            {
                db.CateringServices.Remove(cateringService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException dex)
            {
                if (dex.InnerException.InnerException.Message.Contains("FK_"))
                {
                    ModelState.AddModelError("", "Unable to delete catering service. You cannot delete a catering service that has an event in the system.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to delete catering service. Try again, and if the problem persists see your system adminstrator.");
                }
            }
            return View(cateringService);
        }

    private void PopulateDropDownList(CateringService cateringService = null)
            {
                ViewBag.EventID = new SelectList(db.Events
                    .OrderBy(a => a.ID)
                    .ThenBy(a => a.Name), "ID", "Name", cateringService?.EventID);
            }
        }
    }
