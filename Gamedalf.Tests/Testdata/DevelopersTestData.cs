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
                    Email    = "juan@gmail.com",
                    UserName = "juan@gmail.com",
                },
                new Developer
                {
                    Id       = "developer2",
                    Email    = "guilia@db.net",
                    UserName = "guilia@db.net",
                }
            };
        }
    }
}
