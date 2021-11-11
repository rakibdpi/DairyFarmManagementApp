using System.Collections.Generic;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;



namespace BMSA.App.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult SalesForm(int? id)
        {
            ViewBag.AreaId = new SelectList(new List<Area>(), "Id", "Name");
            ViewBag.ClientInfoId = new SelectList(new List<ClientInfo>(), "Id", "Name");
            return View();
        }

        public ActionResult SalesModifyForm()
        {
            ViewBag.AreaId = new SelectList(new List<Area>(), "Id", "Name");
            ViewBag.ClientInfoId = new SelectList(new List<ClientInfo>(), "Id", "Name");
            return View();
        }
    }
}