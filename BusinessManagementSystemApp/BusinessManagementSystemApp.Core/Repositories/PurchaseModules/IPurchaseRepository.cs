using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;

namespace BusinessManagementSystemApp.Core.Repositories.PurchaseModules
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        IEnumerable<Purchase> GetAllInclude();
    }
}