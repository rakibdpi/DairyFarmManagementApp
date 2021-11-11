using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Core.ViewModels.MilkMamagement;

namespace BMSA.App.Controllers.MilkManament
{
    public class AreasController : Controller
    {
        // GET: Areas
        public ActionResult AreaSetupForm()
        {
            ViewBag.DeliveryManId = new SelectList(new List<DeliveryMan>(), "Id", "Name");
            var vm = new AreaViewModel() {IsActive = true};
            return View(vm);
        }
    }
}