using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.SetupModules;
using BusinessManagementSystemApp.Service.Menagers;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class CategoriesController : ApiController
    {
        private readonly CategoryMenager _categoryMenager;

        public CategoriesController()
        {
            _categoryMenager= new CategoryMenager();
        }
        // GET: api/Categories
        public IHttpActionResult Get()
        {
            try
            {
                var categories = _categoryMenager.GetAll().ToList();
                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // GET: api/Categories/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _categoryMenager.Get(id);
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
        public IHttpActionResult Post([FromBody]CategoryDto dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();

                return Ok(_categoryMenager.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Categories/5
        public IHttpActionResult Put(int id, [FromBody]CategoryDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_categoryMenager.Update(id, dto, user));
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
                return Ok(_categoryMenager.LogicalRemove(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
