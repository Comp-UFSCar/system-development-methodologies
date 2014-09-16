﻿using Gamedalf.Core.Data;
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
        private Mock<ApplicationDbContext> context;

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

            context = new Mock<ApplicationDbContext>();
            context.Setup(c => c.Set<Player>()).Returns(set.Object);
            context.Setup(c => c.Players).Returns(set.Object);
        }

        [TestMethod]
        public async Task PlayerServiceSearchWithoutQuery()
        {
            var players = new PlayerService(context.Object);

            var result = await players.Search(null);

            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public async Task PlayerServiceSearchWithQuery()
        {
            var players = new PlayerService(context.Object);

            var result = await players.Search("maria@db.net");

            Assert.AreEqual(1, result.Count);
        }
    }
}
