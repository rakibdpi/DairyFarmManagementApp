using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.Sales;
using BusinessManagementSystemApp.Core.Repositories.SalesModules;

namespace BusinessManagementSystemApp.Persistense.Repositories.SalesModules
{
    public class SalesDetailsRepository : Repository<SalesDetails>,ISalesDetailsRepository
    {
        public SalesDetailsRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<SalesDetails> GetAllInclude()
        {
            return Context.Set<SalesDetails>()
                .Include(c => c.Product)
                .Include(c => c.Sales)
                .ToList();
        }
    }
}