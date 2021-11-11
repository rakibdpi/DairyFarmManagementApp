using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;

namespace BMSA.App.Controllers
{
    public class OilSellController : Controller
    {
        // GET: OilSell
        public ActionResult SellForm()
        {
            ViewBag.AreaId = new SelectList(new List<Area>(), "Id", "Name");
            ViewBag.ClientInfoId = new SelectList(new List<ClientInfo>(), "Id", "Name");
            return View();
        }

        public ActionResult SellEditForm()
        {
            ViewBag.AreaId = new SelectList(new List<Area>(), "Id", "Name");
            ViewBag.ClientInfoId = new SelectList(new List<ClientInfo>(), "Id", "Name");
            return View();
        }
    }
}