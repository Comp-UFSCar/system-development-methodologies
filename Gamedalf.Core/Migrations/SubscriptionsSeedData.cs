using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamedalf.Core.Migrations
{
    class SubscriptionsSeedData
    {
        public ICollection<Subscription> Data { get; set; }

        public SubscriptionsSeedData()
        {
            Data = new List<Subscription>
            {
                new Subscription{
                    Id = 1,
                    Cost = 10
                },
                new Subscription{
                    Id = 2,
                    Cost = 20
                }
            };
        }
    }
}
