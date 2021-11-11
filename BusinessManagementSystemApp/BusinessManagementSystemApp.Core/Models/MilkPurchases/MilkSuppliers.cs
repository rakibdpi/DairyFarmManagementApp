namespace BusinessManagementSystemApp.Core.Models.MilkPurchases
{
    public class MilkSuppliers : BaseDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
    }
}