using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.MilkPurchases;
using BusinessManagementSystemApp.Core.Repositories.MilkPurchase;

namespace BusinessManagementSystemApp.Persistense.Repositories.MilkPurchaseRepositories
{
    public class MilkSupplierRepository : Repository<MilkSuppliers>, IMilkSupplierRepository
    {
        public MilkSupplierRepository(DbContext context) : base(context)
        {
        }
    }
}