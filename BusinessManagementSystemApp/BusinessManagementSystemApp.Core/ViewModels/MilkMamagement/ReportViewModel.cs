using System.Collections.Generic;

namespace BusinessManagementSystemApp.Core.ViewModels.MilkMamagement
{
    public class ReportViewModel
    {
        
    }

    public class SalesReportViewModel
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public int DeliveryManId { get; set; }
        public int AreaId { get; set; } 
    }

    public class SaleReportByDeliveryMenViewModel
    {
        public string DeliveryMan { get; set; }
        public int DeliveryManId { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int HalfKg { get; set; }
        public int SevenHalf { get; set; }
        public int OneKg { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalCollection { get; set; }    
    }
    public class SaleReportByDeliveryMenMasterViewModel
    {
        public SaleReportByDeliveryMenMasterViewModel()
        {
            Models= new List<SaleReportByDeliveryMenViewModel>();
        }
        public string CompanyName { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public List<SaleReportByDeliveryMenViewModel> Models { get; set; }  
    }
}