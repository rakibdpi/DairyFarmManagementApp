using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.MilkSellsDtos;
using BusinessManagementSystemApp.Core.Dtos.OilSell;
using BusinessManagementSystemApp.Core.Dtos.Sales;
using BusinessManagementSystemApp.Core.Models.MilkSells;
using BusinessManagementSystemApp.Core.Models.OilSell;
using BusinessManagementSystemApp.Core.Models.Sales;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.OilSell
{
    public class OilSellManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public OilSellManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public OilSellDto Get(int id)
        {
            var entity = _unitOfWork.OilSell.Get(id);
            return (Mapper.Map<OilSells, OilSellDto>(entity));
        }


        public IEnumerable<OilSellDto> GetAll()
        {
            return _unitOfWork.OilSell.GetAllInclude().Select(Mapper.Map<OilSells, OilSellDto>);
        }

        public IEnumerable<OilSells> Find(Expression<Func<OilSells, bool>> predicate)
        {
            var info = _unitOfWork.OilSell.Find(predicate);
            return info;
        }

        public OilSells SingleOrDefault(Expression<Func<OilSells, bool>> predicate)
        {
            var info = _unitOfWork.OilSell.SingleOrDefault(predicate);
            return info;
        }

        public bool IsSaleExist(int clientId, string month)
        {
            var status = _unitOfWork.OilSell
                .Find(c => c.ClientInfoId == clientId && c.SalesMonth == month && !c.IsDelete).Any();
            return status;
        }


        public int Add(OilSaleListDto dto, string user)
        {
            foreach (var info in dto.OilSaleDtos)
            {
                var isExist = IsSaleExist(info.ClientInfoId, info.SalesMonth);
                if (isExist)
                    throw new ApplicationException("Already sale for this customer in this month");

                var details = new OilSells()
                {
                    Id = info.Id,
                    AreaId = info.AreaId,
                    ClientInfoId = info.ClientInfoId,
                    SalesMonth = info.SalesMonth,
                    DayNumber = info.DayNumber,
                    Year = "2021",
                    OneKg = info.OneKg,
                    TwoKg = info.TwoKg,
                    FiveKg = info.FiveKg,
                    CreateBy = user,
                    CreateDate = DateTime.Now
                };
                _unitOfWork.OilSell.Add(details);
            }

            return _unitOfWork.Complete();
        }


        public int Update(OilSaleListDto dto, string user)
        {
            try
            {
                foreach (var info in dto.OilSaleDtos)
                {
                    var details = _unitOfWork.OilSell.Find(c => c.Id == info.Id).FirstOrDefault();
                    if (details != null)
                    {
                        var createBy = details.CreateBy;
                        var createDate = details.CreateDate;
                        details.Id = info.Id;
                        details.AreaId = info.AreaId;
                        details.ClientInfoId = info.ClientInfoId;
                        details.SalesMonth = info.SalesMonth;
                        details.DayNumber = info.DayNumber;
                        details.Year = info.Year;
                        details.OneKg = info.OneKg;
                        details.TwoKg = info.TwoKg;
                        details.FiveKg = info.FiveKg;
                        details.CreateBy = createBy;
                        details.CreateDate = createDate;
                        details.UpdateBy = user;
                        details.UpdateDate = DateTime.Now;
                        _unitOfWork.Complete();
                    }
                }
                return 1;
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
                var info = _unitOfWork.OilSell.Get(id);
                if (info == null) return 0;

                info.IsDelete = true;
                info.DeleteBy = user;
                info.DeleteDate = DateTime.Now;
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Delete Fail. Error: " + e.Message);
            }
        }


        public int Add(OilSellDto vm, string user)
        {
            var category = Mapper.Map<OilSellDto, OilSells>(vm);
            _unitOfWork.OilSell.Add(category);
            return _unitOfWork.Complete();
        }

        public int UpdateAgain(long id, OilSellDto vm)
        {
            try
            {
                var saleInDb = _unitOfWork.OilSell.Get(Convert.ToInt32(id));
                if (saleInDb == null) return 0;
                Mapper.Map(vm, saleInDb);
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Update Fail. Error: " + e.Message);
            }
        }

        public OilSells OilSaleExist(string year, string month, int clienId)
        {
            return _unitOfWork.OilSell.SingleOrDefault(c =>
                c.Year == year && c.SalesMonth == month && c.ClientInfoId == clienId);
        }
    }
}