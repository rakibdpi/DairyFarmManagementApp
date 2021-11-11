using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Repositories.MilkManagement;

namespace BusinessManagementSystemApp.Persistense.Repositories.MilkManagement
{
    public class AreaRepository : Repository<Area>,IAreaRepository
    {
        public AreaRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Area> LoadForeignEntities()
        {
            return Context.Set<Area>().Where(c => !c.IsDelete).Include(c => c.DeliveryMan).ToList();
        }
    }
}