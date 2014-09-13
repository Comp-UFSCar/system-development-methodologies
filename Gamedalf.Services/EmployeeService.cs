using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Gamedalf.Services
{
    public class EmployeeService : Service<Employee>
    {
        public EmployeeService(ApplicationDbContext db) : base(db) { }

        public virtual async Task<ICollection<Employee>> Search(string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return await All();
            }

            return await db.Employees
                .Where(e => e.Email.Contains(q))
                .OrderBy(e => e.Email)
                .ToListAsync();
        }
    }
}
