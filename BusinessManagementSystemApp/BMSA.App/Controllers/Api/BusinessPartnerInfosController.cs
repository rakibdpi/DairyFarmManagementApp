using BusinessManagementSystemApp.Core.Dtos.AsthaShopDto;
using BusinessManagementSystemApp.Service.Menagers.AsthaOnline;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMSA.App.Controllers.Api
{
    public class BusinessPartnerInfosController : ApiController
    {
        private readonly BusinessPartnerInfoMenager _businessPartnerInfo;

        public BusinessPartnerInfosController()
        {
            _businessPartnerInfo = new BusinessPartnerInfoMenager();
        }

        // GET: api/BusinessPartnerInfos
        public IHttpActionResult Get()
        {
            try
            {
                var categories = _businessPartnerInfo.GetAll().ToList();
                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // GET: api/BusinessPartnerInfos/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _businessPartnerInfo.Get(id);
                if (entity == null)
                    return NotFound();

                return Ok(entity);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/BusinessPartnerInfos
        public IHttpActionResult Post([FromBody]BusinessPartnerInfoDto dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();

                return Ok(_businessPartnerInfo.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/BusinessPartnerInfos/5
        public IHttpActionResult Put(int id, [FromBody]BusinessPartnerInfoDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_businessPartnerInfo.Update(id, dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/BusinessPartnerInfos/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var user = User.Identity.GetUserId();
                return Ok(_businessPartnerInfo.LogicalRemove(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
