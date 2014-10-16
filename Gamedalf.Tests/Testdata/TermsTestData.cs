using Gamedalf.Core.Models;
using Gamedalf.Tests.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;

namespace Gamedalf.Tests.Testdata
{
    public class TermsTestData
    {
        public ICollection<Terms> Data { get; set; }

        public TermsTestData()
        {
            try
            {
                var employees = (List<Employee>)new EmployeesTestData().Data;

                Data = new List<Terms>
                {
                    new Terms 
                    {
                        Id      = 1,
                        Title   = "terms1",
                        Content = "terms1",
                        EmployeeId = employees[0].Id,
                        Employee   = employees[0]
                    },
                    new Terms 
                    {
                        Id      = 2,
                        Title   = "terms2",
                        Content = "terms2",
                        EmployeeId = employees[1].Id,
                        Employee   = employees[1]
                    },
                    new Terms 
                    {
                        Id      = 3,
                        Title   = "terms3",
                        Content = "terms3",
                        EmployeeId = employees[2].Id,
                        Employee   = employees[2]
                    },
                };
            }
            catch (Exception) {
                throw new TestDataInitializationException();
            }
        }
    }
}
