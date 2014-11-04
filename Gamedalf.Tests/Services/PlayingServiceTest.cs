using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.Tests.Infrastructure;
using Gamedalf.Tests.Testdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
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
        private IQueryable<Playing> _data;

        [TestInitialize]
        public void Setup()
        {
            _data = new PlayingTestData().Data.AsQueryable();

            var set = new Mock<DbSet<Playing>>() { CallBase = true };
            set.As<IDbAsyncEnumerable<Playing>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Playing>(_data.GetEnumerator()));
            set.As<IQueryable<Playing>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Playing>(_data.Provider));
            set.As<IQueryable<Playing>>().Setup(m => m.Expression).Returns(_data.Expression);
            set.As<IQueryable<Playing>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            set.As<IQueryable<Playing>>().Setup(m => m.GetEnumerator()).Returns(_data.GetEnumerator());

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

        [TestMethod]
        public async Task PlayingServiceEvaluate()
        {
            var playing = ((List<Playing>) new PlayingTestData().Data)[1];

            _context
                .Setup(c => c.SaveChangesAsync())
                .ReturnsAsync(1);

            _context
                .Setup(c => c.Set<Playing>().FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync(playing);

            var service = new PlayingService(_context.Object);

            var evaluation = new Playing
            {
                Id = 2,
                Review = "review1",
                Score = 1,
                ReviewDateCreated = DateTime.Now
            };

            var result = await service.Evaluate(evaluation);

            Assert.IsNotNull(result);
            Assert.AreEqual(evaluation.ReviewDateCreated, result.ReviewDateCreated);
        }
    }
}
