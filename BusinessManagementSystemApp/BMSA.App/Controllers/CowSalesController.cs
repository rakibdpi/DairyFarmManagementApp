using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Models.CustomerModules;
using BusinessManagementSystemApp.Core.ReportModels.ViewModels;
using BusinessManagementSystemApp.Service.Menagers;
using Microsoft.Reporting.WebForms;

namespace BMSA.App.Controllers
{
    public class CowSalesController : Controller
    {
        private readonly SalesManager _manager;

        public CowSalesController()
        {
            _manager = new SalesManager();
        }

        // GET: CowSales
        public ActionResult Sale(int? id)
        {

            ViewBag.CustomerId = new SelectList(new List<Customer>(), "Id", "Name");

            return View();
        }



        [System.Web.Mvc.HttpPost]
        public JsonResult BillReportForm([FromBody]string invoiceNo)
        {
            try
            {
                CultureInfo cInfo = new CultureInfo("en-IN");
                ReportViewer viewer = new ReportViewer();

                string path = Path.Combine(Server.MapPath("/Reports/SaleReport"), "SalesInvoice.rdlc");
                viewer.LocalReport.ReportPath = path;


                var reports = _manager.GetSale(invoiceNo);
                var masterDataSource = new ReportDataSource("CowSaleDataset", reports);
                viewer.LocalReport.DataSources.Add(masterDataSource);

                var saleId = _manager.GetSaleId(invoiceNo);

                var details = _manager.GetSaleDetail(saleId);
                var detailDataSource = new ReportDataSource("SaleDetailsDataset", details);
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



    }
}