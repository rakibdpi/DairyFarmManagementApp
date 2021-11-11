using System.Collections.Generic;

namespace BusinessManagementSystemApp.Core.Dtos.MilkPurchase
{
    public class PurchaseReportDataSet
    {
        public PurchaseReportDataSet()
        {
            Models = new List<PurchaseReportDto>();
        }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string MobileNo { get; set; }
        public string YearParameter { get; set; }
        public string MonthParameter { get; set; }
        public string DayParameter { get; set; }    
        public string SupplierParameter { get; set; }
        public List<PurchaseReportDto> Models { get; set; }
    }
}