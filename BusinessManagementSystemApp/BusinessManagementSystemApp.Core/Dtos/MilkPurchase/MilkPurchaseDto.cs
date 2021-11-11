using BusinessManagementSystemApp.Core.Models.MilkPurchases;

namespace BusinessManagementSystemApp.Core.Dtos.MilkPurchase
{
    public class MilkPurchaseDto
    {
        public int Id { get; set; }
        public int MilkSuppliersId { get; set; }
        public MilkSuppliers MilkSuppliers { get; set; }
        public decimal MilkQuantity { get; set; }
        public string PurchaseDate { get; set; }
    }
}