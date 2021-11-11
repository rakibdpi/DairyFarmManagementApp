using BusinessManagementSystemApp.Core.Models.SetupModules;

namespace BusinessManagementSystemApp.Core.Dtos.Sales
{
    public class SalesDetailsDto
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Weight { get; set; }
        public decimal UnitPrice { get; set; }

        public Models.Sales.Sales Sales { get; set; }
        public int SalesId { get; set; }
    }
}