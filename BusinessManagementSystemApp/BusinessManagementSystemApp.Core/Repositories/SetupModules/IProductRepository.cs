using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.SetupModules;

namespace BusinessManagementSystemApp.Core.Repositories.SetupModules
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllInclude();
    }
}