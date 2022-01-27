using BusinessManagementSystemApp.Core.ReportModels.ViewModels;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMSA.App.Controllers.Api
{
    public class DueBillsController : ApiController
    {
        private readonly DueBillManager _manager;

        public DueBillsController()
        {
            _manager = new DueBillManager();
        }

        // GET: api/DueBills
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public IHttpActionResult GetDueData(int areaId,string year,string month)
        {
            try
            {
                var data = _manager.GetDueBillData(areaId, month, year).ToList();
                if (data == null)
                    return NotFound();

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // GET: api/DueBills/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DueBills
        public IHttpActionResult Post([FromBody]BillReportViewModel model)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");


                return Ok(_manager.Add(model));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/DueBills/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DueBills/5
        public void Delete(int id)
        {
        }
    }
}
