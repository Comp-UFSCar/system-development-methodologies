using Gamedalf.Controllers;
using Gamedalf.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;

namespace Gamedalf.Tests.Controllers
{
    [TestClass]
    public class DeveloperControllerTest
    {
        private Mock<PlayerService>    _players;
        private Mock<DeveloperService> _developers;

        [TestInitialize]
        public void Setup()
        {
            _players    = new Mock<PlayerService>(null);
            _developers = new Mock<DeveloperService>(null);
        }

        [TestMethod]
        public void DevelopersBecome()
        {
            var controller = new DevelopersController(null, _developers.Object, _players.Object);

            var result = controller.Become() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
