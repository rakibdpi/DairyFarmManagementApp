using System;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;

namespace BusinessManagementSystemApp.Core.ViewModels.MilkSalesViewModel
{
    public class MilkSaleViewModel
    {
        public long Id { get; set; }
        public string ClientType { get; set; }  
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public string Year { get; set; }
        public int ClientInfoId { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public string SalesMonth { get; set; }        
        public int? OneKg { get; set; }
        public int? HalfKg { get; set; }
        public int? SevenAndHalfGm { get; set; }

        // Row Sale
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public decimal Quantity { get; set; }
        public int StartDate { get; set; }

          
        
    }
}