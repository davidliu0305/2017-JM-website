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
using JMWebsite.ViewModels;
using JMWebsite.Infrastructure;

namespace JMWebsite.Controllers
{
    [Authorize]
    public class AttendeesController : Controller
    {
        private JohnMEntities db = new JohnMEntities();

        // GET: Attendees
        public ActionResult Index()
        {
            return View(db.Attendees.Include(a => a.Events).ToList());
        }

        // GET: Attendees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendee attendee = db.Attendees.Include(g => g.Events)
                .Where(g => g.ID == id).SingleOrDefault(); ;
            if (attendee == null)
            {
                return HttpNotFound();
            }
            return View(attendee);
        }

        // GET: Attendees/Create
        public ActionResult Create()
        {
            var attendee = new Attendee();
            attendee.Events = new List<Event>();
            AssignEvents(attendee);
            return View();
        }

        // POST: Attendees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,MiddleName,LastName,Role,Email")] Attendee attendee, string[] chkEvents)
        {
            try
            {
                AddAttendeeEvents(attendee, chkEvents);
                if (ModelState.IsValid)
                {
                    db.Attendees.Add(attendee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            AssignEvents(attendee);
            return View(attendee);
        }

        // GET: Attendees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendee attendee = db.Attendees.Find(id);
            if (attendee == null)
            {
                return HttpNotFound();
            }
            AssignEvents(attendee);
            return View(attendee);
        }

        // POST: Attendees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, string[] chkEvents)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var attendeeToUpdate = db.Attendees.Include(s => s.Events).Where(s => s.ID == id).
                FirstOrDefault();
            if (TryUpdateModel(attendeeToUpdate, "",
               new string[] { "FirstName", "MiddleName", "LastName", "Role", "Email" }))
            {
                try
                {
                    UpdateAttendeeEvents(attendeeToUpdate, chkEvents);
                    db.Entry(attendeeToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            AssignEvents(attendeeToUpdate);
            return View(attendeeToUpdate);
        }
        //public ActionResult Edit([Bind(Include = "ID,FirstName,MiddleName,LastName,Role,Email")] Attendee attendee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(attendee).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(attendee);
        //}

        // GET: Attendees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendee attendee = db.Attendees.Find(id);
            if (attendee == null)
            {
                return HttpNotFound();
            }
            return View(attendee);
        }

        // POST: Attendees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendee attendee = db.Attendees.Find(id);
            try
            {
                db.Attendees.Remove(attendee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to delete attendee. Try again, and if the problem persists see your system administrator.");
            }
            return View(attendee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private void AssignEvents(Attendee Attendee)
        {
            var EventList = db.Events;
            var AssignedEvents = new HashSet<int>(Attendee.Events.Select(s => s.ID));
            var View = new List<AttendeeEventVM>();

            foreach (var s in EventList)
            {
                View.Add(new AttendeeEventVM
                {
                    EventID = s.ID,
                    _event = s.Name,
                    HasEvent = AssignedEvents.Contains(s.ID)
                });
            }
            ViewBag.Events = View;
        }
        private void AddAttendeeEvents(Attendee attendee, string[] chkEvents)
        {
            if (chkEvents != null)
            {
                attendee.Events = new List<Event>();

                var NewEvents = new HashSet<string>(chkEvents);
                foreach (var s in db.Events)
                {
                    if (NewEvents.Contains(s.ID.ToString()))
                    {
                        attendee.Events.Add(s);
                    }
                }
            }
        }
        private void UpdateAttendeeEvents(Attendee AttendeeToUpdate, string[] chkEvents)
        {
            if (chkEvents == null)
            {
                AttendeeToUpdate.Events = new List<Event>();
                return;
            }

            var NewEvents = new HashSet<string>(chkEvents);
            var AttendeeEvents = new HashSet<int>(AttendeeToUpdate.Events.Select(s => s.ID));

            foreach (var s in db.Events)
            {
                if (NewEvents.Contains(s.ID.ToString()))
                {
                    if (!AttendeeEvents.Contains(s.ID))
                    {
                        AttendeeToUpdate.Events.Add(s);
                    }
                }
                else
                {
                    if (AttendeeEvents.Contains(s.ID))
                    {
                        AttendeeToUpdate.Events.Remove(s);
                    }
                }
            }
        }
    }
}
