using System.Collections.Generic;
using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.AsthaShop;
using BusinessManagementSystemApp.Core.Repositories;
using BusinessManagementSystemApp.Core.Repositories.AsthaOnlineShop;

namespace BusinessManagementSystemApp.Persistense.Repositories.AsthaOnline
{
    public class DatatransectionRepository : Repository<TransectionData>, IDatatransectionRepository
    {
        public DatatransectionRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<TransectionData> GetAllInclude()
        {

            return Context.Set<TransectionData>()
                .Include(c => c.DataType);

        }
    }
}