using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.CustomerModules;
using BusinessManagementSystemApp.Core.Repositories.CustomerModules;

namespace BusinessManagementSystemApp.Persistense.Repositories.CustomerModules
{
    public partial class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        {

        }
    }
}