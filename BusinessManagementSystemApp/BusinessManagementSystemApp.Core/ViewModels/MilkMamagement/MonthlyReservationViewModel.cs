using BusinessManagementSystemApp.Core.Models.MilkMamagement;

namespace BusinessManagementSystemApp.Core.ViewModels.MilkMamagement
{
    public class MonthlyReservationViewModel
    {
        public long Id { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public int ClientInfoId { get; set; }
        public int Area { get; set; } 
    }
}