using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.MilkPurchase;
using BusinessManagementSystemApp.Core.Models.MilkPurchases;
using BusinessManagementSystemApp.Core.ViewModels.MilkPurchase;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.MilkManagement.MilkPurchases
{
    public class MilkPurchaseManager : IManager<MilkPurchaseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MilkPurchaseManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }


        public MilkPurchaseDto Get(int id)
        {
            var entity = _unitOfWork.MilkPurchase.Get(id);
            return (Mapper.Map<MilkPurchase, MilkPurchaseDto>(entity));
        }

        public IEnumerable<MilkPurchaseDto> GetAll(int supplierId)
        {
            var infos = _unitOfWork.MilkPurchase.GetAllInclude(supplierId).Select(Mapper.Map<MilkPurchase, MilkPurchaseDto>).ToList();
            return infos;
        }

        public int Add(MilkPurchaseDto dto, string user)
        {
            var entity = Mapper.Map<MilkPurchaseDto, MilkPurchase>(dto);
            entity.CreateBy = user;
            entity.CreateDate = DateTime.Now;
            _unitOfWork.MilkPurchase.Add(entity);
            return _unitOfWork.Complete();
        }

        public int AddRange(IEnumerable<MilkPurchaseDto> dtos, string user)
        {
            throw new System.NotImplementedException();
        }

        public int Update(int id, MilkPurchaseDto dto, string user)
        {
            try
            {
                var purchaseInDb = _unitOfWork.MilkPurchase.Get(id);
                if (purchaseInDb == null) return 0;

                Mapper.Map(dto, purchaseInDb);
                purchaseInDb.UpdateBy = user;
                purchaseInDb.UpdateDate = DateTime.Now;
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
                var purchaseInDb = _unitOfWork.MilkPurchase.Get(id);
                if (purchaseInDb == null) return 0;

                purchaseInDb.IsDelete = true;
                purchaseInDb.DeleteBy = user;
                purchaseInDb.DeleteDate = DateTime.Now;
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

        public int RemoveRange(IEnumerable<MilkPurchaseDto> dtos)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<MilkPurchaseDto> GetAll()
        {
            throw new System.NotImplementedException();

        }

    }
}