using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Service.Menagers.AsthaOnline;

namespace BMSA.App.Controllers.Api
{
    public class DataTypesController : ApiController
    {
        private readonly DataTypeManager _dataTypeManager;

        public DataTypesController()
        {
            _dataTypeManager = new DataTypeManager();
        }

        // GET: api/DataTypes
        public IHttpActionResult Get()
        {
            try
            {
                var dataType = _dataTypeManager.GetAll().ToList();
                return Ok(dataType);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/DataTypes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DataTypes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DataTypes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DataTypes/5
        public void Delete(int id)
        {
        }



    }
}
