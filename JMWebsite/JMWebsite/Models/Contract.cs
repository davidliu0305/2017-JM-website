using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JMWebsite.Models
{
    public class Contract
    {

        [Key, ForeignKey("_event")]
        [Required]
        public int EventID { get; set; }

        
        [MaxLength(20)]
        [Display(Name = "Contract: ")]
        [Index(IsUnique = true)]
        public string ContractName { get; set; }

        public virtual Event _event { get; set; }
    }
}