using Gamedalf.Controllers;
using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.Tests.Testdata;
using Gamedalf.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gamedalf.Tests.Controllers
{
    [TestClass]
    public class EmployeesControllerTest
    {
        private Mock<EmployeeService> employees;
        private Mock<ApplicationUserManager> userManager;
        private ICollection<Employee> data;
        private Mock<HttpRequestBase> request;
        private Mock<HttpContextBase> context;
        private EmployeesController controller;

        [TestInitialize]
        public void setup()
        {
            // get test data
            data = new EmployeesTestData().Data;

            // setting up EmployeeService
            employees = new Mock<EmployeeService>(null);
            
            // ajust method Search(null)
            employees
                .Setup(e => e.Search(null))
                .Returns(Task.FromResult(data));

            userManager = new Mock<ApplicationUserManager>();

            request = new Mock<HttpRequestBase>();
            
            context = new Mock<HttpContextBase>();
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object);

            controller = new EmployeesController(null, employees.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
        }

        [TestMethod]
        public async Task EmployeesIndex()
        {
            employees
                .Setup(e => e.Search(null))
                .Returns(Task.FromResult(data));

            var view   = await controller.Index() as ViewResult;
            var result = view.Model               as IEnumerable<Employee>;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task EmployeesDetails()
        {
            employees
                .Setup(e => e.Find("employee1"))
                .Returns(Task.FromResult((data as List<Employee>)[0]));

            var view   = await controller.Details("employee1") as ViewResult;
            var result = view.Model                            as Employee;

            Assert.AreEqual("employee1", result.Id);
        }

        [TestMethod]
        public async Task EmployeesDetailsWithNullId()
        {
            var view   = await controller.Details(null);
            
            Assert.IsInstanceOfType(view, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public async Task EmployeesDetailsWithInvalidId()
        {
            Employee unexistent = null;

            employees
                .Setup(e => e.Find("employee42"))
                .Returns(Task.FromResult(unexistent));

            var view = await controller.Details("employee42");

            Assert.IsInstanceOfType(view, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EmployeesCreateForm()
        {
            var view = controller.Create() as ViewResult;

            Assert.IsNotNull(view);
        }

        [TestMethod]
        public async Task EmployeesCreate()
        {
            var viewModel = new EmployeeRegisterViewModel
            {
                Email = "employee5@mail.com", SSN = "202-401-2101"
            };

            var result = await controller.Create(viewModel);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public async Task EmployeesCreateWithInvalidMail()
        {
            var viewModel = new EmployeeRegisterViewModel
            {
                SSN = "202-401-2101"
            };

            var result = await controller.Create(viewModel);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNotNull((result as ViewResult).Model);
        }
    }
}
