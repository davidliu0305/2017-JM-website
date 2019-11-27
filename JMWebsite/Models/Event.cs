using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JMWebsite.Models
{
    public class Event
    {
        public int ID { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Event name cannot be more than 20 characters long.")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [StringLength(255, ErrorMessage = "Event description cannot be more than 255 characters long.")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Actual Cost")]
        [Range(1.00, 999000.00, ErrorMessage = "Estimated value must be between one and 999 thousand dollars.")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false)]
        public Decimal ActCost { get; set; }

        [Required]
        [Display(Name = "Attendees' Dinner")]
        public bool AttendeeDinner { get; set; }

        [Required]
        [Display(Name = "Alcohol Service")]
        public bool AlcService { get; set; }

        [Required]
        [Display(Name = "Antipasto Bar")]
        public bool AntipastoBar { get; set; }

        [Display(Name = "Paid By:")]
        public string fPayName { get; set; }

        [Required]
        [Display(Name = "Payment Date: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fPayDate { get; set; }


        [MaxLength(20)]
        [Display(Name = "Event Poster")]
        [Index(IsUnique = true)]
        public string PosterName { get; set; }

        public string FullDate
        {
            get
            {
                return Date.ToLongDateString();
            }
        }

        public virtual ICollection<Attendee> Attendees { get; set; }

        public virtual ICollection<Guest> Guests { get; set; }

        public virtual ICollection<EventImage> eventImages { get; set; }

        [Required]
        public int ClientID { get; set; }
        public virtual Client client { get; set; }

        public virtual CateringService cateringService { get; set; }

        public virtual Schedule schedule { get; set; }

        public virtual Contract contract { get; set; }

    }
}