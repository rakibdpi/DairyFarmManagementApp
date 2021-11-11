using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;

namespace BusinessManagementSystemApp.Core.Repositories.MilkManagement
{
    public interface IClientInfoRepository : IRepository<ClientInfo>
    {
        IEnumerable<ClientInfo> GetAllInclude();
    }
}