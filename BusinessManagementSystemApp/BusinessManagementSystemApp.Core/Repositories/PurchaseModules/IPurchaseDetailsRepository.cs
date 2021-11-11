using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;

namespace BusinessManagementSystemApp.Core.Repositories.PurchaseModules
{
    public interface IPurchaseDetailsRepository : IRepository<PurchaseDetails>
    {
        IEnumerable<PurchaseDetails> GetAllInclude();
    }
}