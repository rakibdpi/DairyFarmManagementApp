using System.Collections.Generic;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Core.Models.SupplierModules;

namespace BMSA.App.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult PurchaseForm(int? id)
        {
            ViewBag.SupplierId = new SelectList(new List<Supplier>(), "Id", "Name");
            ViewBag.ProductId = new SelectList(new List<Product>(), "Id", "Name");

            return View();
        }
    }
}