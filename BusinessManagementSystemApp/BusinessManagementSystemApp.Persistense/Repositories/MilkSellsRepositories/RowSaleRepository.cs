using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.MilkSells;
using BusinessManagementSystemApp.Core.Repositories.MilkSellsInterfaces;

namespace BusinessManagementSystemApp.Persistense.Repositories.MilkSellsRepositories
{
    public class RowSaleRepository:Repository<RowSale>, IRowSaleRepository
    {
        public RowSaleRepository(DbContext context) : base(context)
        {
        }
    }
}