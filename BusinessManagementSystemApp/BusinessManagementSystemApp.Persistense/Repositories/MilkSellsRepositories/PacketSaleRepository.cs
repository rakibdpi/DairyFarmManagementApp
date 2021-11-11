using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.MilkSells;
using BusinessManagementSystemApp.Core.Repositories.MilkSellsInterfaces;

namespace BusinessManagementSystemApp.Persistense.Repositories.MilkSellsRepositories
{
    public class PacketSaleRepository: Repository<PacketSale>, IPacketSaleRepository
    {
        public PacketSaleRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<PacketSale> GetAllInclude()
        {
            return Context.Set<PacketSale>().Where(c => !c.IsDelete).Include(c => c.ClientInfo).Include(c => c.Area)
                .ToList();
        }
    }
}