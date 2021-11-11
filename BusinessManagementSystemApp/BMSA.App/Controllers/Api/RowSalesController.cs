using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.MilkProductionDtos;
using BusinessManagementSystemApp.Core.Dtos.MilkSellsDtos;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class RowSalesController : ApiController
    {
        private readonly RowSaleManager _saleManager;

        public RowSalesController()
        {
            _saleManager= new RowSaleManager();
        }
        // GET: api/RowSales
        public IHttpActionResult Get()
        {
            try
            {
                var infos = _saleManager.GetAll().ToList();
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("api/RowSales/GetHistory")]
        [HttpGet]
        public IHttpActionResult GetHistory()
        {
            try
            {
                var infos = _saleManager.GetAll().OrderByDescending(c => c.Id).Take(3).ToList();
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/RowSales/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _saleManager.Get(id);
                if (entity == null)
                    return NotFound();

                return Ok(entity);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        // POST: api/RowSales
        public IHttpActionResult Post([FromBody]RowSaleDto model)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();

                return Ok(_saleManager.Add(model, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/RowSales/5
        public IHttpActionResult Put(int id, [FromBody]RowSaleDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_saleManager.Update(id, model, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/RowSales/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var user = User.Identity.GetUserId();
                return Ok(_saleManager.LogicalRemove(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
