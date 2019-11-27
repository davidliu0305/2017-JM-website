using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JMWebsite.Models
{
    public class Attendee
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name of the person to contact is required.")]
        [StringLength(100, ErrorMessage = "Your name cannot be longer than 100 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(100, ErrorMessage = "Your name cannot be longer than 100 characters.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name of the attendee is required.")]
        [StringLength(100, ErrorMessage = "Your name cannot be longer than 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Role of the attendee is required.")]
        [StringLength(30, ErrorMessage = "Your Role cannot be longer than 30 characters.")]
        public string Role { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100, ErrorMessage = "Your email cannot be longer than 100 characters.")]
        public string Email { get; set; }

        public string FormalName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public virtual ICollection<Event> Events { get; set; }
    }
}