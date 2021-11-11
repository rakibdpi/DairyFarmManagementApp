using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;

namespace BusinessManagementSystemApp.Core.Models.Ghee
{
    public class GheeSale
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public int ClientInfoId { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public string SalesMonth { get; set; }
        public string Year { get; set; }
        public int OneFourthKg { get; set; }
        public int HalfKg { get; set; }
        public int OneKg { get; set; }
    }
}