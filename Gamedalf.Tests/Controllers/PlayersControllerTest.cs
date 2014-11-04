using Gamedalf.Controllers;
using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.Tests.Testdata;
using Gamedalf.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gamedalf.Tests.Controllers
{
    [TestClass]
    public class PlayersControllerTest
    {
        private ICollection<Player> _data; 
        private Mock<PlayerService> _service;
        
        [TestInitialize]
        public void Setup()
        {
            // retrieving test _data
            _data    = new PlayersTestData().Data;
            _service = new Mock<PlayerService>(null);
        }

        [TestMethod]
        public async Task PlayersIndex()
        {
            var request = new Mock<HttpRequestBase>();
            var context = new Mock<HttpContextBase>();
            
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object);

            _service
                .Setup(e => e.Search(null))
                .Returns(Task.FromResult(_data));

            var controller = new PlayersController(null, null, _service.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var view   = await controller.Index() as ViewResult;
            var result = view.Model               as IEnumerable<Player>;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PlayersDetails()
        {
            _service
                .Setup(e => e.Find("player1"))
                .Returns(Task.FromResult((_data as List<Player>)[0]));

            var controller = new PlayersController(null, null, _service.Object);

            var view   = await controller.Details("player1") as ViewResult;
            var result = view.Model                            as Player;

            Assert.AreEqual("player1", result.Id);
        }

        [TestMethod]
        public async Task PlayersDetailsNullId()
        {
            var controller = new PlayersController(null, null, _service.Object);

            var view       = await controller.Details(null);

            Assert.IsInstanceOfType(view, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public async Task PlayersDetailsNonexistentId()
        {
            _service
                .Setup(e => e.Find("player4"))
                .ReturnsAsync(null);

            var controller = new PlayersController(null, null, _service.Object);

            var view = await controller.Details("player4");

            Assert.IsInstanceOfType(view, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DevelopersBecome()
        {
            var controller = new PlayersController(null, _service.Object);

            var result = controller.Become() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DevelopersTerms()
        {
            var controller = new PlayersController(null, _service.Object);

            var result = controller.Terms();

            var termsAccepted = ((result as ViewResult).Model as AcceptTermsViewModel).AcceptTerms;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(termsAccepted);
        }

        [TestMethod]
        public async Task DevelopersBecomeDecline()
        {
            var controller = new PlayersController(null, _service.Object);
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
            var developer = new Player { Id = "developer4", DateConverted = DateTime.Now };

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

            var controller = new PlayersController(userManager.Object, _service.Object);
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

