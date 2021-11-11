using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.MilkProduction;
using BusinessManagementSystemApp.Core.Repositories.ProductionInterfaces;

namespace BusinessManagementSystemApp.Persistense.Repositories.ProductionRepositories
{
    public class PacketTypeRepository:Repository<PacketType>, IPacketTypeRepository
    {
        public PacketTypeRepository(DbContext context) : base(context)
        {
        }
    }
}