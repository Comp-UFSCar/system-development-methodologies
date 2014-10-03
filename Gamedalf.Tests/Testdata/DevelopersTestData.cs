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
                    Email    = "lucasolivdavid@gmail.com",
                    UserName = "lucasolivdavid@gmail.com",
                },
                new Developer
                {
                    Id       = "developer2",
                    Email    = "maria@db.net",
                    UserName = "maria@db.net",
                },
                new Developer
                {
                    Id       = "developer3",
                    Email    = "johnhall@provider.com",
                    UserName = "johnhall@provider.com"
                },
                new Developer
                {
                    Id       = "developer4",
                    Email    = "email@mail.com",
                    UserName = "email@mail.com"
                }
            };
        }
    }
}
