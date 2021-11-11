using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Models.SetupModules;

namespace BMSA.App.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult ProductForm(int? id)
        {
            ViewBag.CategoryId = new SelectList(new List<Category>(), "Id", "Name");
            return View();
        }
    }
}