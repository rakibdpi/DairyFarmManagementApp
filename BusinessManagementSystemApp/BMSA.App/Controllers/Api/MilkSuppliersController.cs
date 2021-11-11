using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement.MilkPurchases;

namespace BMSA.App.Controllers.Api
{
    public class MilkSuppliersController : ApiController
    {
        private readonly MilkSupplierManager _milkSupplierManager;

        public MilkSuppliersController()
        {
            _milkSupplierManager = new MilkSupplierManager();
        }

        // GET: api/MilkSuppliers
        public IHttpActionResult Get()
        {
            try
            {
                var purchase = _milkSupplierManager.GetAll().ToList();
                return Ok(purchase);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/MilkSuppliers/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MilkSuppliers
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MilkSuppliers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MilkSuppliers/5
        public void Delete(int id)
        {
        }
    }
}
