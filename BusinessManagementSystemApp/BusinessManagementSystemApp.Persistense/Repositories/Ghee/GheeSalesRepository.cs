using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.Ghee;
using BusinessManagementSystemApp.Core.Repositories.Ghee;

namespace BusinessManagementSystemApp.Persistense.Repositories.Ghee
{
    public class GheeSalesRepository : Repository<GheeSale>, IGheeSalesRepository
    {
        public GheeSalesRepository(DbContext context) : base(context)
        {

        }
    }
}