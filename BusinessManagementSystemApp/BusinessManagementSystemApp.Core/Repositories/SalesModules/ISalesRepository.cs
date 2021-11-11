using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.Sales;

namespace BusinessManagementSystemApp.Core.Repositories.SalesModules
{
    public interface ISalesRepository : IRepository<Sales>
    {
        IEnumerable<Sales> GetAllInclude();
    }
}