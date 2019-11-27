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
    public class ScheduleController : Controller
    {
        private JohnMEntities db = new JohnMEntities();

        //GET: Schedule
        public ActionResult Index()
        {
            return View();
        }

        //GET: Create/Schedule
        public ActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }

        //POST: Create/Schedule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID, Name, Description, Time")] Schedule schedule)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Schedules.Add(schedule);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateDropDownList(schedule);
            return View(schedule);
        }

        //GET: Edit/schedule
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            PopulateDropDownList(schedule);
            return View(schedule);
        }

        //POST: Edit/schedule
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var scheduleToUpdate = db.Schedules.Find(id);
            if (TryUpdateModel(scheduleToUpdate, "", new string[] { "EventID", "Name", "Description", "Time" }))
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
            PopulateDropDownList(scheduleToUpdate);
            return View(scheduleToUpdate);
        }

        //GET: Delete/schedule
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        //POST: Delete/schedule
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            try
            {
                db.Schedules.Remove(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException dex)
            {
                if (dex.InnerException.InnerException.Message.Contains("FK_"))
                {
                    ModelState.AddModelError("", "Unable to delete schedule. You cannot delete a schule that has an event in the system.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to delete schedule. Try again, and if the problem persists see your system adminstrator.");
                }
            }
            return View(schedule);
        }

        private void PopulateDropDownList(Schedule schedule = null)
        {
            ViewBag.EventID = new SelectList(db.Events
                .OrderBy(a => a.ID)
                .ThenBy(a => a.Name), "ID", "Name", schedule?.EventID);
        }
    }
}
