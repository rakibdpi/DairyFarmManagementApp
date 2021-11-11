using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.CustomerModules;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement.SetupModules;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class AreasController : ApiController
    {
        private readonly AreaManager _areaManager;

        public AreasController()
        {
            _areaManager = new AreaManager();
        }

        // GET: api/Areas
        public IHttpActionResult Get()
        {
            try
            {
                var customer = _areaManager.GetAll().ToList();
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Areas/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _areaManager.Get(id);
                if (entity == null)
                    return NotFound();

                return Ok(entity);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("api/Areas/GetByArea")]
        [HttpGet]
        public IHttpActionResult GetByArea(int areaId)
        {
            try
            {
                var entity = _areaManager.GetAll().SingleOrDefault(c => c.Id == areaId);
                if (entity == null)
                    return NotFound();

                return Ok(entity);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // POST: api/Areas
        public IHttpActionResult Post([FromBody]AreaDto dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();

                return Ok(_areaManager.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Areas/5
        public IHttpActionResult Put(int id, [FromBody]AreaDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_areaManager.Update(id, dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Areas/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var user = User.Identity.GetUserId();
                return Ok(_areaManager.LogicalRemove(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
