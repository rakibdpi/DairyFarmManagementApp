using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.OilSell;
using BusinessManagementSystemApp.Core.Dtos.Sales;
using BusinessManagementSystemApp.Service.Menagers.OilSell;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class OilSellsController : ApiController
    {
        private readonly OilSellManager _saleManager;

        public OilSellsController()
        {
            _saleManager = new OilSellManager();
        }

        // GET: api/OilSells
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


        // GET: api/OilSells/5
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

        [HttpGet]
        [Route("api/OilSells/GetByClientIdAndMonth")]
        public IHttpActionResult GetByClientIdAndMonth(int clientId, string month)
        {
            try
            {
                var infos = _saleManager.GetAll().Where(c => c.ClientInfoId == clientId && c.SalesMonth.ToLower() == month.ToLower()).ToList();
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet]
        [Route("api/OilSells/GetStatus")]
        public IHttpActionResult GetStatus(int clientId, string month)
        {
            try
            {
                var status = _saleManager.IsSaleExist(clientId, month);
                return Ok(status);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/OilSells
        public IHttpActionResult Post([FromBody]OilSaleListDto dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();

                return Ok(_saleManager.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/OilSells/5
        public IHttpActionResult Put([FromBody]OilSaleListDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_saleManager.Update(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/PacketSales/5
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
