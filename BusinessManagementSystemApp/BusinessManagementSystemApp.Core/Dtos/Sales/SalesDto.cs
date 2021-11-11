using System;
using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.CustomerModules;
using BusinessManagementSystemApp.Core.Models.Sales;

namespace BusinessManagementSystemApp.Core.Dtos.Sales
{
    public class SalesDto
    {
        public SalesDto()
        {
            SalesDetails = new List<SalesDetails>();
        }
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public string SalesDate { get; set; }
        public decimal TotalBill { get; set; }
        public decimal? TransportCost { get; set; }
        public decimal? OtherBill { get; set; }
        public decimal? Discount { get; set; }
        public decimal Payable { get; set; }
        public decimal PayAmount { get; set; }
        public ICollection<SalesDetails> SalesDetails { get; set; }
    }
}