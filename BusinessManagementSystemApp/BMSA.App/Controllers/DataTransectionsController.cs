using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Models.AsthaShop;

namespace BMSA.App.Controllers
{
    public class DataTransectionsController : Controller
    {
        // GET: DataTransections
        public ActionResult NewData(int? id)
        {
            ViewBag.DataTypeId = new SelectList(new List<DataType>(), "Id", "Name");
            return View();
        }


        public ActionResult Report()
        {
            ViewBag.DataTypeId = new SelectList(new List<DataType>(), "Id", "Name");
            return View();

        }

    }
}