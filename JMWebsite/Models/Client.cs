using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JMWebsite.Models
{
    public class Client
    {
        public int ID { get; set; }

        // public string FirstName { get; set; }

        // public string MiddleName { get; set; }

        // public string LastName { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "You cannot leave the name blank.")]
        [StringLength(100, ErrorMessage = "Your name cannot be longer than 100 characters.")]
        public string cliName { get; set; }

        [Display(Name = "City")]
        [StringLength(100, ErrorMessage = "Your city name cannot be longer than 100 characters.")]
        public string cliCity { get; set; }

        [Display(Name = "Address")]
        [StringLength(100, ErrorMessage = "Your address cannot be longer than 100 characters.")]
        public string cliAddress { get; set; }

        [Display(Name ="Phone Number")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64 cliPhone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email cannot be left blank.")]
        [EmailAddress(ErrorMessage ="Please enter a valid email address.")]
        [StringLength(100, ErrorMessage = "Your email cannot be longer than 100 characters.")]
        public string cliEmail { get; set; }

        [Display(Name ="Postal Code")]
        [RegularExpression("[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]", ErrorMessage ="Please enter a valid Canadian postal code. Ex: A2A 2A2")]
        public string cliPostCode { get; set; }

        [Display(Name ="Contact First Name")]
        [Required(ErrorMessage ="Name of the person to contact is required.")]
        [StringLength(100, ErrorMessage = "Your name cannot be longer than 100 characters.")]
        public string cliContactFirst { get; set; }


        [Display(Name = "Contact Last Name")]
        [Required(ErrorMessage = "Last name of the person to contact is required.")]
        [StringLength(100, ErrorMessage = "Your name cannot be longer than 100 characters.")]
        public string cliContactLast { get; set; }

        public string FullName
        {
            get
            {
                return cliContactFirst + " " + cliContactLast;
            }
        }
        public virtual ICollection<Event> events { get; set; }
    }
}