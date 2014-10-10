using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gamedalf.Controllers;
using System.Threading.Tasks;

namespace Gamedalf.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public async Task HomeIndex()
        {
            // Arrange
            var controller = new HomeController(null);

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
