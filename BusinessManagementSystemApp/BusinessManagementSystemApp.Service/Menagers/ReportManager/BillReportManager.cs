using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BusinessManagementSystemApp.Core.ReportModels;
using BusinessManagementSystemApp.Core.ReportModels.OilReport;
using BusinessManagementSystemApp.Persistense.ReportRepositories;
using BusinessManagementSystemApp.Service.Menagers.Ghee;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;

namespace BusinessManagementSystemApp.Service.Menagers.ReportManager
{
    public class BillReportManager
    {
        private readonly PaymentService _paymentService;
        private readonly BillReportRepository _billRepository;
        private readonly DueBillManager _manager;
        private readonly GheeSaleMenager _gheeSaleMenager;
        public BillReportManager()
        {
            _billRepository= new BillReportRepository();
            _manager = new DueBillManager();
            _gheeSaleMenager = new GheeSaleMenager();
            _paymentService = new PaymentService();
        }

        public decimal BillForView(int clientId, string month,string year)
        {
            decimal priceHalfKg = 43;
            decimal priceSevenHalf = 65;
            decimal priceOneKg = 85;

            decimal priceOneKgOil = 125;
            decimal priceTwoKgOil = 250;
            decimal priceFiveKgOil = 1250;


            var info = _billRepository.GetMasterInfo(clientId, month,year).ToList();
            var dueBill = _manager.GetDueBill(clientId, month,year);
            var bill = _billRepository.GetBillInfo(clientId, month,year);
            var billReports = bill as BillReport[] ?? bill.ToArray();
            var totalNumberOfHalfKg = billReports.Sum(c => c.HalfKg);
            var totalNumberOfSevenHalf = billReports.Sum(c => c.SevenAndHalfGm);
            var totalNumberOfOneKg = billReports.Sum(c => c.OneKg);
            var totalAmountOfHalfKg = totalNumberOfHalfKg * priceHalfKg;
            var totalAmountOfSevenHalf = totalNumberOfSevenHalf * priceSevenHalf;
            var totalAmountOfOneKg = totalNumberOfOneKg * priceOneKg;

            var totalMilkAmount = totalAmountOfHalfKg + totalAmountOfOneKg + totalAmountOfSevenHalf;


            var oilBil = _billRepository.GetOilBillInfo(clientId, month,year);
            var oilBillReports = oilBil as OilBillReport[] ?? oilBil.ToArray();
            var totalNumberOfOneKgOil = oilBillReports.Sum(c => c.OneKg);
            var totalNumberOfTwoKgOil = oilBillReports.Sum(c => c.TwoKg);
            var totalNumberOfFiveKgOil = oilBillReports.Sum(c => c.FiveKg);
            var totalAmountOfOneKgOil = totalNumberOfOneKgOil * priceOneKgOil;
            var totalAmountOfTwoKgOil = totalNumberOfTwoKgOil * priceTwoKgOil;
            var totalAmountOfFiveKgOil = totalNumberOfFiveKgOil * priceFiveKgOil;

            var totalOilAmount = totalAmountOfOneKgOil + totalAmountOfTwoKgOil + totalAmountOfFiveKgOil;

            var getGhee = _gheeSaleMenager.GetAll()
                .SingleOrDefault(c => c.SalesMonth == month && c.ClientInfoId == clientId);

            var numberOfOneFourthKgGhee = 0;
            var numberOfHalfKgGhee = 0;
            var numberOfOneKgGhe = 0;

            var totalAmountOfOneFourthKgGhee = 0.00;
            var totalAmountOfHalfKgGhee = 0.00;
            var totalAmountOfOneKgGhe = 0.00;
            var gheeSubTotal = 0.00;




            if (getGhee != null)
            {
                numberOfOneFourthKgGhee = getGhee.OneFourthKg;
                numberOfHalfKgGhee = getGhee.HalfKg;
                numberOfOneKgGhe = getGhee.OneKg;

                totalAmountOfOneFourthKgGhee = numberOfOneFourthKgGhee * 350;
                totalAmountOfHalfKgGhee = numberOfHalfKgGhee * 700;
                totalAmountOfOneKgGhe = numberOfOneKgGhe * 1400;
                gheeSubTotal = totalAmountOfOneFourthKgGhee + totalAmountOfHalfKgGhee + totalAmountOfOneKgGhe;

            }





            decimal subTotal = 0;
     
            if (dueBill != null && dueBill.AmountType == "Add")
            {
                subTotal = totalMilkAmount + totalOilAmount + Convert.ToDecimal(gheeSubTotal) + dueBill.DueAmount;
                
            }

            else if (dueBill != null && dueBill.AmountType == "Deduct")
            {
                subTotal = (totalMilkAmount + totalOilAmount + Convert.ToDecimal(gheeSubTotal)) - dueBill.DueAmount;
              
            }
            else
            {
                subTotal = totalMilkAmount + totalOilAmount + Convert.ToDecimal(gheeSubTotal);
            }

            return subTotal;
        }
        public IEnumerable<BilReportMaster> GetMaster(int clientId, string month,string year)
        {
            decimal priceHalfKg = 43;
            decimal priceSevenHalf = 65;
            decimal priceOneKg = 85;

            decimal priceOneKgOil = 125;
            decimal priceTwoKgOil = 250;
            decimal priceFiveKgOil = 1250;


            var info = _billRepository.GetMasterInfo(clientId, month,year).ToList();
            var dueBill = _manager.GetDueBill(clientId, month ,year);
            var bill = _billRepository.GetBillInfo(clientId, month,year);
            var billReports = bill as BillReport[] ?? bill.ToArray();
            var totalNumberOfHalfKg = billReports.Sum(c => c.HalfKg);
            var totalNumberOfSevenHalf = billReports.Sum(c => c.SevenAndHalfGm);
            var totalNumberOfOneKg = billReports.Sum(c => c.OneKg);
            var totalAmountOfHalfKg = totalNumberOfHalfKg * priceHalfKg;
            var totalAmountOfSevenHalf = totalNumberOfSevenHalf * priceSevenHalf;
            var totalAmountOfOneKg = totalNumberOfOneKg * priceOneKg;

            var totalMilkAmount = totalAmountOfHalfKg + totalAmountOfOneKg + totalAmountOfSevenHalf;


            var oilBil = _billRepository.GetOilBillInfo(clientId, month,year);
            var oilBillReports = oilBil as OilBillReport[] ?? oilBil.ToArray();
            var totalNumberOfOneKgOil = oilBillReports.Sum(c => c.OneKg);
            var totalNumberOfTwoKgOil = oilBillReports.Sum(c => c.TwoKg);
            var totalNumberOfFiveKgOil = oilBillReports.Sum(c => c.FiveKg);
            var totalAmountOfOneKgOil = totalNumberOfOneKgOil * priceOneKgOil;
            var totalAmountOfTwoKgOil = totalNumberOfTwoKgOil * priceTwoKgOil;
            var totalAmountOfFiveKgOil = totalNumberOfFiveKgOil * priceFiveKgOil;
            var totalOilAmount = totalAmountOfOneKgOil + totalAmountOfTwoKgOil + totalAmountOfFiveKgOil;




            var getGhee = _gheeSaleMenager.GetAll()
                .SingleOrDefault(c => c.SalesMonth == month && c.ClientInfoId == clientId);

            var numberOfOneFourthKgGhee = 0;
            var numberOfHalfKgGhee = 0;
            var numberOfOneKgGhe = 0;

            var totalAmountOfOneFourthKgGhee = 0.00;
            var totalAmountOfHalfKgGhee = 0.00;
            var totalAmountOfOneKgGhe = 0.00;
            var gheeSubTotal = 0.00;




            if (getGhee != null)
            {
                 numberOfOneFourthKgGhee = getGhee.OneFourthKg;
                 numberOfHalfKgGhee = getGhee.HalfKg;
                 numberOfOneKgGhe = getGhee.OneKg;

                 totalAmountOfOneFourthKgGhee = numberOfOneFourthKgGhee * 350;
                 totalAmountOfHalfKgGhee = numberOfHalfKgGhee * 700;
                 totalAmountOfOneKgGhe = numberOfOneKgGhe * 1400;
                 gheeSubTotal = totalAmountOfOneFourthKgGhee + totalAmountOfHalfKgGhee + totalAmountOfOneKgGhe;

            }
       


   

            decimal subTotal = 0;
            string dueAmount = "";
            if (dueBill != null &&  dueBill.AmountType== "Add")
            {
                subTotal = totalMilkAmount + totalOilAmount + Convert.ToDecimal(gheeSubTotal)  + dueBill.DueAmount;
                dueAmount = "+" + dueBill.DueAmount;
            }

            else if (dueBill != null && dueBill.AmountType== "Deduct")
            {
                subTotal = ( totalMilkAmount + totalOilAmount + Convert.ToDecimal(gheeSubTotal)) - dueBill.DueAmount;
                dueAmount = "-" + dueBill.DueAmount;
            }
            else
            {
                subTotal = totalMilkAmount + totalOilAmount + Convert.ToDecimal(gheeSubTotal);
                dueAmount = "00";
            }
            
            foreach (var master in info)
            {
                master.Customer = master.Customer;
                master.PriceHalfKg = priceHalfKg;
                master.PriceSevenHalfGm = priceSevenHalf;
                master.PriceOneKg = priceOneKg;
                master.NumberOfHalfKg = totalNumberOfHalfKg;
                master.NumberOfSevenHalfGm = totalNumberOfSevenHalf;
                master.NumberOfOneKg = totalNumberOfOneKg;
                master.TotalAmountOfHalfKg = totalAmountOfHalfKg;
                master.TotalAmountOfSevenHalfGm = totalAmountOfSevenHalf;
                master.TotalAmountOneKg = totalAmountOfOneKg;
                master.MilkSubTotal = totalMilkAmount;

                master.NumberOfOneKgOil = totalNumberOfOneKgOil;
                master.NumberOfTwoKgOil = totalNumberOfTwoKgOil;
                master.NumberOfFiveKgOil = totalNumberOfFiveKgOil;
                master.TotalAmountOfOneKgOil = totalAmountOfOneKgOil;
                master.TotalAmountOfTwoKgOil = totalAmountOfTwoKgOil;
                master.TotalAmountOfFiveKgOil = totalAmountOfFiveKgOil;
                master.OilSubTotal = totalOilAmount;

                master.NumberOfOneFourthKgGhee = numberOfOneFourthKgGhee;
                master.NumberOfHalfKgGhee = numberOfHalfKgGhee;
                master.NumberOfOneKgGhe = numberOfOneKgGhe;

                master.TotalAmountOfOneFourthKgGhee =Convert.ToDecimal(totalAmountOfOneFourthKgGhee);
                master.TotalAmountOfHalfKgGhee = Convert.ToDecimal(totalAmountOfHalfKgGhee);
                master.TotalAmountOfOneKgGhe = Convert.ToDecimal(totalAmountOfOneKgGhe);
                master.GheeSubTotal =Convert.ToDecimal(gheeSubTotal);


                master.DueBill =dueAmount ;
                master.SubTotal = subTotal;
            }
            return info;
        }

        public IEnumerable<BillReport> GetBillInfo(int clientId, string month, string year)
        {
            var info = _billRepository.GetBillInfo(clientId, month,year).ToList();
            return info;
        }

        public IEnumerable<RunningPaymentInfo> RunningPaymentDue(int areaId, string year, string month)
        {
            var paymentInfo = _paymentService.GetAll().Where(c => c.AreaId == areaId && c.Year == year && c.Month == month).ToList();

            var infos = new List<RunningPaymentInfo>();

            foreach (var item in paymentInfo)
            {
                var bill = BillForView(item.ClientInfoId, month,year);

                var payinfo = new RunningPaymentInfo()
                {
                    Area = item.Area.Name,
                    Name = item.ClientInfo.Name,
                    PhoneNo = item.ClientInfo.PhoneNo,
                    BillAmount = bill,
                    PayAmount = item.BillAmount,
                    DueAmount = bill - item.BillAmount
                };
                infos.Add(payinfo);
            }

            return infos;
        }


    }
}
