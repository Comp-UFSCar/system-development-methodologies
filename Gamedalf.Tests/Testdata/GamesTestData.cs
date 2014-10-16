using System;
using Gamedalf.Core.Models;
using Gamedalf.Tests.Infrastructure;
using System.Collections.Generic;
using Gamedalf.Tests.Infrastructure.Exceptions;

namespace Gamedalf.Tests.Testdata
{
    public class GamesTestData : ITestData<Game>
    {
        public ICollection<Game> Data { get; set; }

        public GamesTestData()
        {
            try
            {
                var developers = (List<Developer>) new DevelopersTestData().Data;
                var employees = (List<Employee>) new EmployeesTestData().Data;
                Data = new List<Game>
                {
                    new Game
                    {
                        Id = 1,
                        Price = 1,
                        Title = "game1",
                        DeveloperId = developers[0].Id,
                        Developer = developers[0],
                        EmployeeId = employees[0].Id,
                        Employee = employees[0]
                    },
                    new Game
                    {
                        Id = 2,
                        Price = 2,
                        Title = "game2",
                        DeveloperId = developers[1].Id,
                        Developer = developers[1],
                        EmployeeId = employees[1].Id,
                        Employee = employees[1]
                    },
                    new Game
                    {
                        Id = 3,
                        Price = 2,
                        Title = "game3",
                        DeveloperId = developers[2].Id,
                        Developer = developers[2],
                        EmployeeId = employees[2].Id,
                        Employee = employees[2]
                    },
                };
            }
            catch (Exception)
            {
                throw new TestDataInitializationException();
            }
        }
    }
}
