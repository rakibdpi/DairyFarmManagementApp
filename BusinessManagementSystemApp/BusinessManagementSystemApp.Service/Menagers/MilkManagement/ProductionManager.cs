using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.MilkProductionDtos;
using BusinessManagementSystemApp.Core.Models.MilkProduction;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.MilkManagement
{
    public class ProductionManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductionManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public ProductionDto Get(int id)
        {
            var entity = _unitOfWork.Production.Get(id);
            return (Mapper.Map<Production, ProductionDto>(entity));
        }


        public IEnumerable<ProductionDto> GetAll()
        {
            return _unitOfWork.Production.GetAllInclude().Select(Mapper.Map<Production, ProductionDto>);
        }

        public IEnumerable<Production> Find(Expression<Func<Production, bool>> predicate)
        {
            var info = _unitOfWork.Production.Find(predicate);
            return info;
        }

        public Production SingleOrDefault(Expression<Func<Production, bool>> predicate)
        {
            var info = _unitOfWork.Production.SingleOrDefault(predicate);
            return info;
        }

        public bool IsSaleExist(int cowId, string month)
        {
            var status = _unitOfWork.Production
                .Find(c => c.CowSetupId == cowId && c.ProductionMonth == month && !c.IsDelete).Any();
            return status;
        }
        //public IEnumerable<SalesReport> GetMilkReport(int year, string month)
        //{
        //    var sales = new List<SalesReport>();

        //    var list = _unitOfWork.Area.GetAll().Where(c => c.IsActive && !c.IsDelete).Select(x => new
        //    {
        //        AreaName = x.Name,
        //        TotalHalf = _unitOfWork.PacketSale.Find(c => c.SalesMonth == month && c.CreateDate.Year == year && c.AreaId == x.Id).Sum(c => c.HalfKg),
        //        TotalSevenHalf = _unitOfWork.PacketSale.Find(c => c.SalesMonth == month && c.CreateDate.Year == year && c.AreaId == x.Id).Sum(c => c.SevenAndHalfGm),
        //        TotalOne = _unitOfWork.PacketSale.Find(c => c.SalesMonth == month && c.CreateDate.Year == year && c.AreaId == x.Id).Sum(c => c.OneKg)

        //    }).Distinct().ToList();

        //    foreach (var l in list)
        //    {
        //        var s = new SalesReport()
        //        {
        //            AreaName = l.AreaName,
        //            TotalHalf = l.TotalHalf,
        //            TotalSevenHalf = l.TotalSevenHalf,
        //            TotalOne = l.TotalOne,
        //            TotalAmount = (l.TotalHalf * 46) + (l.TotalSevenHalf * 68) + (l.TotalOne * 90)
        //        };
        //        sales.Add(s);
        //    }

        //    return sales;
        //}

        public int Add(ProductionListDto dto, string user)
        {
            foreach (var info in dto.DtoList)
            {
                var isExist = IsSaleExist(info.CowSetupId, info.ProductionMonth);
                if (isExist)
                    throw new ApplicationException("Already entry for this Cow in this month");

                var details = new Production()
                {
                    Id = info.Id,
                    CowSetupId = info.CowSetupId,
                    ProductionMonth = info.ProductionMonth,
                    Year = info.Year,
                    DayNumber = info.DayNumber,
                    MorningQuantity = info.MorningQuantity,
                    AfterNoonQuantity = info.AfterNoonQuantity,
                    NightQuantity = info.NightQuantity,
                    OtherTime = info.OtherTime,
                    CreateBy = user,
                    CreateDate = DateTime.Now
                };
                _unitOfWork.Production.Add(details);
            }

            return _unitOfWork.Complete();
        }


        public int Update(ProductionListDto dto, string user)
        {
            try
            {
                foreach (var info in dto.DtoList)
                {
                    var details = _unitOfWork.Production.Find(c => c.Id == info.Id).FirstOrDefault();
                    if (details != null)
                    {
                        var createBy = details.CreateBy;
                        var createDate = details.CreateDate;
                        details.Id = info.Id;
                        details.CowSetupId = info.CowSetupId;
                        details.ProductionMonth = info.ProductionMonth;
                        details.Year = info.Year;
                        details.DayNumber = info.DayNumber;
                        details.MorningQuantity = info.MorningQuantity;
                        details.AfterNoonQuantity = info.AfterNoonQuantity;
                        details.NightQuantity = info.NightQuantity;
                        details.OtherTime = info.OtherTime;
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
                var info = _unitOfWork.Production.Get(id);
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
    }
}