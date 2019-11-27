using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JMWebsite.ViewModels
{
    public class AttendeeEventVM
    {
        public int EventID { get; set; }

        public string _event { get; set; }

        public bool HasEvent { get; set; }
    }
}