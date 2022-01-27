using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Models.DueBill;
using BusinessManagementSystemApp.Core.ReportModels.ViewModels;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Service.Menagers.MilkManagement
{
    public class DueBillManager 
    {
        private readonly IUnitOfWork _unitOfWork;

        public DueBillManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }


        public int Add(BillReportViewModel vm)
        {
            var date = DateTime.Now;
            var entity = new DueBills() {
                Year = date.Year.ToString(),
                ClientInfoId = vm.ClientId,
                MonthId = vm.MonthId,
                AmountType = vm.AmountType,
                DueAmount = vm.DueAmount
            };
            _unitOfWork.DueBill.Add(entity);
            return _unitOfWork.Complete();
        }

        public DueBills GetDueBill(int clientId, string month)
        {
            var result = _unitOfWork.DueBill.GetAll().LastOrDefault(c => c.ClientInfoId == clientId && c.MonthId.ToLower().Trim() == month.ToLower().Trim() &&
                c.Year == DateTime.Now.Year.ToString());
            return result;
        } 

        public decimal Get(int clientId, string monthId)
        {
            decimal dueBill = 0;
            var entity = _unitOfWork.DueBill.GetData(clientId,monthId);

            if(entity != null)
            {
                dueBill = entity.DueAmount;
            }
        
            return dueBill;
        }

        public DueBills GetBill(int clientId, string monthId)
        {
            var entity = _unitOfWork.DueBill.GetData(clientId, monthId);
            return entity;
        }


    }
}
