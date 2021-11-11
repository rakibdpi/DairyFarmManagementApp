using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.MilkPurchase;
using BusinessManagementSystemApp.Core.ViewModels.MilkPurchase;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement.MilkPurchases;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class MilkPurchasesController : ApiController
    {
        private readonly MilkPurchaseManager _milkPurchaseManager;

        public MilkPurchasesController()
        {
            _milkPurchaseManager = new MilkPurchaseManager();
        }

        [Route("api/MilkPurchases/GetHistory")]
        [HttpGet]
        public IHttpActionResult GetHistory(int supplierId)
        {
            try
            {
                var infos = _milkPurchaseManager.GetAll(supplierId).OrderByDescending(c => c.Id).Take(3).ToList();
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/MilkPurchases/GetMilkPurchaseBySupplier")]
        public IHttpActionResult GetMilkPurchaseBySupplier(int supplierId)
        {
            try
            {
                var purchase = _milkPurchaseManager.GetAll(supplierId).ToList();
                return Ok(purchase);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // GET: api/MilkPurchases
        public IHttpActionResult Get()
        {
            try
            {
                var purchase = _milkPurchaseManager.GetAll().ToList();
                return Ok(purchase);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // GET: api/MilkPurchases/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _milkPurchaseManager.Get(id);
                if (entity == null)
                    return NotFound();
                return Ok(entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    

        // POST: api/MilkPurchases
        public IHttpActionResult Post([FromBody]MilkPurchaseDto dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                {
                    return BadRequest("Input Value Not Valid");
                }
                else
                {
                    var user = User.Identity.GetUserId();
                    return Ok(_milkPurchaseManager.Add(dto, user));
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/MilkPurchases/5
        public IHttpActionResult Put(int id, [FromBody]MilkPurchaseDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_milkPurchaseManager.Update(id, dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/MilkPurchases/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var user = User.Identity.GetUserId();
                return Ok(_milkPurchaseManager.LogicalRemove(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
