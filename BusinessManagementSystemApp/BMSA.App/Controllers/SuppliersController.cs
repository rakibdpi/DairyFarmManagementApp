using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMSA.App.Controllers
{
    public class SuppliersController : Controller
    {
        // GET: Suppliers
        public ActionResult SupplierForm(int? id)
        {
            return View();
        }
    }
}