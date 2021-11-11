using BusinessManagementSystemApp.Core.Models.Sales;
using BusinessManagementSystemApp.Core.Models.SetupModules;

namespace BusinessManagementSystemApp.Core.ViewModels
{
    public class SalesDetailsViewModel
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Weight { get; set; }
        public decimal UnitPrice { get; set; }

        public Sales Sales { get; set; }
        public int SalesId { get; set; }
    }
}