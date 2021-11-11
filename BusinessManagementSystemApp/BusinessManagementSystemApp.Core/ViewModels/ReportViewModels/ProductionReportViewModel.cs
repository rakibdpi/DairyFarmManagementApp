using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Dtos.MilkProductionDtos;

namespace BusinessManagementSystemApp.Core.ViewModels.ReportViewModels
{
    public class ProductionReportViewModel
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public int Date { get; set; }
        public int CowId { get; set; }
    }

    public class ProductionReportDataSet
    {
        public ProductionReportDataSet()
        {
            Models= new List<ProductionReportDto>();
        }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string MobileNo { get; set; }
        public string YearParameter { get; set; }   
        public string MonthParameter { get; set; }
        public string DayParameter { get; set; }
        public string CowNumberParameter { get; set; }
        public List<ProductionReportDto> Models { get; set; } 
    }
}