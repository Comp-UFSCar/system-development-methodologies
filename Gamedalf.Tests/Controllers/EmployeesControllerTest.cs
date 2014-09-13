using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gamedalf.Services;
using Moq;
using System.Collections.Generic;
using Gamedalf.Core.Models;
using Gamedalf.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Gamedalf.Tests.Testdata;

namespace Gamedalf.Tests.Controllers
{
    [TestClass]
    public class EmployeesControllerTest
    {
        //private Mock<EmployeeService> employees;
        //private Mock<ApplicationUserManager> userManager;
        //private ICollection<Employee> data;

        [TestInitialize]
        public void setup()
        {
            //employees = new Mock<EmployeeService>(null);
            //userManager = new Mock<ApplicationUserManager>();
            //data = new EmployeesTestData().Data;
        }

        //[TestMethod]
        //public async Task EmployeesControllerIndex()
        //{
        //    employees
        //        .Setup(e => e.Search(null))
        //        .Returns(() => new Task<ICollection<Employee>>(() => data));

        //    var controller = new EmployeesController(null, employees.Object);

        //    var result = (await controller.Index()) as ViewResult;

        //    Assert.AreEqual(4, controller.ViewData.Count);
        //}
    }
}
