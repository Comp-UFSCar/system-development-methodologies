using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.Tests.Infrastructure;
using Gamedalf.Tests.Testdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Gamedalf.Tests.Services
{
    [TestClass]
    public class PlayerServiceTest
    {
        private Mock<ApplicationDbContext> _context;

        [TestInitialize]
        public void Setup()
        {
            var data = new PlayersTestData().Data.AsQueryable();

            var set = new Mock<DbSet<Player>>() { CallBase = true };
            set.As<IDbAsyncEnumerable<Player>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Player>(data.GetEnumerator()));
            set.As<IQueryable<Player>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Player>(data.Provider));
            set.As<IQueryable<Player>>().Setup(m => m.Expression).Returns(data.Expression);
            set.As<IQueryable<Player>>().Setup(m => m.ElementType).Returns(data.ElementType);
            set.As<IQueryable<Player>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _context = new Mock<ApplicationDbContext>();
            _context.Setup(c => c.Set<Player>()).Returns(set.Object);
            _context.Setup(c => c.Players).Returns(set.Object);
        }

        [TestMethod]
        public async Task PlayerServiceSearchWithoutQuery()
        {
            var players = new PlayerService(_context.Object);

            var result = await players.Search(null);

            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public async Task PlayerServiceSearchWithQuery()
        {
            var players = new PlayerService(_context.Object);

            var result = await players.Search("player1@test.com");

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task PlayerServiceConverter()
        {
            // TODO: needs revision
            var developer = new Player { Id = "player2" };
            var developers = new PlayerService(_context.Object);
            _context
                .Setup(c => c.Players.FindAsync(It.IsAny<string>()))
                .ReturnsAsync(developer);

            var result = await developers.Convert(developer.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual("player2", result.Id);
        }
    }
}
