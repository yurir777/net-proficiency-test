using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProficiencyTest.Models
{
    public class Person
    {
        public long Id { get; set; }
        
        [NotMapped]
        public string ContactType { get; set; }
        
        public Name Name { get; set; }

        public Person()
        {
            Name = new Name();
        }
    }
}