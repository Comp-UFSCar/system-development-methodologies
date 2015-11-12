using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gamedalf.Infrastructure.Games;
using System.Web;
using Moq;

namespace Gamedalf.Tests.Complementary
{
    [TestClass]
    public class GameImagesHandlerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GameImagesHandlerInvalidConstructor()
        {
            var handler = new GameImagesHandler(0, null);
        }

        [TestMethod]
        public void GameImagesHandlerValidConstructor()
        {
            var file = new Mock<HttpPostedFileBase>();

            var handler = new GameImagesHandler(1, file.Object);
        }
    }
}
