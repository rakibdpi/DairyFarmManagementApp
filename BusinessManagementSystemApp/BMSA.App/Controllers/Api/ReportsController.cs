using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;

namespace BMSA.App.Controllers.Api
{
    public class ReportsController : ApiController
    {
        private readonly ReportManager _reportManager;

        public ReportsController()
        {
            _reportManager= new ReportManager();
        }

        [HttpGet]
        [Route("api/Reports/SalesReportByDeliveryMenWise")]
        public IHttpActionResult SalesReportByDeliveryMenWise(string month, int year, int? deliveryManId, int? areaId)
        {
            try
            {
                var infos = _reportManager.SaleReportDeliveryManWise(month, year, deliveryManId, areaId);
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
