using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class ReservationsController : ApiController
    {
        private readonly MonthlyReservationManager _reservationManager;

        public ReservationsController()
        {
            _reservationManager = new MonthlyReservationManager();
        }


        // GET: api/Reservations
        public IHttpActionResult Get()
        {
            try
            {
                var categories = _reservationManager.GetAll().ToList();
                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // GET: api/Reservations/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _reservationManager.Get(id);
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
        [Route("api/Reservations/GetByClientId")]
        public IHttpActionResult GetByClientId(int clientId)
        {
            try
            {
                var infos = _reservationManager.GetAll().Where(c => c.ClientInfoId == clientId).ToList();
                return Ok(infos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Reservations
        public IHttpActionResult Post([FromBody]ReservationListDto dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();

                return Ok(_reservationManager.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Reservations/5
        public IHttpActionResult Put([FromBody]ReservationListDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_reservationManager.Update(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Reservations/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var user = User.Identity.GetUserId();
                return Ok(_reservationManager.LogicalRemove(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
