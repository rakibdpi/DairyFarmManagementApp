using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Core.Repositories.SetupModules;

namespace BusinessManagementSystemApp.Persistense.Repositories.SetupModules
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }
    }
}