using System;
using System.ComponentModel.DataAnnotations;
using BusinessManagementSystemApp.Core.Models.SetupModules;


namespace BusinessManagementSystemApp.Core.Dtos.Purchase
{
    public class PurchaseDetailsDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public string ManufacturedDate { get; set; }

        [Required]
        public string ExpireDate { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double UnitPrize { get; set; }

        [Required]
        public double TotalTk { get; set; }

        [Required]
        public double MrpTk { get; set; }

        public int PurchaseId { get; set; }
        public Models.PurchaseModules.Purchase Purchase { get; set; }
        public string Remarks { get; set; }
    }
}