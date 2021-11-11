using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkSells;

namespace BusinessManagementSystemApp.Core.Repositories.MilkSellsInterfaces
{
    public interface IPacketSaleRepository: IRepository<PacketSale>
    {
        IEnumerable<PacketSale> GetAllInclude();
    }
}