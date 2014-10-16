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
    public class PlayingServiceTest
    {
        private Mock<ApplicationDbContext> _context;

        [TestInitialize]
        public void Setup()
        {
            var data = new PlayingTestData().Data.AsQueryable();

            var set = new Mock<DbSet<Playing>>() { CallBase = true };
            set.As<IDbAsyncEnumerable<Playing>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Playing>(data.GetEnumerator()));
            set.As<IQueryable<Playing>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Playing>(data.Provider));
            set.As<IQueryable<Playing>>().Setup(m => m.Expression).Returns(data.Expression);
            set.As<IQueryable<Playing>>().Setup(m => m.ElementType).Returns(data.ElementType);
            set.As<IQueryable<Playing>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _context = new Mock<ApplicationDbContext>();
            _context.Setup(c => c.Set<Playing>()).Returns(set.Object);
            _context.Setup(c => c.Playings).Returns(set.Object);
        }

        [TestMethod]
        public async Task PlayingServiceSearch()
        {
            var data = await _context.Object.Set<Playing>().ToListAsync();

            var playings = new PlayingService(_context.Object);

            var result = await playings.Search(null);

            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public async Task PlayingServiceSearchWithGameQuery()
        {
            var playings = new PlayingService(_context.Object);

            var result = await playings.Search("game1");

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task PlayingServiceSearchWithPlayerQuery()
        {
            var playings = new PlayingService(_context.Object);

            var result = await playings.Search("player1");

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task PlayingServiceSearchWithDeveloperQuery()
        {
            var playings = new PlayingService(_context.Object);

            var result = await playings.Search("developer1");

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task PlayingServiceSearchWithEmployeeQuery()
        {
            var playings = new PlayingService(_context.Object);

            var result = await playings.Search("employee1");

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task PlayingServicePlayingDoneByUser()
        {
            var service = new PlayingService(_context.Object);

            var result = await service.PlayingDoneByUser("player1", null);

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task PlayingServiceEvaluatedByUser()
        {
            var service = new PlayingService(_context.Object);

            var result = await service.EvaluatedByUser("player1");

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task PlayingServiceNotYetEvaluatedByUser()
        {
            var service = new PlayingService(_context.Object);

            var result = await service.NotYetEvaluatedByUser("player2");

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task PlayingServicePlayingDoneByUserGameQuery()
        {
            var playings = new PlayingService(_context.Object);

            var result = await playings.PlayingDoneByUser("player1", "game1");

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task PlayingServicePlayingDoneByUserPlayerQuery()
        {
            var playings = new PlayingService(_context.Object);

            var result = await playings.PlayingDoneByUser("player1", "player1");

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task PlayingServicePlayingDoneByUserDeveloperQuery()
        {
            var playings = new PlayingService(_context.Object);

            var result = await playings.PlayingDoneByUser("player1", "developer1");

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task PlayingServicePlayingDoneByUserEmployeeQuery()
        {
            var playings = new PlayingService(_context.Object);

            var result = await playings.PlayingDoneByUser("player1", "employee1");

            Assert.AreEqual(1, result.Count);
        }
    }
}
