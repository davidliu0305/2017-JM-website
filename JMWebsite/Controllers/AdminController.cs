using JMWebsite.DAL.JMEntities;
using JMWebsite.Models;
using JMWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace JMWebsite.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private JohnMEntities db = new JohnMEntities();
        public ActionResult Index()
        {
            AdminView adminView = new AdminView();
            adminView.clients = db.Clients.ToList();
            adminView.events = db.Events.ToList();
            adminView.attendees = db.Attendees.ToList();
            adminView.contracts = db.Contracts.Include(s => s._event).ToList();
            adminView.cateringServices = db.CateringServices.Include(s => s._event).ToList();
            adminView.guests = db.Guests.ToList();
            adminView.schedules = db.Schedules.Include(s => s._event).ToList();
            return View(adminView);
        }
    }
}
