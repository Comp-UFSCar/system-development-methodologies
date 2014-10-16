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
                    Email    = "employee1@test.com",
                    UserName = "employee1@test.com",
                    SSN      = "111-111-1111"
                },
                new Employee
                {
                    Id       = "employee2",
                    Email    = "employee2@test.com",
                    UserName = "employee2@test.com",
                    SSN      = "222-222-2222"
                },
                new Employee
                {
                    Id       = "employee3",
                    Email    = "employee3@test.com",
                    UserName = "employee3@test.com",
                    SSN      = "333-333-3333"
                }
            };
        }
    }
}
