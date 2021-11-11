using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;

namespace BusinessManagementSystemApp.Core.ViewModels.MilkMamagement
{
    public class AreaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CodeNo { get; set; }
        public int DeliveryManId { get; set; }  
        public bool IsActive { get; set; }
    }
}