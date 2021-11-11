using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;

namespace BMSA.App.Controllers.MilkManament
{
    public class ReservationsController : Controller
    {
        // GET: Reservations
        public ActionResult ReservationForm(long? id)
        {
            ViewBag.Area = new SelectList(new List<Area>(), "Id", "Name");
            ViewBag.ClientInfoId = new SelectList(new List<ClientInfo>(), "Id", "Name");
            return View();
        }
    }
}