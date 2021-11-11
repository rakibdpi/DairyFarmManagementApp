using System;

namespace BusinessManagementSystemApp.Core.Dtos.MilkSellsDtos
{
    public class RowSaleDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public decimal Quantity { get; set; }
        public DateTime SalesDate { get; set; }
    }
}