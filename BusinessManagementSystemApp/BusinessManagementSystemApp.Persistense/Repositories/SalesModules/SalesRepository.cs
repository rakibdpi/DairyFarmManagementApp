using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.Sales;
using BusinessManagementSystemApp.Core.Repositories.SalesModules;

namespace BusinessManagementSystemApp.Persistense.Repositories.SalesModules
{
    public class SalesRepository : Repository<Sales>,ISalesRepository
    {
        public SalesRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Sales> GetAllInclude()
        {
            return Context.Set<Sales>()
                .Include(c => c.Customer)
                .ToList();
        }
    }
}