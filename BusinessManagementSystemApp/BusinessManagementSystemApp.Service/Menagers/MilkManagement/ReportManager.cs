using System.Collections.Generic;
using System.Linq;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.MilkProductionDtos;
using BusinessManagementSystemApp.Core.Dtos.MilkPurchase;
using BusinessManagementSystemApp.Core.ViewModels.MilkMamagement;
using BusinessManagementSystemApp.Core.ViewModels.ReportViewModels;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.MilkManagement
{
    public class ReportManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public List<SaleReportByDeliveryMenViewModel> SaleReportDeliveryManWise(string month, int year, int? deliveryMan, int? areaId)
        {
            decimal halfKgPrice = 46;
            decimal sevenHalfPrice = 68;
            decimal oneKgPrice = 90;
            var sales = _unitOfWork.PacketSale.Find(c => !c.IsDelete && c.SalesMonth == month && c.CreateDate.Year == year).ToList();
            var collections = _unitOfWork.Payment.Find(c => !c.IsDelete && c.Month == month && c.Year == year.ToString()).ToList();

            var resultInfos = (from h in sales
                group h by new { h.AreaId } into hh
                select new SaleReportByDeliveryMenViewModel
                {
                    DeliveryManId = _unitOfWork.Area.Get(hh.Key.AreaId).DeliveryManId,
                    DeliveryMan= _unitOfWork.Area.LoadForeignEntities().LastOrDefault()?.DeliveryMan.Name,
                    AreaId = hh.Key.AreaId,
                    AreaName= _unitOfWork.Area.Get(hh.Key.AreaId).Name,
                    HalfKg = hh.Sum(s => s.HalfKg),
                    SevenHalf = hh.Sum(s => s.SevenAndHalfGm),
                    OneKg = hh.Sum(s => s.OneKg),
                    TotalSale= (hh.Sum(s => s.HalfKg) * halfKgPrice)+ (hh.Sum(s => s.SevenAndHalfGm) * sevenHalfPrice)+ (hh.Sum(s => s.OneKg) * oneKgPrice),
                    TotalCollection= collections.Where(c => c.AreaId== hh.Key.AreaId).Sum(c => c.BillAmount)
                }).Distinct().OrderByDescending(i => i.AreaId).ToList();

            if (deliveryMan > 0)
            {
                resultInfos = resultInfos.Where(c => c.DeliveryManId == deliveryMan).OrderBy(c => c.AreaName).ToList();
            }

            if (areaId>0)
            {
                resultInfos = resultInfos.Where(c => c.AreaId == areaId).ToList();
            }
            
            return resultInfos;
        }

        public List<ProductionReportDto> GetProductionReport(ProductionReportViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Month) && model.Date > 0 && model.CowId > 0)
            {
                var infos = _unitOfWork.Production.
                    Find(c => !c.IsDelete &&
                     c.Year.Trim() == model.Year.Trim() &&
                     c.ProductionMonth.Trim() == model.Month.Trim() &&
                     c.CowSetupId == model.CowId &&
                     c.DayNumber == model.Date).Select(x => new ProductionReportDto
                     {
                         Date = x.DayNumber,
                         CowNo = _unitOfWork.CowSetup.Get(x.CowSetupId).Number,
                         Morning = x.MorningQuantity,
                         Afternoon = x.AfterNoonQuantity,
                         Night = x.NightQuantity,
                         Other = x.OtherTime,
                         TotalQuantity = (x.MorningQuantity + x.AfterNoonQuantity + x.NightQuantity + x.OtherTime)
                     }).Distinct().ToList();
                return infos;
            }
            else if (!string.IsNullOrEmpty(model.Month) && model.Date > 0 && model.CowId < 1)
            {
                var infos = _unitOfWork.Production.
                    Find(c => !c.IsDelete &&
                              c.Year.Trim() == model.Year.Trim() &&
                              c.ProductionMonth.Trim() == model.Month.Trim() &&
                              c.DayNumber == model.Date).Select(x => new ProductionReportDto
                              {
                                  Date = x.DayNumber,
                                  CowNo = _unitOfWork.CowSetup.Get(x.CowSetupId).Number,
                                  Morning = x.MorningQuantity,
                                  Afternoon = x.AfterNoonQuantity,
                                  Night = x.NightQuantity,
                                  Other = x.OtherTime,
                                  TotalQuantity = (x.MorningQuantity + x.AfterNoonQuantity + x.NightQuantity + x.OtherTime)
                              }).Distinct().ToList();
                return infos;
            }
            else if (!string.IsNullOrEmpty(model.Month) && model.Date < 1 && model.CowId > 0)
            {
                var infos = _unitOfWork.Production.
                    Find(c => !c.IsDelete &&
                              c.Year.Trim() == model.Year.Trim() &&
                              c.ProductionMonth.Trim() == model.Month.Trim() &&
                    c.CowSetupId == model.CowId).Select(x => new ProductionReportDto
                    {
                        Date = x.DayNumber,
                        CowNo = _unitOfWork.CowSetup.Get(x.CowSetupId).Number,
                        Morning = x.MorningQuantity,
                        Afternoon = x.AfterNoonQuantity,
                        Night = x.NightQuantity,
                        Other = x.OtherTime,
                        TotalQuantity = (x.MorningQuantity + x.AfterNoonQuantity + x.NightQuantity + x.OtherTime)
                    }).Distinct().ToList();
                return infos;
            }
            else if (!string.IsNullOrEmpty(model.Month) && model.Date < 1 && model.CowId < 1)
            {
                var infos = _unitOfWork.Production.
                    Find(c => !c.IsDelete &&
                              c.Year.Trim() == model.Year.Trim() &&
                              c.ProductionMonth.Trim() == model.Month.Trim()).ToList().Select(x => new ProductionReportDto
                    {
                        Date = x.DayNumber,
                        CowNo = _unitOfWork.CowSetup.Get(x.CowSetupId).Number,
                        Morning =  x.MorningQuantity,
                        Afternoon = x.AfterNoonQuantity,
                        Night = x.NightQuantity,
                        Other = x.OtherTime,
                        TotalQuantity = (x.MorningQuantity + x.AfterNoonQuantity + x.NightQuantity + x.OtherTime)
                    }).Distinct().ToList();
                return infos;
            }

            return null;
        }

        public List<ProductionReportDto> GetProductionReportWithOutDayNumber(ProductionReportViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Month) && model.Date < 1 && model.CowId < 1)
            {
                var uniqueCowId= _unitOfWork.Production.
                    Find(c => !c.IsDelete &&
                              c.Year.Trim() == model.Year.Trim() &&
                              c.ProductionMonth.Trim() == model.Month.Trim()).Select(x => x.CowSetupId).Distinct().ToList();
                var infos = _unitOfWork.Production.
                    Find(c => !c.IsDelete &&
                              c.Year.Trim() == model.Year.Trim() &&
                              c.ProductionMonth.Trim() == model.Month.Trim()).ToList();
                    var newList= uniqueCowId.Select(x => new ProductionReportDto
                              {
                                  Date = 1,
                                  CowNo = _unitOfWork.CowSetup.Get(x).Number,
                                  Morning = infos.Where(c => c.CowSetupId==x).Sum(c => c.MorningQuantity),
                                  Afternoon = infos.Where(c => c.CowSetupId == x).Sum(c => c.AfterNoonQuantity),
                                  Night = infos.Where(c => c.CowSetupId == x).Sum(c => c.NightQuantity),
                                  Other = infos.Where(c => c.CowSetupId == x).Sum(c => c.OtherTime),
                                  TotalQuantity = infos.Where(c => c.CowSetupId == x).Sum(c => c.MorningQuantity) + 
                                                  infos.Where(c => c.CowSetupId == x).Sum(c => c.AfterNoonQuantity) +
                                                  infos.Where(c => c.CowSetupId == x).Sum(c => c.NightQuantity) +
                                                  infos.Where(c => c.CowSetupId == x).Sum(c => c.OtherTime)
                    }).Distinct().ToList();
                return newList;
            }

            return null;
        }

        public List<PurchaseReportDto> GetPurchaseReport(PurchaseReportViewModel model)
        {
            if (model.SupplierId > 0 && model.Date > 0)
            {
                var infos = _unitOfWork
                    .MilkPurchase
                    .Find(c =>
                        !c.IsDelete &&
                        c.PurchaseDate.Year.ToString() == model.Year &&
                        c.PurchaseDate.Month.ToString() == model.Month &&
                        c.MilkSuppliersId == model.SupplierId &&
                        c.PurchaseDate.Day == model.Date)
                    .Select(x => new PurchaseReportDto
                    {
                        Date = x.PurchaseDate.Day,
                        Supplier = _unitOfWork.MilkSupplier.Get(x.MilkSuppliersId).Name,
                        Quantity = x.MilkQuantity
                    }).Distinct().ToList();
                return infos.OrderBy(c => c.Date).ToList();
            }
            else if (model.SupplierId > 0 && model.Date < 1)
            {
                var infos = _unitOfWork
                    .MilkPurchase
                    .Find(c =>
                        !c.IsDelete &&
                        c.PurchaseDate.Year.ToString() == model.Year &&
                        c.PurchaseDate.Month.ToString() == model.Month &&
                        c.MilkSuppliersId == model.SupplierId )
                    .Select(x => new PurchaseReportDto
                    {
                        Date = x.PurchaseDate.Day,
                        Supplier = _unitOfWork.MilkSupplier.Get(x.MilkSuppliersId).Name,
                        Quantity = x.MilkQuantity
                    }).Distinct().ToList();
                return infos.OrderBy(c => c.Date).ToList();
            }
            else if (model.SupplierId < 1 && model.Date > 0)
            {
                var infos = _unitOfWork
                    .MilkPurchase
                    .Find(c =>
                        !c.IsDelete &&
                        c.PurchaseDate.Year.ToString() == model.Year &&
                        c.PurchaseDate.Month.ToString() == model.Month &&
                        c.PurchaseDate.Day == model.Date)
                    .Select(x => new PurchaseReportDto
                    {
                        Date = x.PurchaseDate.Day,
                        Supplier = _unitOfWork.MilkSupplier.Get(x.MilkSuppliersId).Name,
                        Quantity = x.MilkQuantity
                    }).Distinct().ToList();
                return infos.OrderBy(c => c.Date).ToList();
            }
            return null;
        }

        public List<PurchaseReportDto> GetPurchaseReportForMonth(PurchaseReportViewModel model)
        {
            if (model.SupplierId >= 1 || model.Date >= 1) return null;
            var uniqueSupplierId = _unitOfWork.MilkPurchase.Find(c =>
                    !c.IsDelete &&
                    c.PurchaseDate.Year.ToString() == model.Year &&
                    c.PurchaseDate.Month.ToString() == model.Month)
                .Select(c => new{ c.MilkSuppliersId, c.PurchaseDate.Day}).Distinct().ToList();
            var infos = _unitOfWork
                .MilkPurchase
                .Find(c =>
                    !c.IsDelete &&
                    c.PurchaseDate.Year.ToString() == model.Year &&
                    c.PurchaseDate.Month.ToString() == model.Month).ToList();
            var newList= uniqueSupplierId.Select(x => new PurchaseReportDto
            {
                Date = x.Day,
                Supplier = _unitOfWork.MilkSupplier.Get(x.MilkSuppliersId).Name,
                Quantity = infos.Where(c => c.MilkSuppliersId== x.MilkSuppliersId).Sum(c => c.MilkQuantity)
            }).Distinct().ToList();
            return newList.OrderBy(c => c.Date).ThenBy(c => c.Supplier).ToList();
        }
    }
    
}