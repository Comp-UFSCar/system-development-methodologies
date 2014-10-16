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
    public class PlayersControllerTest
    {
        private ICollection<Player> _data; 
        private Mock<PlayerService> _players;
        
        [TestInitialize]
        public void Setup()
        {
            // retrieving test data
            _data    = new PlayersTestData().Data;
            _players = new Mock<PlayerService>(null);
        }

        [TestMethod]
        public async Task PlayersIndex()
        {
            var request = new Mock<HttpRequestBase>();
            var context = new Mock<HttpContextBase>();
            
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object);

            _players
                .Setup(e => e.Search(null))
                .Returns(Task.FromResult(_data));

            var controller = new PlayersController(null, null, _players.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var view   = await controller.Index() as ViewResult;
            var result = view.Model               as IEnumerable<Player>;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PlayersDetails()
        {
            _players
                .Setup(e => e.Find("player1"))
                .Returns(Task.FromResult((_data as List<Player>)[0]));

            var controller = new PlayersController(null, null, _players.Object);

            var view   = await controller.Details("player1") as ViewResult;
            var result = view.Model                            as Player;

            Assert.AreEqual("player1", result.Id);
        }

        [TestMethod]
        public async Task PlayersDetailsNullId()
        {
            var controller = new PlayersController(null, null, _players.Object);

            var view       = await controller.Details(null);

            Assert.IsInstanceOfType(view, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public async Task PlayersDetailsNonexistentId()
        {
            _players
                .Setup(e => e.Find("player4"))
                .ReturnsAsync(null);

            var controller = new PlayersController(null, null, _players.Object);

            var view = await controller.Details("player4");

            Assert.IsInstanceOfType(view, typeof(HttpNotFoundResult));
        }
    }
}

