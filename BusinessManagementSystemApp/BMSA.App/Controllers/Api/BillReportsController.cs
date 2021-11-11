using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Service.Menagers.ReportManager;

namespace BMSA.App.Controllers.Api
{
    public class BillReportsController : ApiController
    {
        private readonly BillReportManager _billManager;

        public BillReportsController()
        {
            _billManager= new BillReportManager();
        }

    }
}
