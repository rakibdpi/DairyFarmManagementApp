using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Core.Repositories.MilkManagement;

namespace BusinessManagementSystemApp.Persistense.Repositories.MilkManagement
{
    public class DeliveryManRepository:Repository<DeliveryMan>,IDeliveryManRepository
    {
        public DeliveryManRepository(DbContext context) : base(context)
        {
        }
    }
}