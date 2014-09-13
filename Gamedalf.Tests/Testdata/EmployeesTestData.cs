using Gamedalf.Core.Models;
using Gamedalf.Tests.Infrastructure;
using System.Collections.Generic;

namespace Gamedalf.Tests.Testdata
{
    public class EmployeesTestData : ITestData<Employee>
    {
        public ICollection<Employee> Data { get; set; }

        public EmployeesTestData()
        {
            Data = new List<Employee>
            {
                new Employee 
                {
                    Id       = "employee1",
                    Email    = "lucasolivdavid@gmail.com",
                    UserName = "lucasolivdavid@gmail.com",
                    SSN      = "101-102-4011"
                },
                new Employee
                {
                    Id       = "employee2",
                    Email    = "maria@db.net"
                },
                new Employee
                {
                    Id       = "employee3",
                    Email    = "johnhall@provider.com"
                },
                new Employee
                {
                    Id       = "employee4",
                    Email    = "email@mail.com"
                }
            };
        }
    }
}
