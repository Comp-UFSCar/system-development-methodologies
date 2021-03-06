﻿using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using Gamedalf.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Gamedalf.Services
{
    public class PaymentService: Service<Payment>
    {
        public PaymentService(ApplicationDbContext db): base(db){}

        public virtual async Task<ICollection<Payment>> Search(string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return await All();
            }

            return await Db.Payments
                .Where(p => p.Player.Email.Contains(q))
                .Include(p => p.Player)
                .OrderBy(p => p.PlayerId)
                .ToListAsync();
        }

        public virtual async Task<ICollection<Payment>> ByUser(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return await All();
            }

            return await Db.Payments
                .Where(p => p.Player.Id == id)
                .Include(p => p.Player)
                .OrderBy(p => p.PlayerId)
                .ToListAsync();
        }

    }
}
