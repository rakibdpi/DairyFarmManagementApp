using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.ReportModels.ViewModels;
using BusinessManagementSystemApp.Core.ViewModels.ReportViewModels;
using BusinessManagementSystemApp.Service.Menagers;
using BusinessManagementSystemApp.Service.Menagers.ReportManager;
using Microsoft.Reporting.WebForms;

namespace BMSA.App.Controllers.MilkManament
{
    public class PaymentsController : Controller
    {
        PaymentService _manager = new PaymentService();
        private readonly BillReportManager _billReportManager = new BillReportManager();


        // GET: Payments
        public ActionResult Index()
        {
            ViewBag.AreaId = new SelectList(new List<Area>(), "Id", "Name");
            ViewBag.ClientInfoId = new SelectList(new List<ClientInfo>(), "Id", "Name");
            return View();
        }


        public ActionResult DueAndPaymentDelete()
        {
            ViewBag.AreaId = new SelectList(new List<Area>(), "Id", "Name");
            ViewBag.ClientInfoId = new SelectList(new List<ClientInfo>(), "Id", "Name");
            return View();
        }




        [System.Web.Mvc.HttpPost]
        //[System.Web.Http.Route("Payments/BillReport")]
        public JsonResult BillReport([FromBody]BillSummaryVm dto)
        {
            try
            {
                CultureInfo cInfo = new CultureInfo("en-IN");
                ReportViewer viewer = new ReportViewer();

                string path = Path.Combine(Server.MapPath("/Reports"), "SummaryReport.rdlc");
                viewer.LocalReport.ReportPath = path;


                var reports = _manager.GetMilkDetailsSummary(dto.Year,dto.Month);
                var masterDataSource = new ReportDataSource("MilkSaleSummary", reports);
                viewer.LocalReport.DataSources.Add(masterDataSource);


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
        //[System.Web.Http.Route("Payments/BillReport")]
        public JsonResult BillPaymentReport([FromBody]PaymentReportViewModel dto)
        {
            try
            {
                CultureInfo cInfo = new CultureInfo("en-IN");
                ReportViewer viewer = new ReportViewer();

                string path = Path.Combine(Server.MapPath("/Reports"), "RunningDueReport.rdlc");
                viewer.LocalReport.ReportPath = path;


                var reports = _billReportManager.RunningPaymentDue(dto.AreaId,dto.Year, dto.Month);
                var masterDataSource = new ReportDataSource("DueBillReportDataset", reports);
                viewer.LocalReport.DataSources.Add(masterDataSource);


                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension,
                    out streamIds, out warnings);


                string fileName ="RunningDue_Report_" + DateTime.Now.ToString("dd_MM_yyyy");
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






    }
}