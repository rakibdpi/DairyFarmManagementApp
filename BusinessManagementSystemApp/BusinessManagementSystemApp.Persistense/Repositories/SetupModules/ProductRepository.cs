using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Core.Repositories.SetupModules;

namespace BusinessManagementSystemApp.Persistense.Repositories.SetupModules
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetAllInclude()
        {
            return Context.Set<Product>().Where(c => !c.IsDelete)
                .ToList();
        }
    }
}