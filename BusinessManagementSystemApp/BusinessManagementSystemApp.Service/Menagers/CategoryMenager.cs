using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.AutoMapperConfigurations;
using BusinessManagementSystemApp.Core.Dtos.SetupModules;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers
{
    public class CategoryMenager : IManager<CategoryDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryMenager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public CategoryDto Get(int id)
        {
            var entity = _unitOfWork.Category.Get(id);
            return (Mapper.Map<Category, CategoryDto>(entity));
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _unitOfWork.Category.Find(c => !c.IsDelete).Select(Mapper.Map<Category, CategoryDto>);
        }

        public int Add(CategoryDto dto, string user)
        {
            var category = Mapper.Map<CategoryDto, Category>(dto);
            category.CreateBy = user;
            category.CreateDate = DateTime.Now;
            _unitOfWork.Category.Add(category);

            return _unitOfWork.Complete();
        }

        public int AddRange(IEnumerable<CategoryDto> dtos, string user)
        {
            throw new System.NotImplementedException();
        }

        public int Update(int id, CategoryDto dto, string user)
        {
            try
            {
                var categoryInDb = _unitOfWork.Category.Get(id);
                if (categoryInDb == null) return 0;
                var createBy = categoryInDb.CreateBy;
                var createDate = categoryInDb.CreateDate;
                Mapper.Map(dto, categoryInDb);
                categoryInDb.CreateBy = createBy;
                categoryInDb.CreateDate = createDate;
                categoryInDb.UpdateBy = user;
                categoryInDb.UpdateDate = DateTime.Now;
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
                var categoryInDb = _unitOfWork.Category.Get(id);
                if (categoryInDb == null) return 0;

                categoryInDb.IsDelete = true;
                categoryInDb.IsActive = false;
                categoryInDb.DeleteBy = user;
                categoryInDb.DeleteDate = DateTime.Now;
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

        public int RemoveRange(IEnumerable<CategoryDto> dtos)
        {
            throw new System.NotImplementedException();
        }
    }
}