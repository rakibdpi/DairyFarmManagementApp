using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Dtos.MilkPurchase;
using BusinessManagementSystemApp.Core.Models.MilkPurchases;
using BusinessManagementSystemApp.Core.ViewModels.ReportViewModels;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement.MilkPurchases;

namespace BMSA.App.Controllers
{
    public class MilkPurchasesController : Controller
    {
        private readonly ReportManager _reportManager;
        private readonly MilkSupplierManager _supplierManager;

        public MilkPurchasesController()
        {
            _reportManager= new ReportManager();
            _supplierManager= new MilkSupplierManager();
        }
        // GET: MilkPurchases
        public ActionResult MilkPurchaseForm(int? id)
        {
            ViewBag.MilkSuppliersId = new SelectList(new List<MilkSuppliers>(), "Id", "Name");

            return View();
        }


        public ActionResult MilkPurchaseList()
        {
            ViewBag.MilkSuppliersId = new SelectList(new List<MilkSuppliers>(), "Id", "Name");

            return View();
        }

        public ActionResult PurchaseReport()
        {
            ViewBag.SupplierId = new SelectList(new List<MilkSuppliers>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult PurchaseReport(PurchaseReportViewModel model)
        {
            ViewBag.SupplierId = new SelectList(new List<MilkSuppliers>(), "Id", "Name");
            if (string.IsNullOrEmpty(model.Year) || string.IsNullOrEmpty(model.Month))
            {
                ViewBag.MessageLabel = "Please Select Year & Month";
                return View();
            }

            if (model.Date > 0 || model.SupplierId > 0)
            {
                return  RedirectToAction("ProductionReportDetail", model);
            }

            return RedirectToAction("ProductionReportDetailForMonth", model);

        }
        public ActionResult ProductionReportDetail(PurchaseReportViewModel model)
        {
            var info = new PurchaseReportDataSet();
            var details = _reportManager.GetPurchaseReport(model);
            info.Models = details;
            info.CompanyName = "Thakugaon Dairy Farm";
            info.CompanyAddress = "HOUSE#89,BLOCK-B,WARD#53,PORAN KALIYA,NOYANAGOR,TURAG,DHAKA-1230";
            info.MobileNo = "01760123281";
            info.MonthParameter = model.Month;
            info.YearParameter = model.Year;
            info.DayParameter = model.Date.ToString();
            info.SupplierParameter = model.SupplierId > 0 ? _supplierManager.GetAll().FirstOrDefault(c=> c.Id== model.SupplierId)?.Name : "";
            return View(info);
        }

        public ActionResult ProductionReportDetailForMonth(PurchaseReportViewModel model)
        {
            var info = new PurchaseReportDataSet();
            var details = _reportManager.GetPurchaseReportForMonth(model);
            info.Models = details;
            info.CompanyName = "Thakugaon Dairy Farm";
            info.CompanyAddress = "HOUSE#89,BLOCK-B,WARD#53,PORAN KALIYA,NOYANAGOR,TURAG,DHAKA-1230";
            info.MobileNo = "01760123281";
            info.MonthParameter = model.Month;
            info.YearParameter = model.Year;
            info.DayParameter = model.Date.ToString();
            info.SupplierParameter = model.SupplierId > 0 ? _supplierManager.GetAll().FirstOrDefault(c => c.Id == model.SupplierId)?.Name : "";
            return View(info);
        }

    }
}