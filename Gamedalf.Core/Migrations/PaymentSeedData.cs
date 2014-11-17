using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;

namespace Gamedalf.Core.Migrations
{
    class PaymentSeedData
    {
        public ICollection<Payment> Data { get; set; }

        public PaymentSeedData()
        {
            var players = new PlayersSeedData().Data as List<Player>;

            Data = new List<Payment>
            {
                new Payment
                {
                    DateCreated = DateTime.Now,
                    PlayerId = "268a55c2-4a26-42eb-98a1-f4cef20908e6",
                    value = 30
                },
                new Payment
                {
                    DateCreated = DateTime.Now,
                    PlayerId = "268a55c2-4a26-42eb-98a1-f4cef20908e6",
                    value = 40
                },
                new Payment
                {
                    DateCreated = DateTime.Now,
                    PlayerId = "268a55c2-4a26-42eb-98a1-f4cef20908e6",
                    value = 10
                },
                new Payment
                {
                    DateCreated = DateTime.Now,
                    PlayerId = "268a55c2-4a26-42eb-98a1-f4cef20908e6",
                    value = 100
                },
            };
        }
    }
}
