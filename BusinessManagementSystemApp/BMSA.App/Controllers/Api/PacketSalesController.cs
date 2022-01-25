using System;
using System.Linq;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.MilkSellsDtos;
using BusinessManagementSystemApp.Core.Dtos.Sales;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class PacketSalesController : ApiController
    {
        private readonly PacketSaleManager _saleManager;

        public PacketSalesController()
        {
            _saleManager= new PacketSaleManager();
        }
        // GET: api/PacketSales
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
        [Route("api/PacketSales/GetHistory")]
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

        [Route("api/PacketSales/GetSalesReport")]
        [HttpGet]
        public IHttpActionResult GetSalesReport(string year, string month)
        {
            try
            {
                var infos = _saleManager.GetMilkReport(year, month);
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/PacketSales/5
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
        [Route("api/PacketSales/GetByClientIdAndMonth")]
        public IHttpActionResult GetByClientIdAndMonth(int clientId, string month,string year)
        {
            try
            {
                var infos = _saleManager.GetAll().Where(c => c.ClientInfoId == clientId && c.SalesMonth.ToLower()== month.ToLower() && c.Year == year).ToList();
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("api/PacketSales/GetSalesInfoByAreaAndDate")]
        public IHttpActionResult GetSalesInfoByAreaAndDate(int areaId, DateTime date)
        {
            try
            {
                var info = _saleManager.GetAll().Where(c =>
                    c.AreaId == areaId && Convert.ToDateTime(c.SalesMonth) == Convert.ToDateTime(date)).ToList();
                return Ok(info);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("api/PacketSales/GetStatus")]
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

        // POST: api/PacketSales
        public IHttpActionResult Post([FromBody]PacketSaleListDto dto)
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

        // PUT: api/PacketSales/5
        public IHttpActionResult Put([FromBody]PacketSaleListDto dto)
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
