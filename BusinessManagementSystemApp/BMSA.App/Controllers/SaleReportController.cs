using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMSA.App.Controllers
{
    public class SaleReportController : Controller
    {
        // GET: SaleReport
        public ActionResult Report()
        {
            return View();
        }
    }
}