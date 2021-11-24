using System.Collections.Generic;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Models.SetupModules;

namespace BMSA.App.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
           // ViewBag.ProductId = new SelectList(new List<Product>(), "Id", "Code");

            return View();
        }

        [HttpGet]
        public ActionResult AnotherLink()
        {

            return View("Index");
        }
    }
}
