using JMWebsite.DAL.JMEntities;
using JMWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Data;

namespace JMWebsite.Controllers
{
    public class EventsController : Controller
    {
        private JohnMEntities db = new JohnMEntities();
        public ActionResult Index()
        {
            return View("Index", db.Events.Include(e => e.eventImages).OrderByDescending(s => s.Date).ToList());
        }

        [Authorize]
        public ActionResult IndexForAdmin()
        {
            return View("IndexForAdmin", db.Events.Include(e => e.eventImages).OrderByDescending(s => s.Date).ToList());
        }

        [Authorize]
        public ActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Name,Type,StartTime,Description,Date,ActCost,AttendeeDinner,AlcService,AntipastoBar,fPayName,fPayDate,ClientID,PosterName")] Event _event, HttpPostedFileBase posterFile)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (posterFile != null && posterFile.ContentLength > 0)
                    {
                        string name = posterFile.FileName;
                        if (posterFile.ContentType.ToLower().Contains("image"))
                        {                            
                            posterFile.SaveAs(HttpContext.Server.MapPath("~/Images/Events/Posters/")
                                                              + posterFile.FileName);
                            _event.PosterName = posterFile.FileName;
                            db.Events.Add(_event);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["notice"] = "Only image file types are allowed to be uploaded for event poster!";
                        }
                    }
                    else
                    {
                        TempData["notice"] = "Event poster must be uploaded!";
                    }
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

            PopulateDropDownList(_event);
            return View(_event);

        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event _event = db.Events.Find(id);
            if (_event == null)
            {
                return HttpNotFound();
            }
            PopulateDropDownList(_event);
            return View(_event);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditPost(int? id, HttpPostedFileBase posterFile)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventToUpdate = db.Events.Find(id);
            if (posterFile != null && posterFile.ContentLength > 0)
            {
                if (posterFile.ContentType.ToLower().Contains("image"))
                {
                    string fullPath = Request.MapPath("~/Images/Events/Posters" + posterFile.FileName);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    posterFile.SaveAs(HttpContext.Server.MapPath("~/Images/Events/Posters/")
                                                             + posterFile.FileName);
                    eventToUpdate.PosterName = posterFile.FileName;
                    if (TryUpdateModel(eventToUpdate, "",
                        new string[] { "Name", "Type", "StartTime", "Description", "Date", "ActCost", "AttendeeDinner", "AlcService", "AntipastoBar", "fPayName", "fPayDate", "ClientID", "PosterName" }))
                    {
                        try
                        {
                            db.Entry(eventToUpdate).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
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

                    }
                }
                else
                {
                    TempData["notice"] = "Only image file types are allowed to be uploaded for event poster!";
                }
            }
            else
            {
                if (TryUpdateModel(eventToUpdate, "",
                        new string[] { "Name", "Type", "StartTime", "Description", "Date", "ActCost", "AttendeeDinner", "AlcService", "AntipastoBar", "fPayName", "fPayDate", "ClientID" }))
                {
                    try
                    {
                        db.Entry(eventToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
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

                }
            }
            PopulateDropDownList(eventToUpdate);
            return View(eventToUpdate);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event _event = db.Events
                .Where(a => a.ID == id).SingleOrDefault();
            if (_event == null)
            {
                return HttpNotFound();
            }
            return View(_event);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             Event _event = db.Events.Find(id);
            if (_event == null)
            {
                return HttpNotFound();
            }
            return View(_event);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Event _event = db.Events.Find(id);
            string fullPath = Request.MapPath("~/Images/Events/Posters" + _event.PosterName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            try
            {
                db.Events.Remove(_event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException)//Note: there is really no reason a delete should fail if you can "talk" to the database.
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(_event);
        }

        public string GetEventsForUserJSON(int month)
        {
           List<Event> events = new List<Event>();
            if (month != 0)
            {
                events = db.Events.Where(s => s.Date.Month == month).AsEnumerable().Select(s => new Event
                {
                    ID = s.ID,
                    Name = s.Name,
                    Type = s.Type,
                    Description = s.Description,
                    Date = s.Date,
                    PosterName = s.PosterName
                }).OrderByDescending(s => s.Date).ToList();
            }
            else
            {
               events = db.Events.AsEnumerable().Select(s => new Event
                {
                    ID = s.ID,
                    Name = s.Name,
                    Type = s.Type,
                    Description = s.Description,
                    Date = s.Date,
                    PosterName = s.PosterName
               }).OrderByDescending(s => s.Date).ToList();
            }
            return JsonConvert.SerializeObject(events, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
        private void PopulateDropDownList(Event _event = null)
        {
            ViewBag.ClientID = new SelectList(db.Clients
                .OrderBy(a => a.ID)
                .ThenBy(a => a.cliName), "ID", "FullName", _event?.ClientID);
        }
    }
}