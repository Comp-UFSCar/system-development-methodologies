using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gamedalf.Services;
using Moq;
using System.Collections.Generic;
using Gamedalf.Core.Models;
using Gamedalf.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Gamedalf.Tests.Controllers
{
    [TestClass]
    public class EmployeesControllerTest
    {
        private Mock<EmployeeService> employees;
        private Mock<ApplicationUserManager> userManager;

        [TestInitialize]
        public void setup()
        {
            employees = new Mock<EmployeeService>();
            userManager = new Mock<ApplicationUserManager>();
        }

        [TestMethod]
        public async Task EmployeesControllerIndex()
        {
            var controller = new EmployeesController(userManager.Object, employees.Object);

            var result = (await controller.Index()) as ViewResult;

            Assert.AreEqual(4, controller.ViewData.Count);
        }
    }
}
