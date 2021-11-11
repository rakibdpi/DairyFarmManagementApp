using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.AsthaShopDto;
using BusinessManagementSystemApp.Core.Dtos.MilkPurchase;
using BusinessManagementSystemApp.Core.Models.AsthaShop;
using BusinessManagementSystemApp.Core.Models.MilkPurchases;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.AsthaOnline
{
    public class DataTransectionManager : IManager<TransectionDataDto>
    {
        public string name { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public DataTransectionManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public TransectionDataDto Get(int id)
        {
            var entity = _unitOfWork.Datatransection.Get(id);
            return (Mapper.Map<TransectionData, TransectionDataDto>(entity));

        }

        public IEnumerable<TransectionDataDto> GetAll()
        {
            var infos = _unitOfWork.Datatransection.GetAllInclude().Select(Mapper.Map<TransectionData, TransectionDataDto>).ToList();
            return infos;
        }


        public string chart()
        {
            var infos = _unitOfWork.Datatransection.GetAllInclude().ToList();

            foreach (var info in infos)
            {
                name = info.DataType.Name;
            }

            return name;
        }

        public int Add(TransectionDataDto dto, string user)
        {
            var entity = Mapper.Map<TransectionDataDto, TransectionData>(dto);
            entity.DateTime = DateTime.Now;
            _unitOfWork.Datatransection.Add(entity);
            return _unitOfWork.Complete();
        }


        public int AddRange(IEnumerable<TransectionDataDto> dtos, string user)
        {
            throw new System.NotImplementedException();
        }

        public int Update(int id, TransectionDataDto dto, string user)
        {
            try
            {
                var purchaseInDb = _unitOfWork.Datatransection.Get(id);
                if (purchaseInDb == null) return 0;

                Mapper.Map(dto, purchaseInDb);
                purchaseInDb.DateTime = DateTime.Now;
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Update Fail. Error: " + e.Message);
            }
        }

        public int LogicalRemove(int id, string user)
        {
            throw new System.NotImplementedException();
        }

        public int Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public int RemoveRange(IEnumerable<TransectionDataDto> dtos)
        {
            throw new System.NotImplementedException();
        }


        public IEnumerable<TransectionDataDto> GetAllForReport(DateTime? fromDate, DateTime? toDate, int? dataTypeId)
        {
            var infos = _unitOfWork.Datatransection.GetAllInclude();


            if (fromDate != null && toDate != null)
            {
                infos = infos.Where(c => c.DateTime >= fromDate && c.DateTime <= toDate).ToList();
            }
            if (dataTypeId != null)
            {
                infos = infos.Where(c => c.DataTypeId == dataTypeId).ToList();
            }


            return infos.Select(Mapper.Map<TransectionData, TransectionDataDto>).ToList();
        }

    }
}