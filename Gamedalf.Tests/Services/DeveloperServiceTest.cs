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
        private Mock<ApplicationDbContext> context;

        [TestInitialize]
        public void Setup()
        {
            var data = new DevelopersTestData().Data.AsQueryable();

            var set = new Mock<DbSet<Developer>>() { CallBase = true };
            set.As<IDbAsyncEnumerable<Developer>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Developer>(data.GetEnumerator()));
            set.As<IQueryable<Developer>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Developer>(data.Provider));
            set.As<IQueryable<Developer>>().Setup(m => m.Expression).Returns(data.Expression);
            set.As<IQueryable<Developer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            set.As<IQueryable<Developer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            context = new Mock<ApplicationDbContext>();
            context.Setup(c => c.Set<Developer>()).Returns(set.Object);
            context.Setup(c => c.Developers).Returns(set.Object);
        }

        [TestMethod]
        public async Task DeveloperServiceSearchWithoutQuery()
        {
            var developers = new DeveloperService(context.Object);

            var result = await developers.Search(null);

            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public async Task DeveloperServiceSearchWithQuery()
        {
            var developers = new DeveloperService(context.Object);

            var result = await developers.Search("maria@db.net");

            Assert.AreEqual(1, result.Count);
        }
    }
}
