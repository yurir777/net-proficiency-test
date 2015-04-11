using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProficiencyTest.Models
{
    public class Customer : Person
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Description = "mm/dd/yyyy (optional)")]
        public DateTime? Birthday { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email address is invalid.")]
        [Display(Name = "Email Address", Prompt = "Please enter Email address")]
        public string Email { get; set; }

        public Customer()
        {
            ContactType = Models.ContactType.Customer;
        }
    }
}