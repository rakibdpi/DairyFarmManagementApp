using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Dtos.SetupModules;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.MilkManagement.SetupModules
{
    public class AreaManager : IManager<AreaDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AreaManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }
        public AreaDto Get(int id)
        {
            var entity = _unitOfWork.Area.Get(id);
            return (Mapper.Map<Area, AreaDto>(entity));

        }

        public IEnumerable<AreaDto> GetAll()
        {
            return _unitOfWork.Area.LoadForeignEntities().Select(Mapper.Map<Area, AreaDto>).OrderBy(c => c.Name).ToList();

        }

        public int Add(AreaDto dto, string user)
        {
            var area = Mapper.Map<AreaDto, Area>(dto);
            area.CreateBy = user;
            area.CreateDate = DateTime.Now;
            _unitOfWork.Area.Add(area);

            return _unitOfWork.Complete();
        }

        public int AddRange(IEnumerable<AreaDto> dtos, string user)
        {
            throw new System.NotImplementedException();
        }

        public int Update(int id, AreaDto dto, string user)
        {
            try
            {
                var areaInDb = _unitOfWork.Area.Get(id);
                if (areaInDb == null) return 0;
                var createBy = areaInDb.CreateBy;
                var createDate = areaInDb.CreateDate;
                Mapper.Map(dto, areaInDb);
                areaInDb.UpdateBy = user;
                areaInDb.UpdateDate = DateTime.Now;
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
                var areaInDb = _unitOfWork.Area.Get(id);
                if (areaInDb == null) return 0;

                areaInDb.IsDelete = true;
                areaInDb.IsActive = false;
                areaInDb.DeleteBy = user;
                areaInDb.DeleteDate = DateTime.Now;
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

        public int RemoveRange(IEnumerable<AreaDto> dtos)
        {
            throw new System.NotImplementedException();
        }
    }
}