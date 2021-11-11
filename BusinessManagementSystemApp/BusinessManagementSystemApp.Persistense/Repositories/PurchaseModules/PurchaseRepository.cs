using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;
using BusinessManagementSystemApp.Core.Repositories.PurchaseModules;

namespace BusinessManagementSystemApp.Persistense.Repositories.PurchaseModules
{
    public class PurchaseRepository : Repository<Purchase>,IPurchaseRepository
    {
        public PurchaseRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Purchase> GetAllInclude()
        {
            return Context.Set<Purchase>()
                .Include(c => c.Supplier)
                .ToList();
        }
    }
}