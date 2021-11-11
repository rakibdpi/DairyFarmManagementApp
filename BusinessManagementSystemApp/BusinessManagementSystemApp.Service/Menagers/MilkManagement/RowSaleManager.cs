using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.MilkSellsDtos;
using BusinessManagementSystemApp.Core.Models.MilkSells;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.MilkManagement
{
    public class RowSaleManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public RowSaleManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public RowSaleDto Get(int id)
        {
            var entity = _unitOfWork.RowSale.Get(id);
            return (Mapper.Map<RowSale, RowSaleDto>(entity));
        }

        public IEnumerable<RowSaleDto> GetAll()
        {
            return _unitOfWork.RowSale.Find(c => !c.IsDelete).Select(Mapper.Map<RowSale, RowSaleDto>);
        }

        public IEnumerable<RowSale> Find(Expression<Func<RowSale, bool>> predicate)
        {
            var info = _unitOfWork.RowSale.Find(predicate);
            return info;
        }

        public RowSale SingleOrDefault(Expression<Func<RowSale, bool>> predicate)
        {
            var info = _unitOfWork.RowSale.SingleOrDefault(predicate);
            return info;
        }

        public int Add(RowSaleDto dto, string user)
        {
            var info = Mapper.Map<RowSaleDto, RowSale>(dto);
            info.CreateBy = user;
            info.CreateDate = DateTime.Now;
            _unitOfWork.RowSale.Add(info);
            return _unitOfWork.Complete();
        }


        public int Update(int id, RowSaleDto dto, string user)
        {
            try
            {
                var info = _unitOfWork.RowSale.Get(id);
                if (info == null) return 0;
                var createBy = info.CreateBy;
                var createDate = info.CreateDate;
                Mapper.Map(dto, info);  
                info.CreateBy = createBy;
                info.CreateDate = createDate;
                info.UpdateBy = user;
                info.UpdateDate = DateTime.Now;
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Update Fail. Error: " + e.Message);
            }
        }

        public int LogicalRemove(int id, string user)
        {
            try
            {
                var info = _unitOfWork.RowSale.Get(id);
                if (info == null) return 0;

                info.IsDelete = true;
                info.DeleteBy = user;
                info.DeleteDate = DateTime.Now;
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Delete Fail. Error: " + e.Message);
            }
        }
    }
}