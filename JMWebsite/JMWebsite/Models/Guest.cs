using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JMWebsite.Models
{
    public class Guest
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name of the guest is required.")]
        [StringLength(100, ErrorMessage = "Your name cannot be longer than 100 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(100, ErrorMessage = "Your name cannot be longer than 100 characters.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name of the guest is required.")]
        [StringLength(100, ErrorMessage = "Your name cannot be longer than 100 characters.")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100, ErrorMessage = "Your email cannot be longer than 100 characters.")]
        public string Email { get; set; }

        [Required]
        public bool Entree { get; set; }

        public string AgeTitle { get; set; }

        public string FormalName {
            get {
                return FirstName + " " + LastName;
            }
        }
        public virtual ICollection<Event> Events { get; set; }
    }
}