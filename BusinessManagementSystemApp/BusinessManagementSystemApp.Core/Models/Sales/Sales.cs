using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManagementSystemApp.Core.Models.CustomerModules;

namespace BusinessManagementSystemApp.Core.Models.Sales
{
    public class Sales : BaseDomain
    {
        public Sales()
        {
            SalesDetails = new List<SalesDetails>();
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
        public ICollection<SalesDetails> SalesDetails { get; set; }

    }
}
