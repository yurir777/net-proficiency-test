using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProficiencyTest.Models
{
    public class Supplier : Person
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(12, MinimumLength = 7, ErrorMessage = "Telephone number must be between 7 and 12 digits long and include digits only.")]
        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^[0-9]{7,12}$", ErrorMessage = "Telephone number must be between 7 and 12 digits long and include digits only.")]
        public string Telephone { get; set; }

        public Supplier()
        {
            ContactType = Models.ContactType.Supplier;
        }
    }
}