using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMSA.App.Controllers
{
    public class CowSetupsController : Controller
    {
        // GET: CowSetups
        public ActionResult CowEntryForm(int? id)
        {
            return View();  
        }

        public ActionResult CowList()
        {
            return View();
        }
    }
}