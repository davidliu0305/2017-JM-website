using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JMWebsite.Models
{
    public class Testimonial
    {
        public int ID { get; set; }

        [Display(Name = "Poster")]
        [Required(ErrorMessage = "Last name of the poster is required.")]
        [StringLength(30, ErrorMessage = "Your posted name cannot be longer than 30 characters.")]
        public string Poster { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name of author is required.")]
        [StringLength(30, ErrorMessage = "Your first name cannot be more than 30 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name of the author is required.")]
        [StringLength(30, ErrorMessage = "Your last name cannot be longer than 30 characters.")]
        public string LastName { get; set; }

        [Display(Name = "Company Name")]
        [StringLength(50, ErrorMessage = "Your company name cannot be more than 50 characters.")]
        public string CompName { get; set; }

        [Display(Name = "Recipient")]
        [Required(ErrorMessage = "First Name of the person to contact is required.")]
        [StringLength(25, ErrorMessage = "The recipients name cannot be longer than 25 characters.")]
        public string RecipName { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Cat { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "A message is required")]
        [StringLength(1200, ErrorMessage = "The message cannot be larger than 1200 characters")]
        public string Message { get; set; }

        [Display(Name = "Closing")]
        [Required(ErrorMessage = "A closing statement is required")]
        [StringLength(50, ErrorMessage = "Your closing statement cannot be more than 50 characters.")]
        public string CloseState { get; set; }

        public string Name
        {
            get
            {
                string c = CompName;
                if(c == null)
                {
                    return FirstName + " " + LastName;
                }
                return FirstName + " " + LastName + " " + c;
            }
        }
    }
}