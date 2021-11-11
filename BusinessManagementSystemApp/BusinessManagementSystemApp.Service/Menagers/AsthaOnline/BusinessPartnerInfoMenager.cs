using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.AsthaShopDto;
using BusinessManagementSystemApp.Core.Models.AsthaShop;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Service.Menagers.AsthaOnline
{
    public class BusinessPartnerInfoMenager : IManager<BusinessPartnerInfoDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public BusinessPartnerInfoMenager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }
        public int Add(BusinessPartnerInfoDto dto, string user)
        {
            var partnerInfo = Mapper.Map<BusinessPartnerInfoDto, BusinessPartnerInfo>(dto);
            partnerInfo.CreateBy = user;
            partnerInfo.CreateDate = DateTime.Now;
            _unitOfWork.BusinessPartnerInfo.Add(partnerInfo);

            return _unitOfWork.Complete();
        }

        public int AddRange(IEnumerable<BusinessPartnerInfoDto> dtos, string user)
        {
            throw new NotImplementedException();
        }

        public BusinessPartnerInfoDto Get(int id)
        {
            var entity = _unitOfWork.BusinessPartnerInfo.Get(id);
            return (Mapper.Map<BusinessPartnerInfo, BusinessPartnerInfoDto>(entity));
        }

        public IEnumerable<BusinessPartnerInfoDto> GetAll()
        {
            return _unitOfWork.BusinessPartnerInfo.Find(c => !c.IsDelete).Select(Mapper.Map<BusinessPartnerInfo, BusinessPartnerInfoDto>);

        }

        public int LogicalRemove(int id, string user)
        {
            try
            {
                var partnerInfoInDb = _unitOfWork.BusinessPartnerInfo.Get(id);
                if (partnerInfoInDb == null) return 0;

                partnerInfoInDb.IsDelete = true;
                partnerInfoInDb.IsActive = false;
                partnerInfoInDb.DeleteBy = user;
                partnerInfoInDb.DeleteDate = DateTime.Now;
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Delete Fail. Error: " + e.Message);
            }
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int RemoveRange(IEnumerable<BusinessPartnerInfoDto> dtos)
        {
            throw new NotImplementedException();
        }

        public int Update(int id, BusinessPartnerInfoDto dto, string user)
        {
            try
            {
                var partnerInfoInDb = _unitOfWork.BusinessPartnerInfo.Get(id);
                if (partnerInfoInDb == null) return 0;
                var createBy = partnerInfoInDb.CreateBy;
                var createDate = partnerInfoInDb.CreateDate;
                Mapper.Map(dto, partnerInfoInDb);
                partnerInfoInDb.CreateBy = createBy;
                partnerInfoInDb.CreateDate = createDate;
                partnerInfoInDb.UpdateBy = user;
                partnerInfoInDb.UpdateDate = DateTime.Now;
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Update Fail. Error: " + e.Message);
            }
        }
    }
}
