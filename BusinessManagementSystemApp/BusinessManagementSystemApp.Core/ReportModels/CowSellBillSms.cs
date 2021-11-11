namespace BusinessManagementSystemApp.Core.ReportModels
{
    public class CowSellBillSms
    {
        public string CustomerName { get; set; }

        public string PhoneNo { get; set; }

        public string CowNo { get; set; }

        public string Weight { get; set; }

        public decimal Price { get; set; }

        public decimal PayAmount { get; set; }

        public decimal DueAmount { get; set; }
        public decimal TransportCost { get; set; }  

    }
}