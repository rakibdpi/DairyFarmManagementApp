using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.MilkProduction;
using BusinessManagementSystemApp.Core.Repositories.ProductionInterfaces;

namespace BusinessManagementSystemApp.Persistense.Repositories.ProductionRepositories
{
    public class ProductionRepository:Repository<Production>, IProductionRepository
    {
        public ProductionRepository(DbContext context) : base(context)
        {
            
        }

        public IEnumerable<Production> GetAllInclude()
        {
            var infos = Context.Set<Production>()
                .Where(c => !c.IsDelete)
                .Include(c => c.CowSetup)
                .ToList();
            return infos;
        }
    }
}