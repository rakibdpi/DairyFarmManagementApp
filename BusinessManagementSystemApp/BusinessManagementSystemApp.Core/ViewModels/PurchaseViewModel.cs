using System;
using System.ComponentModel.DataAnnotations;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Core.Models.SupplierModules;

namespace BusinessManagementSystemApp.Core.ViewModels
{
    public class PurchaseViewModel
    {
        //Purchase
        public int Id { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public string BillOrInvoiceNo { get; set; }
        public string Code { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        //Purchase Details

        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public DateTime ManufacturedDate { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double UnitPrize { get; set; }

        [Required]
        public double TotalTk { get; set; }

        [Required]
        [Display(Name = "New Mrp Tk")]
        public double MrpTk { get; set; }

        [Display(Name = "Previous Cost Prize")]
        public double PreviousCostPrize { get; set; }

        [Display(Name = "Previous Mrp Tk")]
        public double PreviousMrpTk { get; set; }
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public string Remarks { get; set; }
    }
}