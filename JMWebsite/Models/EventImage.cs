using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JMWebsite.Models
{
    public class EventImage
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Decription { get; set; }

        public string FileType { get; set; }

        [Required(ErrorMessage = "You must select the Artist.")]
        public int EventID { get; set; }

        public virtual Event _event { get; set; }
    }
}