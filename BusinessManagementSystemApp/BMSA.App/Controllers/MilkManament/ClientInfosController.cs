using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.ViewModels.MilkMamagement;

namespace BMSA.App.Controllers.MilkManament
{
    public class ClientInfosController : Controller
    {
        // GET: ClientInfos
        public ActionResult ClientInfoForm(int? id)
        {
            ViewBag.AreaId = new SelectList(new List<Area>(), "Id", "Name");

            var viewModel = new ClientInfoViewModels() { IsActive = true };
            return View(viewModel);
        }

        public ActionResult ActiveClientInfo()
        {
            return View();
        }

        public ActionResult DeActiveClientInfo()
        {
            return View();
        }
    }
}