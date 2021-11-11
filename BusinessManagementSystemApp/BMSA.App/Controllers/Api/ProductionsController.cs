using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Dtos.MilkProductionDtos;
using BusinessManagementSystemApp.Core.ViewModels.MilkProductionViewModels;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class ProductionsController : ApiController
    {
        private readonly ProductionManager _productionManager;

        public ProductionsController()
        {
            _productionManager= new ProductionManager();
        }
        // GET: api/Productions
        public IHttpActionResult Get()
        {
            try
            {
                var infos = _productionManager.GetAll().ToList();
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("api/Productions/GetHistory")]
        [HttpGet]
        public IHttpActionResult GetHistory()
        {
            try
            {
                var infos = _productionManager.GetAll().OrderByDescending(c => c.Id).Take(3).ToList();
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // GET: api/Productions/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _productionManager.Get(id);
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
        [Route("api/Productions/GetByClientIdAndMonth")]
        public IHttpActionResult GetByClientIdAndMonth(int cowId, string month , string year)
        {
            try
            {
                var infos = _productionManager.GetAll().Where(c => c.CowSetupId == cowId && c.ProductionMonth.ToLower() == month.ToLower() && c.Year == year).ToList();
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        [Route("api/Productions/GetStatus")]
        public IHttpActionResult GetStatus(int cowId, string month)
        {
            try
            {
                var status = _productionManager.IsSaleExist(cowId, month);
                return Ok(status);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Productions
        public IHttpActionResult Post([FromBody]ProductionListDto dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();

                return Ok(_productionManager.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Productions/5
        public IHttpActionResult Put([FromBody]ProductionListDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_productionManager.Update(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Productions/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var user = User.Identity.GetUserId();
                return Ok(_productionManager.LogicalRemove(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
