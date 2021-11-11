using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.MilkManagement
{
    public class CowSetupManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CowSetupManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public CowSetupDto Get(int id)
        {
            var entity = _unitOfWork.CowSetup.Get(id);
            return (Mapper.Map<CowSetup, CowSetupDto>(entity));
        }

        public IEnumerable<CowSetupDto> GetAll()
        {
            return _unitOfWork.CowSetup.Find(c => !c.IsDelete).Select(Mapper.Map<CowSetup, CowSetupDto>);
        }

        public IEnumerable<CowSetup> Find(Expression<Func<CowSetup, bool>> predicate)
        {
            var info = _unitOfWork.CowSetup.Find(predicate);
            return info;
        }

        public CowSetup SingleOrDefault(Expression<Func<CowSetup, bool>> predicate)
        {
            var info = _unitOfWork.CowSetup.SingleOrDefault(predicate);
            return info;    
        }

        public int Add(CowSetupDto dto, string user)
        {
            var cowSetup = Mapper.Map<CowSetupDto, CowSetup>(dto);
            cowSetup.CreateBy = user;
            cowSetup.CreateDate = DateTime.Now;
            _unitOfWork.CowSetup.Add(cowSetup); 
            return _unitOfWork.Complete();
        }

        
        public int Update(int id, CowSetupDto dto, string user)
        {
            try
            {
                var cowSetup = _unitOfWork.CowSetup.Get(id);
                if (cowSetup == null) return 0;
                var createBy = cowSetup.CreateBy;
                var createDate = cowSetup.CreateDate;
                Mapper.Map(dto, cowSetup);
                cowSetup.CreateBy = createBy;
                cowSetup.CreateDate = createDate;
                cowSetup.UpdateBy = user;
                cowSetup.UpdateDate = DateTime.Now;
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
                var cowSetup = _unitOfWork.CowSetup.Get(id);
                if (cowSetup == null) return 0;

                cowSetup.IsDelete = true;
                cowSetup.DeleteBy = user;   
                cowSetup.DeleteDate = DateTime.Now;
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Delete Fail. Error: " + e.Message);
            }
        }
    }
}