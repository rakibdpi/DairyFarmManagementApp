using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.CustomerModules;
using BusinessManagementSystemApp.Core.Dtos.SetupModules;
using BusinessManagementSystemApp.Service.Menagers;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly CustomerManager _customerManager;

        public CustomersController()
        {
            _customerManager = new CustomerManager();
        }


        // GET: api/Customers
        public IHttpActionResult Get()
        {
            try
            {
                var customer = _customerManager.GetAll().ToList();
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Customers/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _customerManager.Get(id);
                if (entity == null)
                    return NotFound();

                return Ok(entity);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Customers
        public IHttpActionResult Post([FromBody]CustomerDto dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();

                return Ok(_customerManager.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Customers/5
        public IHttpActionResult Put(int id, [FromBody]CustomerDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_customerManager.Update(id, dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Customers/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var user = User.Identity.GetUserId();
                return Ok(_customerManager.LogicalRemove(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
