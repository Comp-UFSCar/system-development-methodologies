using Gamedalf.Controllers;
using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.Tests.Testdata;
using Gamedalf.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gamedalf.Tests.Controllers
{
    [TestClass]
    public class EmployeesControllerTest
    {
        private ICollection<Employee> data; 
        private Mock<EmployeeService> employees;
        
        [TestInitialize]
        public void setup()
        {
            // retrieving test data
            data      = new EmployeesTestData().Data;
            employees = new Mock<EmployeeService>(null);
        }

        [TestMethod]
        public async Task EmployeesIndex()
        {
            // mocking request and context so Request.IsAjaxRequest() can be executed
            var request = new Mock<HttpRequestBase>();
            var context = new Mock<HttpContextBase>();
            
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object);

            employees
                .Setup(e => e.Search(null))
                .Returns(Task.FromResult(data));

            var controller = new EmployeesController(null, employees.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

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

            var controller = new EmployeesController(null, employees.Object);

            var view   = await controller.Details("employee1") as ViewResult;
            var result = view.Model                            as Employee;

            Assert.AreEqual("employee1", result.Id);
        }

        [TestMethod]
        public async Task EmployeesDetailsWithNullId()
        {
            var controller = new EmployeesController(null, employees.Object);

            var view       = await controller.Details(null);

            Assert.IsInstanceOfType(view, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public async Task EmployeesDetailsWithInvalidId()
        {
            Employee unexistent = null;

            employees
                .Setup(e => e.Find("employee42"))
                .Returns(Task.FromResult(unexistent));

            var controller = new EmployeesController(null, employees.Object);

            var view = await controller.Details("employee42");

            Assert.IsInstanceOfType(view, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EmployeesCreateGet()
        {
            var controller = new EmployeesController(null, employees.Object);

            var view       = controller.Create() as ViewResult;

            Assert.IsNotNull(view);
        }

        [TestMethod]
        public async Task EmployeesCreate()
        {
            // calls for userManager.CreateAsync that use a instance of Employee
            // as parameter will now return Task<IdentityResult>
            var userManager = new Mock<ApplicationUserManager>(new Mock<IUserStore<ApplicationUser>>().Object);
            userManager
                .Setup(u => u.CreateAsync(It.IsAny<Employee>()))
                .Returns(Task.FromResult(new IdentityResult()));

            // calls for userManager.AddToRoleAsync that use two whichever strings
            // as parameters will now return Task<IdentityResult>
            userManager
                .Setup(u => u.AddToRoleAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new IdentityResult()));

            var controller = new EmployeesController(userManager.Object, employees.Object);

            // Valid model that will be inserted
            var viewModel = new EmployeeRegisterViewModel
            {
                Email = "employee5@mail.com",
                SSN = "202-401-2101"
            };

            var result = await controller.Create(viewModel);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public async Task EmployeesCreateWithRepeatedSSN()
        {
            var userStore   = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<ApplicationUserManager>(userStore.Object);
            userManager
                .Setup(u => u.CreateAsync(It.IsAny<Employee>(), It.IsAny<string>()))
                .Throws(new DbUpdateException());
            
            var viewmodel = new EmployeeRegisterViewModel
            {
                SSN = "101-102-4011"
            };
            
            var controller = new EmployeesController(userManager.Object, employees.Object);

            var result     = await controller.Create(viewmodel);

            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNotNull((result as ViewResult).Model);
        }

        [TestMethod]
        public async Task EmployeesEdit()
        {
            employees
                .Setup(e => e.Find("employee1"))
                .Returns(Task.FromResult((data as List<Employee>)[0]));
            var controller = new EmployeesController(null, employees.Object);

            var view   = await controller.Edit("employee1") as ViewResult;
            var result = view.Model                         as EmployeeEditViewModel;

            Assert.AreEqual("employee1", result.Id);
        }

        [TestMethod]
        public async Task EmployeesEditWithNullId()
        {
            var controller = new EmployeesController(null, employees.Object);

            var view       = await controller.Edit(id: null);

            Assert.IsInstanceOfType(view, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public async Task EmployeesEditWithInvalidId()
        {
            Employee unexistent = null;

            employees
                .Setup(e => e.Find("unexistent"))
                .Returns(Task.FromResult(unexistent));

            var controller = new EmployeesController(null, employees.Object);

            var view = await controller.Edit(id: "unexistent");

            Assert.IsInstanceOfType(view, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public async Task EmployeesDelete()
        {
            employees
                .Setup(e => e.Find("employee1"))
                .Returns(Task.FromResult((data as List<Employee>)[0]));

            var controller = new EmployeesController(null, employees.Object);

            var view   = await controller.Delete("employee1") as ViewResult;
            var result = view.Model                           as Employee;

            Assert.AreEqual("employee1", result.Id);
        }

        [TestMethod]
        public async Task EmployeesDeleteWithNullId()
        {
            var controller = new EmployeesController(null, employees.Object);

            var view = await controller.Delete(id: null);

            Assert.IsInstanceOfType(view, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public async Task EmployeesDeleteWithInvalidId()
        {
            Employee unexistent = null;

            employees
                .Setup(e => e.Find("unexistent"))
                .Returns(Task.FromResult(unexistent));

            var controller = new EmployeesController(null, employees.Object);

            var view = await controller.Edit(id: "unexistent");

            Assert.IsInstanceOfType(view, typeof(HttpNotFoundResult));
        }

        public async Task EmployeesDeleteConfirmed()
        {
            var success = 1;

            employees
                .Setup(e => e.Delete("employee1"))
                .Returns(Task.FromResult(success));

            var controller = new EmployeesController(null, employees.Object);

            var result = await controller.DeleteConfirmed("employee1");

            Assert.IsNotNull(result);
        }
    }
}
