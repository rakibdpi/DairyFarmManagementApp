using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;
using BusinessManagementSystemApp.Core.Repositories.PurchaseModules;

namespace BusinessManagementSystemApp.Persistense.Repositories.PurchaseModules
{
    public class PurchaseDetailsRepository : Repository<PurchaseDetails>,IPurchaseDetailsRepository
    {
        public PurchaseDetailsRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<PurchaseDetails> GetAllInclude()
        {
            return Context.Set<PurchaseDetails>()
                .Include(c => c.Product)
                .Include(c => c.Purchase)
                .ToList();
        }
    }
}