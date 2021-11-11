using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.AsthaShopDto;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Models.Base;
using BusinessManagementSystemApp.Service.Menagers.AsthaOnline;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class DataTransectionsController : ApiController
    {
        private readonly DataTransectionManager _dataTransection;

        public DataTransectionsController()
        {
            _dataTransection = new DataTransectionManager();

        }

        // GET: api/DataTransections
        public IHttpActionResult Get()
        {
            try
            {
                var customer = _dataTransection.GetAll().ToList();
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/DataTransections/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _dataTransection.Get(id);
                if (entity == null)
                    return NotFound();

                return Ok(entity);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/DataTransections
        [HttpPost]
        public IHttpActionResult Post([FromUri] int dataTypeId, decimal value)
        {
            try
            {
                var dto = new TransectionDataDto()
                {
                    DataTypeId = dataTypeId,
                    Value = value
                };

                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_dataTransection.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/DataTransections/5
        public IHttpActionResult Put(int id, [FromBody]TransectionDataDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_dataTransection.Update(id, dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/DataTransections/5
        public void Delete(int id)
        {
        }


        [Route("api/DataTransections/GetReport")]
        [HttpGet]
        public IHttpActionResult GetReport(string fromDate, string toDate, int? dataTypeId)
        {
            try
            {
                DateTime? fDate = null;
                DateTime? tDate = null;
                if (!string.IsNullOrEmpty(fromDate))
                    fDate = DateTimeFormatter.StringToDate(fromDate);
                if (!string.IsNullOrEmpty(toDate))
                    tDate = DateTimeFormatter.StringToDate(toDate);

                var infos = _dataTransection.GetAllForReport(fDate, tDate,dataTypeId);
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
