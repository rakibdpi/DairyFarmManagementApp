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
                Year = vm.Year,
                ClientInfoId = vm.ClientId,
                MonthId = vm.MonthId,
                AmountType = vm.AmountType,
                DueAmount = vm.DueAmount
            };
            _unitOfWork.DueBill.Add(entity);
            return _unitOfWork.Complete();
        }

        public DueBills GetDueBill(int clientId, string month,string year)
        {
            var result = _unitOfWork.DueBill.GetAll().LastOrDefault(c => c.ClientInfoId == clientId && c.MonthId.ToLower().Trim() == month.ToLower().Trim() &&
                c.Year == year);
            return result;
        }

        public IEnumerable<DueBills> GetDueBillData(int areaId, string month, string year)
        {
            var result = _unitOfWork.DueBill.GetAll().Where(c => c.ClientInfo.AreaId == areaId && c.MonthId.ToLower().Trim() == month.ToLower().Trim() &&
                c.Year == year);
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
