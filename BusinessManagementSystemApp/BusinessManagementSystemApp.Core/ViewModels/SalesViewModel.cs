using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessManagementSystemApp.Core.Models.CustomerModules;
using BusinessManagementSystemApp.Core.Models.Sales;
using BusinessManagementSystemApp.Core.Models.SetupModules;

namespace BusinessManagementSystemApp.Core.ViewModels
{
    public class SalesViewModel
    {
        public SalesViewModel()
        {
            SalesDetails = new List<SalesDetailsViewModel>();

        }
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal TotalBill { get; set; }
        public decimal? TransportCost { get; set; }
        public decimal? OtherBill { get; set; }
        public decimal? Discount { get; set; }
        public decimal Payable { get; set; }
        public decimal? PayAmount { get; set; }
        public IEnumerable<SalesDetailsViewModel> SalesDetails { get; set; }


        //view
        public string CustomerName { get; set; }
        public string PhoneNo { get; set; }

    }
}