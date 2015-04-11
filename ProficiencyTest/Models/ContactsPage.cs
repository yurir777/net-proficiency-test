using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProficiencyTest.Models
{
    public class ContactsPage
    {
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public List<Person> Contacts { get; set; }
    }
}
