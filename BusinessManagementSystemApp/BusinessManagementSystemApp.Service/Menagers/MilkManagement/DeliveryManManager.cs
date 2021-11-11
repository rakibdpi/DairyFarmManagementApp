using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.MilkManagement
{
    public class DeliveryManManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeliveryManManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }
        public DeliveryMan Get(int id)
        {
            var entity = _unitOfWork.DeliveryMan.Get(id);
            return entity;

        }

        public IEnumerable<DeliveryMan> GetAll()
        {
            return _unitOfWork.DeliveryMan.GetAll().ToList();
        }

        public int Add(DeliveryMan dto)
        {
            _unitOfWork.DeliveryMan.Add(dto);
            return _unitOfWork.Complete();
        }

        public int Update(int id, DeliveryMan dto)
        {
            var areaInDb = _unitOfWork.DeliveryMan.Get(id);
            if (areaInDb == null) return 0;
            Mapper.Map(dto, areaInDb);
            return _unitOfWork.Complete();
        }
        
    }
}