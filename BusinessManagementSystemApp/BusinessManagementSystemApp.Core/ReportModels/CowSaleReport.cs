namespace BusinessManagementSystemApp.Core.ReportModels
{
    public class CowSaleReport
    {
        public string CustomerName { get; set; }

        public string CustomerId { get; set; }

        public string Address { get; set; }

        public string InvoiceNo { get; set; }

        public string PhoneNo { get; set; }

        public decimal TotalBill { get; set; }

        public decimal? TransportBill  { get; set; }

        public decimal Payable { get; set; }

        public decimal Payment { get; set; }

        public  decimal Due { get; set; }


    }
}