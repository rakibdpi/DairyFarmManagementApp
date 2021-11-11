using System;

namespace BusinessManagementSystemApp.Core.ReportModels
{
    public class BilReportMaster
    {
        public string Customer { get; set; }
        public string PhoneNo { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string AreaName { get; set; }
        public string NameOfMonth { get; set; } 
        public int NumberOfHalfKg { get; set; }
        public int NumberOfSevenHalfGm { get; set; }
        public int NumberOfOneKg { get; set; }
        public decimal PriceHalfKg { get; set; }
        public decimal PriceSevenHalfGm { get; set; }
        public decimal PriceOneKg { get; set; } 
        public decimal TotalAmountOfHalfKg { get; set; }
        public decimal TotalAmountOfSevenHalfGm { get; set; }
        public decimal TotalAmountOneKg { get; set; }
        public decimal MilkSubTotal { get; set; }

        public decimal SubTotal { get; set; }
        public string DueBill { get; set; }

        public int NumberOfOneKgOil { get; set; }
        public int NumberOfTwoKgOil { get; set; }
        public int NumberOfFiveKgOil { get; set; }
        public decimal TotalAmountOfOneKgOil { get; set; }
        public decimal TotalAmountOfTwoKgOil { get; set; }
        public decimal TotalAmountOfFiveKgOil { get; set; }
        public decimal OilSubTotal { get; set; }


        public int NumberOfOneFourthKgGhee { get; set; }
        public int NumberOfHalfKgGhee { get; set; }
        public int NumberOfOneKgGhe { get; set; }
        public decimal TotalAmountOfOneFourthKgGhee { get; set; }
        public decimal TotalAmountOfHalfKgGhee { get; set; }
        public decimal TotalAmountOfOneKgGhe { get; set; }
        public decimal GheeSubTotal { get; set; }






    }
}