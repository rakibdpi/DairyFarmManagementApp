using BusinessManagementSystemApp.Core.ViewModels.AsthaShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMSA.App.Controllers.AsthaShop
{
    public class BusinessPartnerInfosController : Controller
    {
        // GET: BusinessPartnerInfos
        public ActionResult BusinessPartnerInfoFrom(int? id)
        {
            var viewModel = new BusinessPartnerInfoViewModel() { IsActive = true };
            return View(viewModel);
        }
    }
}