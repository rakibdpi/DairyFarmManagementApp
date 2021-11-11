using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.ViewModels.MilkMamagement;
using BusinessManagementSystemApp.Core.ViewModels.ReportViewModels;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;

namespace BMSA.App.Controllers
{
    public class ProductionsController : Controller
    {
        private readonly ReportManager _reportManager;
        private readonly CowSetupManager _cowSetupManager;

        public ProductionsController()
        {
            _reportManager= new ReportManager();
            _cowSetupManager= new CowSetupManager();
        }
       // GET: Productions
        public ActionResult ProductionForm(int? id)
        {
            ViewBag.CowSetupId = new SelectList(new List<CowSetup>(), "Id", "Number");

            return View();
        }

        public ActionResult ProductionEdit()
        {
            ViewBag.CowSetupId = new SelectList(new List<CowSetup>(), "Id", "Number");

            return View();
        }
        public ActionResult ProductionReport()
        {
            ViewBag.CowId = new SelectList(new List<CowSetup>(), "Id", "Number");
            return View();
        }

        [HttpPost]
        public ActionResult ProductionReport(ProductionReportViewModel model)
        {
            ViewBag.CowId = new SelectList(new List<CowSetup>(), "Id", "Number");
            if (string.IsNullOrEmpty(model.Year) || string.IsNullOrEmpty(model.Month))
            {
                ViewBag.MessageLabel = "Please Select Year & Month";
                return View();
            }
            if (model.CowId < 1 && model.Date < 1)
            {
                return RedirectToAction("ProductionReportDetailForMonth", model);
            }
            else
            {
                return RedirectToAction("ProductionReportDetail", model);
            }
        }


        public ActionResult ProductionReportDetail(ProductionReportViewModel model)
        {
            var info = new ProductionReportDataSet();
            var details = _reportManager.GetProductionReport(model);
            info.Models = details;
            info.CompanyName = "North Bengal Dairy Firm";
            info.CompanyAddress = "Khristan Builder er Pashe, Solmaid, Vatara, Dhaka 1212 ";
            info.MobileNo = "01748-095352, 01978-095352";
            info.MonthParameter = model.Month;
            info.YearParameter = model.Year;
            info.DayParameter = model.Date.ToString();
            info.CowNumberParameter = model.CowId > 0 ? _cowSetupManager.Get(model.CowId).Number : "";
            return View(info);
        }

        public ActionResult ProductionReportDetailForMonth(ProductionReportViewModel model)
        {
            var info = new ProductionReportDataSet();
            var details = _reportManager.GetProductionReportWithOutDayNumber(model);
            info.Models = details;
            info.CompanyName = "North Bengal Dairy Firm";
            info.CompanyAddress = "Khristan Builder er Pashe, Solmaid, Vatara, Dhaka 1212 ";
            info.MobileNo = "01748-095352, 01978-095352";
            info.MonthParameter = model.Month;
            info.YearParameter = model.Year;
            info.DayParameter = model.Date.ToString();
            info.CowNumberParameter = model.CowId > 0 ? _cowSetupManager.Get(model.CowId).Number : "";
            return View(info);
        }
    }
}