using System.Collections.Generic;
using System.Web.Mvc;
using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.Tests.Testdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gamedalf.Controllers;
using System.Threading.Tasks;
using Moq;

namespace Gamedalf.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<GameService> _games;
        private ICollection<Game> _data;

        [TestInitialize]
        public void Setup()
        {
            _games = new Mock<GameService>(null);
            _data  = new GamesTestData().Data;
        }

        [TestMethod]
        public async Task HomeIndex()
        {
            _games
                .Setup(g => g.Recent(It.IsAny<int>()))
                .ReturnsAsync(_data);

            // Arrange
            var controller = new HomeController(_games.Object);

            // Act
            var result = (await controller.Index()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HomeAbout()
        {
            // Arrange
            var controller = new HomeController(null);

            // Act
            var result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HomeContact()
        {
            // Arrange
            var controller = new HomeController(null);

            // Act
            var result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
