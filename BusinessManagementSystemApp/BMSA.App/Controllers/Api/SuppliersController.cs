using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.SupplierModules;
using BusinessManagementSystemApp.Service.Menagers;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class SuppliersController : ApiController
    {
        private readonly SupplierMenager _supplierMenager;

        public SuppliersController()
        {
                _supplierMenager = new SupplierMenager();
        }


        // GET: api/Suppliers
        public IHttpActionResult Get()
        {
            try
            {
                var customer = _supplierMenager.GetAll().ToList();
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

    
        }

        // GET: api/Suppliers/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _supplierMenager.Get(id);
                if (entity == null)
                    return NotFound();

                return Ok(entity);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Suppliers
        public IHttpActionResult Post([FromBody]SupplierDto dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();

                return Ok(_supplierMenager.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Suppliers/5
        public IHttpActionResult  Put(int id, [FromBody]SupplierDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_supplierMenager.Update(id, dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Suppliers/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var user = User.Identity.GetUserId();
                return Ok(_supplierMenager.LogicalRemove(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
