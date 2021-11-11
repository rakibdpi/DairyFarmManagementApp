using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.CustomerModules;
using BusinessManagementSystemApp.Core.Dtos.SupplierModules;
using BusinessManagementSystemApp.Core.Models.CustomerModules;
using BusinessManagementSystemApp.Core.Models.SupplierModules;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers
{
    public class SupplierMenager : IManager<SupplierDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierMenager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }
        public SupplierDto Get(int id)
        {
            var entity = _unitOfWork.Supplier.Get(id);
            return (Mapper.Map<Supplier, SupplierDto>(entity));
        }

        public IEnumerable<SupplierDto> GetAll()
        {
            return _unitOfWork.Supplier.Find(c => !c.IsDelete).Select(Mapper.Map<Supplier, SupplierDto>);

        }

        public int Add(SupplierDto dto, string user)
        {
            var supplier = Mapper.Map<SupplierDto, Supplier>(dto);
            supplier.CreateBy = user;
            supplier.CreateDate = DateTime.Now;
            _unitOfWork.Supplier.Add(supplier);

            return _unitOfWork.Complete();
        }

        public int AddRange(IEnumerable<SupplierDto> dtos, string user)
        {
            throw new System.NotImplementedException();
        }

        public int Update(int id, SupplierDto dto, string user)
        {
            try
            {
                var supplierInDb = _unitOfWork.Supplier.Get(id);
                if (supplierInDb == null) return 0;
                var createBy = supplierInDb.CreateBy;
                var createDate = supplierInDb.CreateDate;
                Mapper.Map(dto, supplierInDb);
                supplierInDb.CreateBy = createBy;
                supplierInDb.CreateDate = createDate;
                supplierInDb.UpdateBy = user;
                supplierInDb.UpdateDate = DateTime.Now;
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
                var supplierInDb = _unitOfWork.Supplier.Get(id);
                if (supplierInDb == null) return 0;

                supplierInDb.IsDelete = true;
                supplierInDb.DeleteBy = user;
                supplierInDb.DeleteDate = DateTime.Now;
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Delete Fail. Error: " + e.Message);
            }
        }

        public int Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public int RemoveRange(IEnumerable<SupplierDto> dtos)
        {
            throw new System.NotImplementedException();
        }
    }
}