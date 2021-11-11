using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.MilkPurchases;
using BusinessManagementSystemApp.Core.Repositories.MilkPurchase;

namespace BusinessManagementSystemApp.Persistense.Repositories.MilkPurchaseRepositories
{
    public class MilkPurchaseRepository:Repository<MilkPurchase>, IMilkPurchaseRepository
    {
        public MilkPurchaseRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<MilkPurchase> GetAllInclude(int supplierId)
        {
            var entity = Context.Set<MilkPurchase>()
                .Where(c => !c.IsDelete && c.MilkSuppliersId == supplierId)
                .Include(c => c.MilkSuppliers)
                .ToList();
            return entity;
        }
    }
}