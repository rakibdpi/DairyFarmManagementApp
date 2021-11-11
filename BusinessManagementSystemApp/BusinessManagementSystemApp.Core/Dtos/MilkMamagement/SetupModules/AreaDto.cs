using BusinessManagementSystemApp.Core.Models.SetupModules;

namespace BusinessManagementSystemApp.Core.Dtos.MilkMamagement.SetupModules
{
    public class AreaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CodeNo { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
        public int DeliveryManId { get; set; }
        public bool IsActive { get; set; }
    }
}