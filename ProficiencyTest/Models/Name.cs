using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProficiencyTest.Models
{
    public class Name
    {
        [Required]
        [MaxLength(50, ErrorMessage = "First Name cannot be longer that 50 characters.")]
        [Display(Name = "First Name", Prompt = "Please enter First Name")]
        public string First { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Last Name cannot be longer that 50 characters.")]
        [Display(Name = "Last Name", Prompt = "Please enter Last Name")]
        public string Last { get; set; }
    }
}