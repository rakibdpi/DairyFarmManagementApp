using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Repositories.MilkManagement;

namespace BusinessManagementSystemApp.Persistense.Repositories.MilkManagement
{
    public class ClientInfoRepository : Repository<ClientInfo>, IClientInfoRepository
    {
        public ClientInfoRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<ClientInfo> GetAllInclude()
        {
            return Context.Set<ClientInfo>().Where(c => !c.IsDelete)
                .Include(c => c.Area)
                .ToList();
        }
    }
}