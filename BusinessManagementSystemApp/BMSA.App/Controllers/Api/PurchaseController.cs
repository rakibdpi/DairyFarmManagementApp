using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using BusinessManagementSystemApp.Core.Dtos.Purchase;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;
using BusinessManagementSystemApp.Core.ViewModels;
using BusinessManagementSystemApp.Service.Menagers;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class PurchaseController : ApiController
    {
        private readonly PurchaseManager _purchaseManager;

        public PurchaseController()
        {
            _purchaseManager = new PurchaseManager();
        }

        // GET: api/Purchase
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Purchase/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Purchase
        public IHttpActionResult Post(PurchaseDto dto)
        {
            var exam = Mapper.Map<PurchaseDto, Purchase>(dto);

            var success = _purchaseManager.Save(exam);
            return Ok(success);
        }

        // PUT: api/Purchase/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Purchase/5
        public void Delete(int id)
        {
        }

        [Route("api/Purchase/GetPreviousPriceProduct")]
        [HttpGet]
        public IHttpActionResult GetPreviousPriceProduct(int productId)
        {
            try
            {
                var entity = _purchaseManager.GetPreviousPriceProduct(productId);
                if (entity == null)
                    return NotFound();

                return Ok(entity);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
