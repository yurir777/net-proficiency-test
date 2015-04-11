using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ProficiencyTest.Models;

namespace ProficiencyTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("ContactManager")
        {
        }

        public virtual DbSet<Person> Contacts { get; set; }
    }
}