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
    public class ClientController : Controller
    {
        private JohnMEntities db = new JohnMEntities();

        //GET: Client
        public ActionResult Index(string searchString)
        {
            var client = from c in db.Clients
                         select c;
            if(!String.IsNullOrEmpty(searchString))
            {
                client = client.Where(s => s.cliName.Contains(searchString));
            }
            return View(client);
        }   

        //GET: Details/Client
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients
                .Include(c => c.events)
                .Where(c => c.ID == id).SingleOrDefault();
            if(client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //GET: Create/Client
        public ActionResult Create()
        {
            return View();
        }

        //POST: Create/Client
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="ID, cliName, cliCity, cliAddress, cliPhone, cliEmail, cliPostCode, cliContactFirst, cliContactLast")] Client client)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(client);
        }

        //GET: Edit/Client
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //POST: Edit/Client
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clientToUpdate = db.Clients.Find(id);
            if(TryUpdateModel(clientToUpdate, "", new string[] { "cliName","cliCity","cliAddress","cliPhone","cliEmail","cliPostCode","cliContactFirst","cliContactLast"}))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(clientToUpdate);
        }

        //GET: Delete/Client
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if(client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //POST: Delete/Client
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            try
            {
                db.Clients.Remove(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(DataException dex)
            {
                if(dex.InnerException.InnerException.Message.Contains("FK_"))
                {
                    ModelState.AddModelError("", "Unable to delete client. You cannot delete a client that has an event in the system.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to delete client. Try again, and if the problem persists see your system adminstrator.");
                }
            }
            return View(client);
        }
    }
}