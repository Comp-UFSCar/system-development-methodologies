using Gamedalf.Controllers;
using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gamedalf.Tests.Controllers
{
    [TestClass]
    public class DevelopersControllerTest
    {
        private Mock<DeveloperService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<DeveloperService>(null);
        }

        [TestMethod]
        public void DevelopersBecome()
        {
            var controller = new DevelopersController(null, _service.Object, null);

            var result = controller.Become() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DevelopersTerms()
        {
            var controller = new DevelopersController(null, _service.Object, null);

            var result = controller.Terms();

            var termsAccepted = ((result as ViewResult).Model as AcceptTermsViewModel).AcceptTerms;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(termsAccepted);
        }

        [TestMethod]
        public async Task DevelopersBecomeDecline()
        {
            var controller = new DevelopersController(null, _service.Object, null);
            controller.ModelState.AddModelError("error1", "error1");
            
            var result = await controller.Become(new AcceptTermsViewModel
            {
                AcceptTerms = false
            });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNotNull((result as ViewResult).Model);
        }

        [TestMethod]
        public async Task DevelopersBecomeAccept()
        {
            var developer = new Developer { Id = "developer4" };

            var context = new Mock<HttpContextBase>();
            var identity = new Mock<IIdentity>();
            context
                .SetupGet(c => c.User.Identity)
                .Returns(identity.Object);

            _service
                .Setup(s => s.Convert(It.IsAny<string>()))
                .ReturnsAsync(developer);

            var userManager = new Mock<ApplicationUserManager>(new Mock<IUserStore<ApplicationUser>>().Object);
            userManager
                .Setup(u => u.AddToRoleAsync(null, "developer"))
                .ReturnsAsync(new IdentityResult());

            var controller = new DevelopersController(userManager.Object, _service.Object, null);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = await controller.Become(new AcceptTermsViewModel
            {
                AcceptTerms = true
            });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }
    }
}
