using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMSA.App.Controllers.MilkManament
{
    public class ClientReportsController : Controller
    {
        // GET: ClientReports
        public ActionResult ClientList()
        {
            ViewBag.AreaId = new SelectList(new List<Area>(), "Id", "Name");
            return View();
        }
    }
}