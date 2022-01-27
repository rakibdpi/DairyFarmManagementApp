using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.PaymentDtos;
using BusinessManagementSystemApp.Core.Models.Payments;
using BusinessManagementSystemApp.Core.ReportModels;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers
{
    public class PaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public PaymentDto Get(int id)
        {
            var entity = _unitOfWork.Payment.Get(id);
            return (Mapper.Map<Payment, PaymentDto>(entity));
        }

        public IEnumerable<PaymentDto> GetAll()
        {
            return _unitOfWork.Payment.GetInclude().Select(Mapper.Map<Payment, PaymentDto>);
        }


        public List<MilkDetailsSummary> GetMilkDetailsSummary(string year, string month)
        {
            var salesData = _unitOfWork.PacketSale.Find(c => c.SalesMonth == month && c.Year == year).ToList();

            //Total Packet Get
            var halfKgPacket = salesData.Sum(c => c.HalfKg);
            var sevenHalfKgPacket = salesData.Sum(c => c.SevenAndHalfGm);
            var oneKgPacket = salesData.Sum(c => c.OneKg);

            //Packet to Litter
            var halfKgTotal = ( halfKgPacket * 450 ) / 900;
            var sevenHalfKgTotal = (sevenHalfKgPacket * 675) / 900;
            var ongKgTotal = oneKgPacket;

            //Total Milk Litter
            var totalMilk = halfKgTotal + sevenHalfKgTotal + ongKgTotal;
            var totalMilkAmount = halfKgPacket * 43 + sevenHalfKgPacket * 65 + oneKgPacket * 85;

            //Oil Sale
            var oilSell = _unitOfWork.OilSell.Find(c => c.SalesMonth == month && c.Year == year).ToList();
            var oneLitterOil = oilSell.Sum(c => c.OneKg);
            var twoLitter = oilSell.Sum(c => c.TwoKg);
            var fiveLitter = oilSell.Sum(c => c.FiveKg);
            var totalOilPrice = oneLitterOil * 125 + twoLitter * 250 + fiveLitter * 1250;

            //Ghee Sale 
            var gheSale = _unitOfWork.GheeSale.Find(c => c.SalesMonth == month && c.Year == year).ToList();
            var oneFourGhee = gheSale.Sum(c => c.OneFourthKg);
            var halfGhee = gheSale.Sum(c => c.HalfKg);
            var oneGhee = gheSale.Sum(c => c.OneKg);
            var totalGheeAmount = oneFourGhee * 350 + halfGhee * 700 + oneGhee * 1400;

            // Total Due
            var addEntryDue = _unitOfWork.DueBill.GetAll().Where(c => c.MonthId == month && c.Year == year && c.AmountType == "Add").ToList()
                .Sum(c => c.DueAmount);
            var deductEntryDue = _unitOfWork.DueBill.GetAll().Where(c => c.MonthId == month && c.Year == year && c.AmountType == "Deduct").ToList()
                .Sum(c => c.DueAmount);

            var totalDue = addEntryDue - deductEntryDue;


            //Total Due 

            var totalBillAmount = totalMilkAmount + totalOilPrice + totalGheeAmount + totalDue;



            //Collection and Running Due
            var totalCollection = _unitOfWork.Payment.Find(c => c.Month == month && c.Year == year).Sum(c => c.BillAmount);
            var dueAmount = totalBillAmount - totalCollection;




            var list = new List<MilkDetailsSummary>();

            var detailSummary = new MilkDetailsSummary()
            {
                TotalMilkSale = totalMilk,
                TotalMilkAmount = totalMilkAmount,
                TotalCollection = totalCollection,
                TotalDue = dueAmount,
                OneKgPacket = oneKgPacket,
                HalfKgPacket = halfKgPacket,
                SevenHalfKgPacket = sevenHalfKgPacket,
                TotalOilPrice = totalOilPrice,
                TotalGhePrice = totalGheeAmount,
                TotalAmount = totalBillAmount,
                EntryDue = totalDue

            };

            list.Add(detailSummary);

            return list;
        }





        public int Add(List<Payment> dto)
        {
            _unitOfWork.Payment.AddRange(dto);
            return _unitOfWork.Complete();
        }

        public int Update(int id, PaymentDto dto, string user)
        {
            var customerInDb = _unitOfWork.Payment.Get(id);
            if (customerInDb == null) return 0;
            var createBy = customerInDb.CreateBy;
            var createDate = customerInDb.CreateDate;
            Mapper.Map(dto, customerInDb);
            customerInDb.CreateBy = createBy;
            customerInDb.CreateDate = createDate;
            customerInDb.UpdateBy = user;
            customerInDb.UpdateDate = DateTime.Now;
            return _unitOfWork.Complete();
        }
        public int LogicalRemove(int id, string user)
        {
            var customerInDb = _unitOfWork.Payment.Get(id);
            if (customerInDb == null) return 0;

            customerInDb.IsDelete = true;
            customerInDb.DeleteBy = user;
            customerInDb.DeleteDate = DateTime.Now;
            return _unitOfWork.Complete();
        }
    }
}