using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.AsthaShop;

namespace BusinessManagementSystemApp.Core.Repositories.AsthaOnlineShop
{
    public interface IDatatransectionRepository : IRepository<TransectionData>
    {
        IEnumerable<TransectionData> GetAllInclude();

    }
}