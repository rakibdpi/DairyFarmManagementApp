using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;
using BusinessManagementSystemApp.Core.Models.SupplierModules;

namespace BusinessManagementSystemApp.Core.ViewModels
{
    public class PurchaseViewModelForDataPass
    {
        public PurchaseViewModelForDataPass()
        {
            PurchaseDetails = new List<PurchaseDetails>();
        }

        public int Id { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public string BillOrInvoiceNo { get; set; }
        public string Code { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public IEnumerable<PurchaseDetails> PurchaseDetails { get; set; }

    }
}