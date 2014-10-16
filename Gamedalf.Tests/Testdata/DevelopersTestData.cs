using Gamedalf.Core.Models;
using Gamedalf.Tests.Infrastructure;
using System.Collections.Generic;

namespace Gamedalf.Tests.Testdata
{
    public class DevelopersTestData : ITestData<Developer>
    {
        public ICollection<Developer> Data { get; set; }

        public DevelopersTestData()
        {
            Data = new List<Developer>
            {
                new Developer 
                {
                    Id       = "developer1",
                    Email    = "developer1@test.com",
                    UserName = "developer1@test.com",
                },
                new Developer
                {
                    Id       = "developer2",
                    Email    = "developer2@test.com",
                    UserName = "developer2@test.com",
                },
                new Developer
                {
                    Id       = "developer3",
                    Email    = "developer3@test.com",
                    UserName = "developer3@test.com",
                }
            };
        }
    }
}
