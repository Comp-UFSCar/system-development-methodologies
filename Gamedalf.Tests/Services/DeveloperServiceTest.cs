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
    public class DeveloperServiceTest
    {
        private Mock<ApplicationDbContext> _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new Mock<ApplicationDbContext>();

            var dataDevelopers = new DevelopersTestData().Data.AsQueryable();
            var dataPlayer     = new PlayersTestData().Data.AsQueryable();

            var setDeveloper = new Mock<DbSet<Developer>>() { CallBase = true };
            setDeveloper.As<IDbAsyncEnumerable<Developer>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Developer>(dataDevelopers.GetEnumerator()));
            setDeveloper.As<IQueryable<Developer>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Developer>(dataDevelopers.Provider));
            setDeveloper.As<IQueryable<Developer>>().Setup(m => m.Expression).Returns(dataDevelopers.Expression);
            setDeveloper.As<IQueryable<Developer>>().Setup(m => m.ElementType).Returns(dataDevelopers.ElementType);
            setDeveloper.As<IQueryable<Developer>>().Setup(m => m.GetEnumerator()).Returns(dataDevelopers.GetEnumerator());

            var setPlayer = new Mock<DbSet<Player>>() { CallBase = true };
            setPlayer.As<IDbAsyncEnumerable<Player>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Player>(dataPlayer.GetEnumerator()));
            setPlayer.As<IQueryable<Player>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Developer>(dataPlayer.Provider));
            setPlayer.As<IQueryable<Player>>().Setup(m => m.Expression).Returns(dataPlayer.Expression);
            setPlayer.As<IQueryable<Player>>().Setup(m => m.ElementType).Returns(dataPlayer.ElementType);
            setPlayer.As<IQueryable<Player>>().Setup(m => m.GetEnumerator()).Returns(dataPlayer.GetEnumerator());

            _context.Setup(c => c.Set<Developer>()).Returns(setDeveloper.Object);
            _context.Setup(c => c.Developers).Returns(setDeveloper.Object);

            _context.Setup(c => c.Set<Player>()).Returns(setPlayer.Object);
            _context.Setup(c => c.Players).Returns(setPlayer.Object);
        }

        [TestMethod]
        public async Task DeveloperServiceSearchWithoutQuery()
        {
            var developers = new DeveloperService(_context.Object);

            var result = await developers.Search(null);

            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public async Task DeveloperServiceSearchWithQuery()
        {
            var developers = new DeveloperService(_context.Object);

            var result = await developers.Search("developer1@test.com");

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task DeveloperServiceConverter()
        {
            var developers = new DeveloperService(_context.Object);
            var player     = _context.Object.Players.First();

            _context
                .Setup(c => c.Players.Remove(It.IsAny<Player>()))
                .Returns((Player p) => p);
            _context
                .Setup(c => c.Set<Developer>().Add(It.IsAny<Developer>()))
                .Returns((Developer d) => d);

            var result     = await developers.Convert(player);

            Assert.IsNotNull(result);
        }
    }
}
