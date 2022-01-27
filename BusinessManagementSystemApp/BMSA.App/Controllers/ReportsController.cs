using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Dtos.OilSell;
using BusinessManagementSystemApp.Core.Models.Ghee;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Core.ReportModels.ViewModels;
using BusinessManagementSystemApp.Core.ViewModels.GheeSale;
using BusinessManagementSystemApp.Core.ViewModels.MilkMamagement;
using BusinessManagementSystemApp.Core.ViewModels.MuriSell;
using BusinessManagementSystemApp.Service.Menagers.Ghee;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement.SetupModules;
using BusinessManagementSystemApp.Service.Menagers.Muri;
using BusinessManagementSystemApp.Service.Menagers.OilSell;
using BusinessManagementSystemApp.Service.Menagers.ReportManager;
using Microsoft.Reporting.WebForms;

namespace BMSA.App.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ClientInfoMenager _manager;
        private readonly BillReportManager _billManager;
        private readonly AreaManager _areaManager;
        private readonly ReportManager _reportManager;
        private readonly GheeSaleMenager _gheeSaleMenager;
        private readonly OilSellManager _oilSellManager;
        private readonly MuriSaleManager _muriSaleManager;

        public ReportsController()
        {
            _manager= new ClientInfoMenager();
            _billManager= new BillReportManager();
            _areaManager= new AreaManager();
            _reportManager = new ReportManager();
            _gheeSaleMenager = new GheeSaleMenager();
            _oilSellManager = new OilSellManager();
            _muriSaleManager = new MuriSaleManager();
        }
        // GET: Reports
        public ActionResult BillReportForm()
        {
            ViewBag.AreaId = new SelectList(_areaManager.GetAll(), "Id", "Name");
            ViewBag.ClientId = new SelectList(new List<ClientInfo>(), "Id", "Name");
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult BillReportForm([FromBody]BillReportViewModel model)
        {
            try
            {

                CultureInfo cInfo = new CultureInfo("en-IN");
                ReportViewer viewer = new ReportViewer();

                string path = Path.Combine(Server.MapPath("/Reports"), "MonthlyBill.rdlc");
                viewer.LocalReport.ReportPath = path;

                //Ghee Sell

                var salecheck = _gheeSaleMenager.GetSale(model.Year, model.MonthId, model.ClientId);

                if (model.OneFourthKg != null || model.HalfKg != null || model.OneKg != null)
                {
                    if (salecheck != null)
                    {
                        var vm = new GheeSaleViewModel()
                        {
                            Id = salecheck.Id,
                            AreaId = model.AreaId,
                            ClientInfoId = model.ClientId,
                            SalesMonth = model.MonthId,
                            Year = model.Year,
                            OneFourthKg = model.OneFourthKg,
                            HalfKg = model.HalfKg,
                            OneKg = model.OneKg
                        };

                        var update = _gheeSaleMenager.Update(salecheck.Id, vm);
                    }
                    else
                    {
                        if (model.OneFourthKg != null || model.HalfKg != null || model.OneKg != null)
                        {
                            var vm = new GheeSaleViewModel()
                            {
                                AreaId = model.AreaId,
                                ClientInfoId = model.ClientId,
                                SalesMonth = model.MonthId,
                                Year = model.Year,
                                OneFourthKg = model.OneFourthKg,
                                HalfKg = model.HalfKg,
                                OneKg = model.OneKg
                            };

                            var save = _gheeSaleMenager.Add(vm, "Admin");
                        }
                    }
                }


                //Oil Sell

                var oilSellCheck = _oilSellManager.OilSaleExist(model.Year, model.MonthId, model.ClientId);

                if (model.OilOneKg != null || model.OilTwoKg != null || model.OilFiveKg != null)
                {
                    if (oilSellCheck != null)
                    {
                        if(model.OilOneKg == null)
                        {
                            model.OilOneKg = 0;
                        }
                        if (model.OilTwoKg == null)
                        {
                            model.OilTwoKg = 0;
                        }
                        if (model.OilFiveKg == null)
                        {
                            model.OilFiveKg = 0;
                        }

                        var vm = new OilSellDto()
                        {
                            Id = oilSellCheck.Id,
                            AreaId = model.AreaId,
                            ClientInfoId = model.ClientId,
                            SalesMonth = model.MonthId,
                            Year = model.Year,
                            DayNumber = 0,
                            OneKg = model.OilOneKg,
                            TwoKg = model.OilTwoKg,
                            FiveKg = model.OilFiveKg,
                            IsDelete = false,
                            CreateBy = "Admin",
                            CreateDate = DateTime.Now
                        };

                        var update = _oilSellManager.UpdateAgain(oilSellCheck.Id, vm);
                    }
                    else
                    {
                        if (model.OilOneKg != null || model.OilTwoKg != null || model.OilFiveKg != null)
                        {
                            if (model.OilOneKg == null)
                            {
                                model.OilOneKg = 0;
                            }
                            if (model.OilTwoKg == null)
                            {
                                model.OilTwoKg = 0;
                            }
                            if (model.OilFiveKg == null)
                            {
                                model.OilFiveKg = 0;
                            }
                            var vm = new OilSellDto()
                            {
                                AreaId = model.AreaId,
                                ClientInfoId = model.ClientId,
                                SalesMonth = model.MonthId,
                                Year = model.Year,
                                DayNumber = 0,
                                OneKg = model.OilOneKg,
                                TwoKg = model.OilTwoKg,
                                FiveKg = model.OilFiveKg,
                                  IsDelete = false,
                                CreateBy = "Admin",
                                CreateDate = DateTime.Now
                            };
                            var save = _oilSellManager.Add(vm, "Admin");
                        }
                    }
                }

                //Muri Sell

                var muriSalecheck = _muriSaleManager.GetSale(model.Year, model.MonthId, model.ClientId);

                if (model.MuriHalfKg != null || model.MuriOneKg != null )
                {
                    if (muriSalecheck != null)
                    {
                        var vm = new MuriSellVm()
                        {
                            Id = muriSalecheck.Id,
                            AreaId = model.AreaId,
                            ClientInfoId = model.ClientId,
                            SalesMonth = model.MonthId,
                            Year = model.Year,
                            HalfKg = model.MuriHalfKg,
                            OneKg = model.MuriOneKg
                        };

                        var update = _muriSaleManager.Update(muriSalecheck.Id, vm);
                    }
                    else
                    {
                        if (model.MuriHalfKg != null || model.MuriOneKg != null)
                        {
                            var vm = new MuriSellVm()
                            {
                                AreaId = model.AreaId,
                                ClientInfoId = model.ClientId,
                                SalesMonth = model.MonthId,
                                Year = model.Year,
                                HalfKg = model.MuriHalfKg,
                                OneKg = model.MuriOneKg
                            };

                            var save = _muriSaleManager.Add(vm, "Admin");
                        }
                    }
                }

                var reports = _billManager.GetMaster(model.ClientId, model.MonthId,model.Year);
                var masterDataSource = new ReportDataSource("BillMasterDataSet", reports);
                viewer.LocalReport.DataSources.Add(masterDataSource);

                var details = _billManager.GetBillInfo(model.ClientId, model.MonthId,model.Year);
                var detailDataSource = new ReportDataSource("BillDetailsDataSet", details);
                viewer.LocalReport.DataSources.Add(detailDataSource);

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension,
                    out streamIds, out warnings);


                string fileName = DateTime.Now.ToString("dd_MM_yyyy");
                string outputPath = "~/Reports/ReportPdf";
                //var di = new DirectoryInfo(Server.MapPath(outputPath));
                if (System.IO.File.Exists(Server.MapPath(outputPath + fileName + ".pdf")))
                {
                    try
                    {
                        System.IO.File.Delete(Server.MapPath(outputPath + fileName + ".pdf"));
                    }
                    catch (Exception)
                    {
                        fileName = DateTime.Now.ToString("dd_MM_yyyy");
                    }

                }

                using (var stream = System.IO.File.Create(Path.Combine(Server.MapPath(outputPath), fileName + ".pdf")))
                {
                    stream.Write(bytes, 0, bytes.Length);
                }

                var pdfHref = "/Reports/ReportPdf/" + fileName + ".pdf";

                return Json(pdfHref, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        [System.Web.Mvc.HttpPost]
        public JsonResult ClientReport([FromBody]int areaId,int type)
        {
            try
            {
                CultureInfo cInfo = new CultureInfo("en-IN");
                ReportViewer viewer = new ReportViewer();

                string path = Path.Combine(Server.MapPath("/Reports"), "ClientInformation.rdlc");
                viewer.LocalReport.ReportPath = path;


                var reports = _manager.GetClientInfo(areaId,type);
                var masterDataSource = new ReportDataSource("ClientInfoDataset", reports);
                viewer.LocalReport.DataSources.Add(masterDataSource);


                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension,
                    out streamIds, out warnings);


                string fileName = DateTime.Now.ToString("dd_MM_yyyy");
                string outputPath = "~/Reports/ClientReportPdf";
                //var di = new DirectoryInfo(Server.MapPath(outputPath));
                if (System.IO.File.Exists(Server.MapPath(outputPath + fileName + ".pdf")))
                {
                    try
                    {
                        System.IO.File.Delete(Server.MapPath(outputPath + fileName + ".pdf"));
                    }
                    catch (Exception)
                    {
                        fileName = DateTime.Now.ToString("dd_MM_yyyy");
                    }

                }

                using (var stream = System.IO.File.Create(Path.Combine(Server.MapPath(outputPath), fileName + ".pdf")))
                {
                    stream.Write(bytes, 0, bytes.Length);
                }

                var pdfHref = "/Reports/ClientReportPdf/" + fileName + ".pdf";

                return Json(pdfHref, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        [System.Web.Mvc.HttpPost]
        public JsonResult ClientReportWord([FromBody]int areaId,int type)
        {
            try
            {
                CultureInfo cInfo = new CultureInfo("en-IN");
                ReportViewer viewer = new ReportViewer();

                string path = Path.Combine(Server.MapPath("/Reports"), "ClientInformation.rdlc");
                viewer.LocalReport.ReportPath = path;


                var reports = _manager.GetClientInfo(areaId,type);
                var masterDataSource = new ReportDataSource("ClientInfoDataset", reports);
                viewer.LocalReport.DataSources.Add(masterDataSource);


                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                byte[] bytes2 = viewer.LocalReport.Render("WORD", null, out mimeType, out encoding, out extension,
                    out streamIds, out warnings);

                string fileName2 = DateTime.Now.ToString("dd_MM_yyyy");
                string outputPath2 = "~/Reports/ClientReportPdf";
                //var di = new DirectoryInfo(Server.MapPath(outputPath));
                if (System.IO.File.Exists(Server.MapPath(outputPath2 + fileName2 + ".doc")))
                {
                    try
                    {
                        System.IO.File.Delete(Server.MapPath(outputPath2 + fileName2 + ".doc"));
                    }
                    catch (Exception)
                    {
                        fileName2 = DateTime.Now.ToString("dd_MM_yyyy");
                    }

                }

                using (var stream = System.IO.File.Create(Path.Combine(Server.MapPath(outputPath2), fileName2 + ".doc")))
                {
                    stream.Write(bytes2, 0, bytes2.Length);
                }

                var pdfHref2 = "/Reports/ClientReportPdf/" + fileName2 + ".doc";
                return Json(pdfHref2, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ActionResult SalesReport()
        {
            ViewBag.DeliveryManId = new SelectList(new List<DeliveryMan>(), "Id", "Name");
            ViewBag.AreaId = new SelectList(new List<Area>(), "Id", "Name");
            return View();
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult SalesReport(SalesReportViewModel model)
        {
            ViewBag.DeliveryManId = new SelectList(new List<DeliveryMan>(), "Id", "Name");
            ViewBag.AreaId = new SelectList(new List<Area>(), "Id", "Name");
            return RedirectToAction("SalesReportDetail", model);
        }

        public ActionResult SalesReportDetail(SalesReportViewModel model)
        {
            var info = new SaleReportByDeliveryMenMasterViewModel();
            var details = _reportManager.SaleReportDeliveryManWise(model.Month, model.Year, model.DeliveryManId, model.AreaId);
            info.Models = details;
            info.CompanyName = "North Bengal Dairy Firm";
            info.Address = "Khristan Builder er Pashe, Solmaid, Vatara, Dhaka 1212 ";
            info.PhoneNo = "01748-095352, 01978-095352";
            info.Month = model.Month;
            info.Year = model.Year.ToString();
            return View(info);
        }
    }
}
