using Gamedalf.Controllers;
using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.Tests.Testdata;
using Gamedalf.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gamedalf.Tests.Controllers
{
    [TestClass]
    public class TermsControllerTest
    {
        private Mock<TermsService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<TermsService>(null);
        }

        [TestMethod]
        public async Task TermsIndex()
        {
            var request = new Mock<HttpRequestBase>();
            var context = new Mock<HttpContextBase>();

            context
                .SetupGet(x => x.Request)
                .Returns(request.Object);

            var data = new TermsTestData().Data;

            _service
                .Setup(s => s.Search(null))
                .ReturnsAsync(data);

            var controller = new TermsController(_service.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = await controller.Index() as ViewResult;
            var terms = result.Model as IEnumerable<Terms>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(terms);
        }

        [TestMethod]
        public async Task TermsDetails()
        {
            var terms = ((List<Terms>)new TermsTestData().Data)[0];

            _service
                .Setup(s => s.Find(terms.Id))
                .ReturnsAsync(terms);

            var controller = new TermsController(_service.Object);

            var result = await controller.Details(terms.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task TermsDetailsNullId()
        {
            var controller = new TermsController(_service.Object);

            var result = await controller.Details(null) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TermsInvalidId()
        {
            _service
                .Setup(s => s.Find(4))
                .ReturnsAsync(null);

            var controller = new TermsController(_service.Object);

            var result = await controller.Details(4) as HttpNotFoundResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TermsCreate()
        {
            var controller = new TermsController(_service.Object);

            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TermsCreateValid()
        {
            var context = new Mock<HttpContextBase>();
            var identity = new Mock<IIdentity>();
            context
                .SetupGet(c => c.User.Identity)
                .Returns(identity.Object);

            // Defines that a user test is authenticated
            identity
                .Setup(c => c.Name)
                .Returns("test");

            // Defines that every call 
            _service
                .Setup(s => s.Add(It.IsAny<Terms>()))
                .ReturnsAsync(new Terms
                {
                    Id = 4,
                    Title = "terms4",
                    Content = "terms4"
                });

            var model = new TermsCreateViewModel
            {
                Title = "terms4",
                Content = "terms4"
            };

            var controller = new TermsController(_service.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = await controller.Create(model) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TermsUpdate()
        {
            var terms = ((List<Terms>)new TermsTestData().Data)[0];

            _service
                .Setup(s => s.Find(terms.Id))
                .ReturnsAsync(terms);

            var controller = new TermsController(_service.Object);

            var result = await controller.Update(terms.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task TermsUpdateNullId()
        {
            int? id = null;

            var controller = new TermsController(_service.Object);

            var result = await controller.Update(id) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TermsUpdateNonexistentId()
        {
            _service
                .Setup(s => s.Find(4))
                .ReturnsAsync(null);

            var controller = new TermsController(_service.Object);

            var result = await controller.Update(4) as HttpNotFoundResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TermsUpdateValid()
        {
            var terms = ((List<Terms>) new TermsTestData().Data)[0];

            var viewmodel = new TermsEditViewModel
            {
                Id = terms.Id,
                Title = terms.Title,
                Content = terms.Content
            };

            // Mock _terms.Find(1);
            _service
                .Setup(s => s.Find(terms.Id))
                .ReturnsAsync(terms);

            // Mock _terms.Add(terms);
            _service
                .Setup(s => s.Add(It.IsAny<Terms>()))
                .ReturnsAsync(terms);

            var context = new Mock<HttpContextBase>();
            var identity = new Mock<IIdentity>();
            context
                .SetupGet(c => c.User.Identity)
                .Returns(identity.Object);

            // Defines that a user test is authenticated
            identity
                .Setup(c => c.Name)
                .Returns("test");

            var controller = new TermsController(_service.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = await controller.Update(viewmodel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TermsDelete()
        {
            var terms = ((List<Terms>) new TermsTestData().Data)[0];

            _service
                .Setup(s => s.Find(terms.Id))
                .ReturnsAsync(terms);

            var controller = new TermsController(_service.Object);

            var result = await controller.Delete(terms.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task TermsDeleteNullId()
        {
            var controller = new TermsController(_service.Object);

            var result = await controller.Delete(null) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TermsDeleteInvalidId()
        {
            _service
                .Setup(s => s.Find(It.IsAny<int>()))
                .ReturnsAsync(null);

            var controller = new TermsController(_service.Object);

            var result = await controller.Delete(4) as HttpNotFoundResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TermsDeleteConfirm()
        {
            _service
                .Setup(s => s.Delete(1))
                .ReturnsAsync(1);

            var controller = new TermsController(_service.Object);

            var result = await controller.DeleteConfirmed(1) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
