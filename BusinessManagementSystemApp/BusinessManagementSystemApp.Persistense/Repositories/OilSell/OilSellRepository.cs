using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.OilSell;
using BusinessManagementSystemApp.Core.Repositories.OilSell;

namespace BusinessManagementSystemApp.Persistense.Repositories.OilSell
{
    public class OilSellRepository : Repository<OilSells>,IOilSellRepository
    {
        public OilSellRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<OilSells> GetAllInclude()
        {
            return Context.Set<OilSells>().Where(c => !c.IsDelete)
                .Include(c => c.ClientInfo)
                .Include(c => c.Area)
                .ToList();
        }
    }
}