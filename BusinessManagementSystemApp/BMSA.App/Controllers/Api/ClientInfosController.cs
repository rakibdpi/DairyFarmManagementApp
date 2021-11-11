using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement;
using BusinessManagementSystemApp.Core.Dtos.SetupModules;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class ClientInfosController : ApiController
    {

        private readonly ClientInfoMenager _clientInfoMenager;

        public ClientInfosController()
        {
            _clientInfoMenager = new ClientInfoMenager();
        }


        // GET: api/ClientInfos
        public IHttpActionResult Get()
        {
            try
            {
                var categories = _clientInfoMenager.GetAll().ToList();
                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("api/ClientInfos/GetClientCode")]
        [HttpGet]
        public IHttpActionResult GetClientCode()
        {
            try
            {
                var clientCode = _clientInfoMenager.GenerateCode();
                return Ok(clientCode);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [Route("api/ClientInfos/GetAllActive")]
        [HttpGet]
        public IHttpActionResult GetAllActive()
        {
            try
            {
                var categories = _clientInfoMenager.GetAllActive().ToList();
                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("api/ClientInfos/GetAllDeActive")]
        [HttpGet]
        public IHttpActionResult GetAllDeActive()
        {
            try
            {
                var categories = _clientInfoMenager.GetAllDeActive().ToList();
                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("api/ClientInfos/GetClientByArea")]
        [HttpGet]
        public IHttpActionResult GetClientByArea(int areaId)
        {
            try
            {
                var info = _clientInfoMenager.GetAllActive().Where(c => c.AreaId == areaId).ToList().OrderBy(c=>c.Code);
                return Ok(info);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/ClientInfos/GetNextClient")]
        [HttpGet]
        public IHttpActionResult GetNextClient(int areaId, int clientId)
        {
            try
            {
                var allClients = _clientInfoMenager.GetAllActive().OrderBy(c => c.Code).ToList();
                var clients = allClients.Where(c => c.AreaId == areaId).OrderBy(c => c.Code).ToList();
                var index = clients.FindIndex(c => c.Id == clientId);
                if (index == clients.Count - 1)
                {
                    return BadRequest("this is the last Customer in this area");
                }
                var client = clients.Skip(index + 1).Take(1).FirstOrDefault();
                return Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/ClientInfos/GetNextClientByNumber")]
        [HttpGet]
        public IHttpActionResult GetNextClientByNumber(int areaId, int clientId, int number)
        {
            try
            {
                var clients = _clientInfoMenager.GetAllActive().Where(c => c.AreaId == areaId).OrderBy(c => c.Id).ToList();
                var index = clients.FindIndex(c => c.Id == clientId);
                if (index== 0 && number==-1)
                {
                    return BadRequest("this is the first Customer in this area");
                }
                if (index== clients.Count - 1 && number == 1)
                {
                    return BadRequest("this is the last Customer in this area");
                }
                var client = clients.Skip(index + number).Take(1).FirstOrDefault();
                return Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: api/ClientInfos/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _clientInfoMenager.Get(id);
                if (entity == null)
                    return NotFound();

                return Ok(entity);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Categories
        public IHttpActionResult Post([FromBody]ClientInfoDto dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");
                var isExist = _clientInfoMenager.CodeExistCheck(dto.Code,dto.Id);
                if (isExist)
                {
                    return BadRequest("Code No All Ready Exist");
                }
                var user = User.Identity.GetUserId();

                return Ok(_clientInfoMenager.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/ClientInfos/5
        public IHttpActionResult Put(int id, [FromBody]ClientInfoDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var isExist = _clientInfoMenager.CodeExistCheck(dto.Code,dto.Id);
                if (isExist)
                {
                    return BadRequest("Code No All Ready Exist");
                }
                var user = User.Identity.GetUserId();
                return Ok(_clientInfoMenager.Update(id, dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Categories/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var user = User.Identity.GetUserId();
                return Ok(_clientInfoMenager.LogicalRemove(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
