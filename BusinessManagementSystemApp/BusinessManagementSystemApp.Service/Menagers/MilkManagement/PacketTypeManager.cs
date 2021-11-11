using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Models.MilkProduction;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.MilkManagement
{
    public class PacketTypeManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public PacketTypeManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public PacketType Get(int id)
        {
            var entity = _unitOfWork.PacketType.Get(id);
            return entity;
        }

        public IEnumerable<PacketType> GetAll()
        {
            return _unitOfWork.PacketType.GetAll().ToList();
        }

        public IEnumerable<PacketType> Find(Expression<Func<PacketType, bool>> predicate)
        {
            var info = _unitOfWork.PacketType.Find(predicate);
            return info;
        }

        public PacketType SingleOrDefault(Expression<Func<PacketType, bool>> predicate)
        {
            var info = _unitOfWork.PacketType.SingleOrDefault(predicate);
            return info;        
        }
    }
}