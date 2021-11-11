using System;
using BusinessManagementSystemApp.Core.Models.CustomerModules;

namespace BusinessManagementSystemApp.Core.Models.Sales
{
    public class Transaction
    {
        public int Id { get; set; }

        public string InvoiceNo { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal PayAmount { get; set; }

        public int? ReferenceTableId { get; set; }
    }
}