﻿using System.Web.Mvc;

namespace BMSA.App.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult InternalServerError()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NotFound()
        {
            return View();
        }
    }
}
