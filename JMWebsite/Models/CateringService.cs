using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JMWebsite.Models
{
    public class CateringService
    {
        [Key, ForeignKey("_event")]
        [Required]
        [Display(Name = "Event")]
        public int EventID { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "A description is required")]
        public string Description { get; set; }
        //[Display(Name="Name")]
        //[Required(ErrorMessage ="You cannot leave the name blank.")]
        //[StringLength(100,ErrorMessage ="Name cannot be longer than 100 characters.")]
        //public string Name { get; set; }

        //[Required(ErrorMessage ="You cannot leave the Time blank.")]
        //[DataType(DataType.Time)]
        //[Display(Name="Time")]
        //public DateTime Time { get; set; }

        public virtual Event _event { get; set; }
    }
}