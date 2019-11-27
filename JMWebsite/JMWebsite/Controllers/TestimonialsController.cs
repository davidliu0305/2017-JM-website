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
using System.Data.Entity.Infrastructure;

namespace JMWebsite.Controllers
{
    public class TestimonialsController : Controller
    {
        private JohnMEntities db = new JohnMEntities();

        // GET: Testimonials
        public ActionResult Index()
        {
            return View("IndexView",db.Testimonials.ToList());
        }

        public ActionResult IndexView()
        {
            return View("Index",db.Testimonials.ToList());
        }

        // GET: Testimonials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = db.Testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // GET: Testimonials/Create
        public ActionResult Create()
        {
            ViewBag.Cat = new SelectList(db.Testimonials, "ID", "Cat");
            return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Poster,FirstName,LastName,CompName,RecipName,Cat,Message,CloseState")] Testimonial testimonial)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Testimonials.Add(testimonial);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes - Retry Limit Exceeded. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(testimonial);
        }

        // GET: Testimonials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = db.Testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Poster,FirstName,LastName,CompName,RecipName,Cat,Message,CloseState")] Testimonial testimonial)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(testimonial).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(testimonial);
        }

        // GET: Testimonials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = db.Testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Testimonial testimonial = db.Testimonials.Find(id);
            try
            {        
                db.Testimonials.Remove(testimonial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes - Retry Limit Exceeded. Try again, and if the problem persists, see your system administrator.");
            }
            return View(testimonial);
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
