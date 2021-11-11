using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class CowSetupsController : ApiController
    {
        private readonly CowSetupManager _cowSetupManager;

        public CowSetupsController()
        {
            _cowSetupManager= new CowSetupManager();
        }
        // GET: api/CowSetups
        public IHttpActionResult Get()
        {
            try
            {
                var infos = _cowSetupManager.GetAll().ToList();
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("api/CowSetups/GetHistory")]
        [HttpGet]
        public IHttpActionResult GetHistory()
        {
            try
            {
                var infos = _cowSetupManager.GetAll().OrderByDescending(c => c.Id).Take(3).ToList();
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/CowSetups/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _cowSetupManager.Get(id);
                if (entity == null)
                    return NotFound();

                return Ok(entity);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/CowSetups
        public IHttpActionResult Post([FromBody]CowSetupDto dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();

                return Ok(_cowSetupManager.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/CowSetups/5
        public IHttpActionResult Put(int id, [FromBody]CowSetupDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_cowSetupManager.Update(id, dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/CowSetups/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var user = User.Identity.GetUserId();
                return Ok(_cowSetupManager.LogicalRemove(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
