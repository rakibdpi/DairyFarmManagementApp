namespace BusinessManagementSystemApp.Core.Dtos.MilkProductionDtos
{
    public class ProductionReportDto
    {
        public int Date { get; set; }
        public string CowNo { get; set; }
        public decimal Morning { get; set; }
        public decimal Afternoon { get; set; }
        public decimal Night { get; set; }
        public decimal Other { get; set; }
        public decimal TotalQuantity { get; set; }
    }
}