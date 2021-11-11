using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;
using BusinessManagementSystemApp.Core.Models.Sales;

namespace BusinessManagementSystemApp.Core.Repositories.SalesModules
{
    public interface ISalesDetailsRepository : IRepository<SalesDetails>
    {
        IEnumerable<SalesDetails> GetAllInclude();
    }
}