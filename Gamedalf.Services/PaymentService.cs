using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using Gamedalf.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                .Where(e => e.Player.UserName.Contains(q))
                .OrderBy(e => e.Player)
                .ToListAsync();
        }

    }
}
