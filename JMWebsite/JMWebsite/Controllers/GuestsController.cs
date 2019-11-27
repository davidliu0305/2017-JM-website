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
    public class GuestsController : Controller
    {
        private JohnMEntities db = new JohnMEntities();

        // GET: Guests
        public ActionResult Index()
        {
            return View(db.Guests.Include(g => g.Events).ToList());
        }

        // GET: Guests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = db.Guests
                .Include(g => g.Events)
                .Where(g => g.ID == id).SingleOrDefault();
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }
        // GET: Guests/Create
        public ActionResult Create()
        {
            var guest = new Guest();
            guest.Events = new List<Event>();
            AssignEvents(guest);
            return View();
        }

        // POST: Guests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,MiddleName,LastName,Phone,Email,Entree,AgeTitle")] Guest guest, string[] chkEvents)
        {
            try
            {
                AddGuestEvents(guest, chkEvents);
                if (ModelState.IsValid)
                {
                    db.Guests.Add(guest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            AssignEvents(guest);
            return View(guest);
        }

        // GET: Guests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = db.Guests.Include(g => g.Events).Where(g => g.ID == id).FirstOrDefault();
            if (guest == null)
            {
                return HttpNotFound();
            }
            AssignEvents(guest);
            return View(guest);
        }

        // POST: Guests/Edit/5
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
            var guestToUpdate = db.Guests.Include(s => s.Events).Where(s => s.ID == id).
                FirstOrDefault();
            if (TryUpdateModel(guestToUpdate, "",
               new string[] { "FirstName", "MiddleName", "LastName", "Phone", "Email", "Entree", "AgeTitle" }))
            {
                try
                {
                    UpdateGuestEvents(guestToUpdate, chkEvents);
                    db.Entry(guestToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            AssignEvents(guestToUpdate);
            return View(guestToUpdate);
        }    
        //public ActionResult Edit([Bind(Include = "ID,FirstName,MiddleName,LastName,Phone,Email,Entree,AgeTitle")] Guest guest)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(guest).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(guest);
        //}

        // GET: Guests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = db.Guests.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        // POST: Guests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest guest = db.Guests.Find(id);
            try
            {
                db.Guests.Remove(guest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to delete guest. Try again, and if the problem persists see your system administrator.");
            }
            return View(guest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private void AssignEvents(Guest guest)
        {
            var EventList = db.Events;
            var AssignedEvents = new HashSet<int>(guest.Events.Select(s => s.ID));
            var View = new List<GuestEventVM>();

            foreach (var s in EventList)
            {
                View.Add(new GuestEventVM
                {
                    EventID = s.ID,
                    _event = s.Name,
                    HasEvent = AssignedEvents.Contains(s.ID)
                });
            }
            ViewBag.Events = View;
        }
        private void AddGuestEvents(Guest guest, string[] chkEvents)
        {
            if (chkEvents != null)
            {
                guest.Events = new List<Event>();

                var NewEvents = new HashSet<string>(chkEvents);
                foreach (var s in db.Events)
                {
                    if (NewEvents.Contains(s.ID.ToString()))
                    {
                        guest.Events.Add(s);
                    }
                }
            }
        }
        private void UpdateGuestEvents(Guest GuestToUpdate, string[] chkEvents)
        {
            if (chkEvents == null)
            {
                GuestToUpdate.Events = new List<Event>();
                return;
            }

            var NewEvents = new HashSet<string>(chkEvents);
            var GuestEvents = new HashSet<int>(GuestToUpdate.Events.Select(s => s.ID));

            foreach (var s in db.Events)
            {
                if (NewEvents.Contains(s.ID.ToString()))
                {
                    if (!GuestEvents.Contains(s.ID))
                    {
                        GuestToUpdate.Events.Add(s);
                    }
                }
                else
                {
                    if (GuestEvents.Contains(s.ID))
                    {
                        GuestToUpdate.Events.Remove(s);
                    }
                }
            }
        }
    }
}
