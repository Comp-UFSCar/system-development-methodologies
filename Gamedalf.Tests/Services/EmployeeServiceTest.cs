using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using Gamedalf.Services;
using Gamedalf.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Gamedalf.Tests.Services
{
    [TestClass]
    public class EmployeeServiceTest
    {
        private Mock<ApplicationDbContext> context;

        [TestInitialize]
        public void Setup()
        {
            var data = new List<Employee>
            {
                new Employee { Id = "employee1", Email = "ld492@drexel.edu" },
                new Employee { Id = "employee2", Email = "maria@db.com" },
                new Employee { Id = "employee3", Email = "johnhall@provider.co" },
                new Employee { Id = "employee4", Email = "email@mail.com" },
            }.AsQueryable();

            var set = new Mock<DbSet<Employee>>() { CallBase = true };
            set.As<IDbAsyncEnumerable<Employee>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Employee>(data.GetEnumerator()));
            set.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Employee>(data.Provider));
            set.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            set.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            set.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            context = new Mock<ApplicationDbContext>();
            context.Setup(c => c.Set<Employee>()).Returns(set.Object);
            context.Setup(c => c.Employees).Returns(set.Object);
        }

        [TestMethod]
        public async Task SearchWithoutQuery()
        {
            var employees = new EmployeeService(context.Object);

            var result = await employees.Search(null);

            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public async Task SearchWithQuery()
        {
            var employees = new EmployeeService(context.Object);

            var result = await employees.Search("co");

            Assert.AreEqual(3, result.Count);
        }
    }
}
