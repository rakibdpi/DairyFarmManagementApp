using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.SupplierModules;
using BusinessManagementSystemApp.Core.Repositories.SupplierModules;

namespace BusinessManagementSystemApp.Persistense.Repositories.SupplierRepository
{
    public class SupplierRepository : Repository<Supplier>,ISupplierRepository
    {
        public SupplierRepository(DbContext context) : base(context)
        {
        }
    }
}