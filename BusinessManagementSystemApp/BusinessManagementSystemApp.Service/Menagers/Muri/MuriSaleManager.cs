using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Models.Muri;
using BusinessManagementSystemApp.Core.ViewModels.MuriSell;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Service.Menagers.Muri
{
    public class MuriSaleManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public MuriSaleManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }


        public MuriSale Get(int id)
        {
            return _unitOfWork.MuriSale.Get(id);
        }

        public MuriSale GetSale(string year, string month, int clienId)
        {
            return _unitOfWork.MuriSale.SingleOrDefault(c =>
                c.Year == year && c.SalesMonth == month && c.ClientInfoId == clienId);
        }


        public IEnumerable<MuriSale> GetAll()
        {
            return _unitOfWork.MuriSale.GetAll().ToList();
        }


        public MuriSale SingleOrDefault(Expression<Func<MuriSale, bool>> predicate)
        {
            return _unitOfWork.MuriSale.SingleOrDefault(predicate);
        }

        public int Add(MuriSellVm vm, string user)
        {
            var category = Mapper.Map<MuriSellVm, MuriSale>(vm);
            _unitOfWork.MuriSale.Add(category);
            return _unitOfWork.Complete();
        }

        public int Update(int id, MuriSellVm vm)
        {
            try
            {
                var saleInDb = _unitOfWork.MuriSale.Get(id);
                if (saleInDb == null) return 0;
                Mapper.Map(vm, saleInDb);
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Update Fail. Error: " + e.Message);
            }
        }
    }
}
