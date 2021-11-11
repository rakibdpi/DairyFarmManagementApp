using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessManagementSystemApp.Core.Models.SupplierModules;

namespace BusinessManagementSystemApp.Core.Models.PurchaseModules
{
    public class Purchase : BaseDomain
    {
        public Purchase()
        {
            PurchaseDetails=new List<PurchaseDetails>();
        }

        public IEnumerable<PurchaseDetails> PurchaseDetails { get; set; } 
        public int Id { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public string BillOrInvoiceNo { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }


    }
}