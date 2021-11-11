using System;
using System.Linq;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;

namespace BMSA.App.Controllers.Api
{
    public class DeliveryMenController : ApiController
    {
        private readonly DeliveryManManager _deliveryManManager;

        public DeliveryMenController()
        {
            _deliveryManManager = new DeliveryManManager();
        }

        // GET: api/DeliveryMen
        public IHttpActionResult Get()
        {
            try
            {
                var customer = _deliveryManManager.GetAll().OrderBy(c => c.Name).ToList();
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/DeliveryMen/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _deliveryManManager.Get(id);
                if (entity == null)
                    return NotFound();

                return Ok(entity);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/DeliveryMen
        public IHttpActionResult Post([FromBody]DeliveryMan dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                return Ok(_deliveryManManager.Add(dto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/DeliveryMen/5
        public IHttpActionResult Put(int id, [FromBody]DeliveryMan dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");
                
                return Ok(_deliveryManManager.Update(id, dto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
