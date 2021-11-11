using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;
using BusinessManagementSystemApp.Core.Models.SupplierModules;

namespace BusinessManagementSystemApp.Core.Dtos.Purchase
{
    public class PurchaseDto
    {
        public PurchaseDto()
        {
            PurchaseDetails = new List<PurchaseDetailsDto>();
        }
        public int Id { get; set; }

        [Required]
        public string PurchaseDate { get; set; }

        [Required]
        public string BillOrInvoiceNo { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public IEnumerable<PurchaseDetailsDto> PurchaseDetails { get; set; }
    }
}