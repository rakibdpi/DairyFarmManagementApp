using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.AsthaShop;
using BusinessManagementSystemApp.Core.Repositories.AsthaOnlineShop;

namespace BusinessManagementSystemApp.Persistense.Repositories.AsthaOnline
{
    public class DataTypeRepository : Repository<DataType>,IDataTypeRepository
    {
        public DataTypeRepository(DbContext context) : base(context)
        {
        }
    }
}