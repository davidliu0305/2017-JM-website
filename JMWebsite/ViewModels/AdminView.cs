using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMWebsite.Models;
namespace JMWebsite.ViewModels
{
    public class AdminView
    {
        public List<Event> events { get; set; }
        public List<Guest> guests { get; set; }

        public List<Attendee> attendees { get; set; }

        public List<Contract> contracts { get; set; }

        public List<CateringService> cateringServices { get; set; }

        public List<Client> clients { get; set; }

        public List<Schedule> schedules  { get; set; }
    }
}