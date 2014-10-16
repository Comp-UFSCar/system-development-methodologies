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
    public class PlayingControllerTest
    {
        private Mock<PlayingService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<PlayingService>(null);
        }

        [TestMethod]
        public async Task PlayingIndex()
        {
            var request  = new Mock<HttpRequestBase>();
            var context  = new Mock<HttpContextBase>();
            var identity = new Mock<IIdentity>();
            context
                .SetupGet(c => c.User.Identity)
                .Returns(identity.Object);
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object);

            identity
                .Setup(c => c.Name)
                .Returns("player1");

            var data = new PlayingTestData().Data;

            _service
                .Setup(s => s.PlayingDoneByUser(null, null))
                .ReturnsAsync(data);

            var controller = new PlayingController(_service.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = await controller.Index() as ViewResult;
            var terms = result.Model as IEnumerable<Playing>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(terms);
        }

        [TestMethod]
        public async Task PlayingDetails()
        {
            var playing = ((List<Playing>) new PlayingTestData().Data)[0];
            _service
                .Setup(s => s.Find(It.IsAny<int?>()))
                .ReturnsAsync(playing);

            var controller = new PlayingController(_service.Object);

            var result = await controller.Details(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            Assert.IsNotNull((result as ViewResult).Model);
        }

        [TestMethod]
        public async Task PlayingDetailsNullId()
        {
            _service
                .Setup(s => s.Find(It.IsAny<int?>()))
                .ReturnsAsync(null);

            var controller = new PlayingController(_service.Object);

            var result = await controller.Details(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public async Task PlayingDetailsInvalidId()
        {
            _service
                .Setup(s => s.Find(It.IsAny<int?>()))
                .ReturnsAsync(null);

            var controller = new PlayingController(_service.Object);

            var result = await controller.Details(4);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public async Task PlayingCreate()
        {
            var context = new Mock<HttpContextBase>();
            var identity = new Mock<IIdentity>();
            context
                .SetupGet(c => c.User.Identity)
                .Returns(identity.Object);

            var gameId = 1;
            _service
                .Setup(s => s.Add(It.IsAny<Playing>()))
                .ReturnsAsync(new Playing { Id = 4 });

            var controller = new PlayingController(_service.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = await controller.Create(gameId);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public async Task PlayingEvaluate()
        {
            var playing = ((List<Playing>)new PlayingTestData().Data)[0];

            _service
                .Setup(s => s.Find(It.IsAny<int?>()))
                .ReturnsAsync(playing);

            var controller = new PlayingController(_service.Object);

            var result = await controller.Evaluate(playing.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task PlayingEvaluateNullId()
        {
            int? id = null;

            var controller = new PlayingController(_service.Object);

            var result = await controller.Evaluate(id) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PlayingEvaluateInvalidId()
        {
            _service
                .Setup(s => s.Find(4))
                .ReturnsAsync(null);

            var controller = new PlayingController(_service.Object);

            var result = await controller.Evaluate(4) as HttpNotFoundResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PlayingEvaluateValid()
        {
            _service
                .Setup(s => s.Evaluate(It.IsAny<Playing>()))
                .ReturnsAsync(new Playing());

            var viewmodel = new EvaluationViewModel
            {
                Id = 1,
                Review = "review1",
                Score = 1
            };

            var controller = new PlayingController(_service.Object);

            var result = await controller.Evaluate(viewmodel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
