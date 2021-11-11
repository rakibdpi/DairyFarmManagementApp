using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Models.Ghee;
using BusinessManagementSystemApp.Core.ViewModels.GheeSale;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.Ghee
{
    public class GheeSaleMenager
    {
        private readonly IUnitOfWork _unitOfWork;

        public GheeSaleMenager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }


        public GheeSale Get(int id)
        {
            return _unitOfWork.GheeSale.Get(id);
        }

        public GheeSale GetSale(string year,string month,int clienId)
        {
            return _unitOfWork.GheeSale.SingleOrDefault(c =>
                c.Year == year && c.SalesMonth == month && c.ClientInfoId == clienId);
        }


        public IEnumerable<GheeSale> GetAll()
        {
            return _unitOfWork.GheeSale.GetAll().ToList();
        }


        public GheeSale SingleOrDefault(Expression<Func<GheeSale, bool>> predicate)
        {
            return _unitOfWork.GheeSale.SingleOrDefault(predicate);
        }

        public int Add(GheeSaleViewModel vm, string user)
        {
            var category = Mapper.Map<GheeSaleViewModel, GheeSale>(vm);
            _unitOfWork.GheeSale.Add(category);
            return _unitOfWork.Complete();
        }

        public int Update(int id, GheeSaleViewModel vm)
        {
            try
            {
                var saleInDb = _unitOfWork.GheeSale.Get(id);
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