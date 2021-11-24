namespace BusinessManagementSystemApp.Core.ReportModels.ViewModels
{
    public class BillReportViewModel
    {
        public int AreaId { get; set; } 
        public int ClientId { get; set; }
        public string MonthId { get; set; }
        public string AmountType { get; set; }  
        public decimal DueAmount { get; set; }

        public int? OneFourthKg { get; set; }
        public int? HalfKg { get; set; }
        public int? OneKg { get; set; }

        public int StartDate { get; set; }

        public int Id { get; set; }

        public int? OilOneKg { get; set; }
        public int? OilTwoKg { get; set; }
        public int? OilFiveKg { get; set; }

        public int? MuriHalfKg { get; set; }
        public int? MuriOneKg { get; set; }
    }
}