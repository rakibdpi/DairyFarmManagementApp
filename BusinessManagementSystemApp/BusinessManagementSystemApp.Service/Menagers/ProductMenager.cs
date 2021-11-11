using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.SetupModules;
using BusinessManagementSystemApp.Core.Models.OperationModules;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers
{
    public class ProductMenager : IManager<ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductMenager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }


        public ProductDto Get(int id)
        {
            var product = _unitOfWork.Product.Get(id);
            return (Mapper.Map<Product, ProductDto>(product));
        }

        public IEnumerable<ProductDto> GetAll()
        {
            return _unitOfWork.Product.GetAllInclude().Select(Mapper.Map<Product, ProductDto>).ToList();
        }

        //public int AddFile(MainEntryPdfDto pdf)
        //{
        //    var entityInDb = _unitOfWork.Product.Get(pdf.MasterId);
        //    if (entityInDb == null) return 0;

        //    //.Length.Split('.')[0];
        //    entityInDb.ImagePath = pdf.FilePath;
        //    return _unitOfWork.Complete();
        //}


        public int Add(ProductDto dto, string user)
        {
            var product = Mapper.Map<ProductDto,Product>(dto);
            product.CreateBy = user;
            product.CreateDate = DateTime.Now;
            _unitOfWork.Product.Add(product);

            return _unitOfWork.Complete();
        }

        public int AddRange(IEnumerable<ProductDto> dtos, string user)
        {
            throw new System.NotImplementedException();
        }

        public int Update(int id, ProductDto dto, string user)
        {
            var productInDb = _unitOfWork.Product.Get(id);
            if (productInDb == null)
            {
                return 0;
            }

            var createBy = productInDb.CreateBy;
            var createDate = productInDb.CreateDate;
            Mapper.Map(dto, productInDb);
            productInDb.CreateBy = createBy;
            productInDb.CreateDate = createDate;
            productInDb.UpdateBy = user;
            productInDb.UpdateDate = DateTime.Now;
            return _unitOfWork.Complete();
        }

        public int LogicalRemove(int id, string user)
        {
            var productInDb = _unitOfWork.Product.Get(id);
            if (productInDb == null)
            {
                return 0;
            }

            productInDb.IsDelete = true;
            productInDb.DeleteBy = user;
            productInDb.DeleteDate = DateTime.Now;
            return _unitOfWork.Complete();
        }

        public int Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public int RemoveRange(IEnumerable<ProductDto> dtos)
        {
            throw new System.NotImplementedException();
        }


    }
}