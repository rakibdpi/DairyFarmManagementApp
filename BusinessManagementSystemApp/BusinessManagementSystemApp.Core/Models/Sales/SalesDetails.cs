using BusinessManagementSystemApp.Core.Models.SetupModules;

namespace BusinessManagementSystemApp.Core.Models.Sales
{
    public class SalesDetails
    {
        public int Id { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Weight { get; set; }
        public decimal UnitPrice { get; set; }

        public Sales Sales { get; set; }
        public int SalesId { get; set; }
    }
}