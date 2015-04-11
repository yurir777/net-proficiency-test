using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using EntityFramework.Testing;
using ProficiencyTest.Controllers;
using ProficiencyTest.Data;
using ProficiencyTest.Models;

namespace ProficiencyTest.Tests.Controllers
{
    [TestClass]
    public class PeopleControllerTest
    {
        [TestMethod]
        public async Task GetContacts()
        {
            var data = new List<Person>
            {
                new Customer{ Name = new Name { First = "CCC" } },
                new Supplier{ Name = new Name { First = "SSS" } },
            };
            var set = Substitute.For<DbSet<Person>, IQueryable<Person>, IDbAsyncEnumerable<Person>>().SetupData(data);
            var context = Substitute.For<DataContext>();
            context.Contacts.Returns(set);

            var controller = new PeopleController(context);
            var result = await controller.GetContacts();

            Assert.AreEqual(2, result.Contacts.Count());
            Assert.AreEqual("CCC", result.Contacts[0].Name.First);
            Assert.AreEqual("SSS", result.Contacts[1].Name.First);
        }
    }
}
