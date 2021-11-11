using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMSA.App.Controllers
{
    public class CutomersController : Controller
    {
        // GET: Cutomers
        public ActionResult Customer(int? id)
        {
            return View();
        }
    }
}