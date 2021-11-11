using System;

namespace BusinessManagementSystemApp.Core.Models.MilkPurchases
{
    public class MilkPurchase : BaseDomain
    {
        public int Id { get; set; }
        public int MilkSuppliersId { get; set; }
        public MilkSuppliers MilkSuppliers { get; set; }
        public decimal MilkQuantity { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}