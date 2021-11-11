using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.MilkSells;
using BusinessManagementSystemApp.Core.Models.OilSell;

namespace BusinessManagementSystemApp.Core.Repositories.OilSell
{
    public interface IOilSellRepository : IRepository<OilSells>
    {
        IEnumerable<OilSells> GetAllInclude();
    }
}