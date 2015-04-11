using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ProficiencyTest.Controllers;
using ProficiencyTest.Models;
using ProficiencyTest.Utility;

namespace ProficiencyTest.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(null);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(null);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Contacts()
        {
            // Arrange
            var listTask = Task.FromResult(new List<Person> { new Person { Name = new Name { First = "PPP" } } });
            var intTask = Task.FromResult(5);
            var helper = Substitute.For<ServiceHelper>();
            helper.GetServiceAsync<List<Person>>(Arg.Any<HttpVerbs>(), Arg.Any<string>(), Arg.Any<object>()).Returns(listTask);
            helper.GetServiceAsync<int>(Arg.Any<HttpVerbs>(), Arg.Any<string>(), Arg.Any<object>()).Returns(intTask);
            HomeController controller = new HomeController(helper);

            // Act
            ViewResult result = await controller.Contacts() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
