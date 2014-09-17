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
        private ICollection<Player> data; 
        private Mock<PlayerService> players;
        
        [TestInitialize]
        public void setup()
        {
            // retrieving test data
            data    = new PlayersTestData().Data;
            players = new Mock<PlayerService>(null);
        }

        [TestMethod]
        public async Task PlayersIndex()
        {
            var request = new Mock<HttpRequestBase>();
            var context = new Mock<HttpContextBase>();
            
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object);

            players
                .Setup(e => e.Search(null))
                .Returns(Task.FromResult(data));

            var controller = new PlayersController(null, null, players.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var view   = await controller.Index() as ViewResult;
            var result = view.Model               as IEnumerable<Player>;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PlayersDetails()
        {
            players
                .Setup(e => e.Find("player1"))
                .Returns(Task.FromResult((data as List<Player>)[0]));

            var controller = new PlayersController(null, null, players.Object);

            var view   = await controller.Details("player1") as ViewResult;
            var result = view.Model                            as Player;

            Assert.AreEqual("player1", result.Id);
        }

        [TestMethod]
        public async Task PlayersDetailsWithNullId()
        {
            var controller = new PlayersController(null, null, players.Object);

            var view       = await controller.Details(null);

            Assert.IsInstanceOfType(view, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public async Task PlayersDetailsWithInvalidId()
        {
            Player unexistent = null;

            players
                .Setup(e => e.Find("player42"))
                .Returns(Task.FromResult(unexistent));

            var controller = new PlayersController(null, null, players.Object);

            var view = await controller.Details("player42");

            Assert.IsInstanceOfType(view, typeof(HttpNotFoundResult));
        }
    }
}

