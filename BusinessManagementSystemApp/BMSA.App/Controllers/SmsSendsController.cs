using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;

namespace BMSA.App.Controllers
{
    public class SmsSendsController : Controller
    {
        // GET: SmsSends
        public ActionResult SmsSendForm()
        {
            ViewBag.AreaId = new SelectList(new List<Area>(), "Id", "Name");
            return View();
        }

        public ActionResult MessageForm()
        {
            ViewBag.AreaId = new SelectList(new List<Area>(), "Id", "Name");
            return View();
        }
    }
}