using Gamedalf.Core.Data;
using Gamedalf.Core.Models;

namespace Gamedalf.Services
{
    public class EmployeeService : Service<Employee>
    {
        public EmployeeService(ApplicationDbContext db) : base(db) { }
    }
}
