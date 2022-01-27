using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.MilkSellsDtos;
using BusinessManagementSystemApp.Core.Dtos.Sales;
using BusinessManagementSystemApp.Core.Models.MilkSells;
using BusinessManagementSystemApp.Core.Models.Sales;
using BusinessManagementSystemApp.Core.ReportModels;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.MilkManagement
{
    public class PacketSaleManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public PacketSaleManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public PacketSaleDto Get(int id)
        {
            var entity = _unitOfWork.PacketSale.Get(id);
            return (Mapper.Map<PacketSale, PacketSaleDto>(entity));
        }
        

        public IEnumerable<PacketSaleDto> GetAll()
        {
            return _unitOfWork.PacketSale.GetAllInclude().Select(Mapper.Map<PacketSale, PacketSaleDto>);
        }

        public IEnumerable<PacketSale> Find(Expression<Func<PacketSale, bool>> predicate)
        {
            var info = _unitOfWork.PacketSale.Find(predicate);
            return info;
        }

        public PacketSale SingleOrDefault(Expression<Func<PacketSale, bool>> predicate)
        {
            var info = _unitOfWork.PacketSale.SingleOrDefault(predicate);
            return info;
        }

        public bool IsSaleExist(int clientId, string month)
        {
            var status = _unitOfWork.PacketSale
                .Find(c => c.ClientInfoId == clientId && c.SalesMonth == month && !c.IsDelete).Any();
            return status;
        }
        public IEnumerable<SalesReport> GetMilkReport(int year, string month)
        {
            var sales = new List<SalesReport>();

            var list = _unitOfWork.Area.GetAll().Where(c => c.IsActive && !c.IsDelete).Select(x => new
            {
                AreaName= x.Name,
                TotalHalf= _unitOfWork.PacketSale.Find(c => c.SalesMonth== month && c.CreateDate.Year==year && c.AreaId==x.Id).Sum(c => c.HalfKg),
                TotalSevenHalf = _unitOfWork.PacketSale.Find(c => c.SalesMonth == month && c.CreateDate.Year == year && c.AreaId == x.Id).Sum(c => c.SevenAndHalfGm),
                TotalOne = _unitOfWork.PacketSale.Find(c => c.SalesMonth == month && c.CreateDate.Year == year && c.AreaId == x.Id).Sum(c => c.OneKg)

            }).Distinct().ToList();

            foreach(var l in list)
            {
                var s = new SalesReport()
                {
                    AreaName = l.AreaName,
                    TotalHalf = l.TotalHalf,
                    TotalSevenHalf = l.TotalSevenHalf,
                    TotalOne = l.TotalOne,
                    TotalAmount = (l.TotalHalf * 46) + (l.TotalSevenHalf * 68) + (l.TotalOne * 90)
                };
                sales.Add(s);
            }

            return sales;
        }


        //public MilkDetailsSummary  GetMilkSalesAndDetailsReport(int year, string month)
        //{
        //    var totalHalf = _unitOfWork.PacketSale.Find(c => c.SalesMonth == month && c.CreateDate.Year == year).Sum(c => c.HalfKg);
        //    var totalSevenHalf = _unitOfWork.PacketSale.Find(c => c.SalesMonth == month && c.CreateDate.Year == year).Sum(c => c.SevenAndHalfGm);
        //    var totalOne = _unitOfWork.PacketSale.Find(c => c.SalesMonth == month && c.CreateDate.Year == year).Sum(c => c.OneKg);





        //}







        public int Add(PacketSaleListDto dto, string user)
        {
            foreach (var info in dto.PacketSaleDtos)
            {
                var isExist = IsSaleExist(info.ClientInfoId, info.SalesMonth);
                if (isExist)
                    throw new ApplicationException("Already sale for this customer in this month");

                var details = new PacketSale()
                {
                    Id = info.Id,
                    AreaId = info.AreaId,
                    ClientInfoId = info.ClientInfoId,
                    SalesMonth = info.SalesMonth,
                    DayNumber = info.DayNumber,
                    HalfKg = info.HalfKg,
                    SevenAndHalfGm = info.SevenAndHalfGm,
                    OneKg = info.OneKg,
                    CreateBy = user,
                    CreateDate = DateTime.Now
                };
                _unitOfWork.PacketSale.Add(details);
            }
           
            return _unitOfWork.Complete();
        }


        public int Update(PacketSaleListDto dto, string user)
        {
            try
            {
                foreach (var info in dto.PacketSaleDtos)
                {
                    var details = _unitOfWork.PacketSale.Find(c => c.Id== info.Id).FirstOrDefault();
                    if (details!= null)
                    {                        
                        var createBy = details.CreateBy;
                        var createDate = details.CreateDate;
                        details.Id = info.Id;
                        details.AreaId = info.AreaId;
                        details.ClientInfoId = info.ClientInfoId;
                        details.SalesMonth = info.SalesMonth;
                        details.DayNumber = info.DayNumber;
                        details.HalfKg = info.HalfKg;
                        details.SevenAndHalfGm = info.SevenAndHalfGm;
                        details.OneKg = info.OneKg;
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
                var info = _unitOfWork.PacketSale.Get(id);
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