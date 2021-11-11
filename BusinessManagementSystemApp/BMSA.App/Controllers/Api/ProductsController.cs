using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.SetupModules;
using BusinessManagementSystemApp.Core.Models.OperationModules;
using BusinessManagementSystemApp.Core.ViewModels;
using BusinessManagementSystemApp.Service.Menagers;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private readonly ProductMenager _productMenager;
        private readonly DriveMenager _driveMenager;


        public ProductsController()
        {
            _productMenager = new ProductMenager();
            _driveMenager = new DriveMenager();
        }
        // GET: api/Products
        public IHttpActionResult Get()
        {
            try
            {
                var products = _productMenager.GetAll().ToList();

                var list = new List<ProductViewModel>();
                foreach (var p in products)
                {
                    var cow = new ProductViewModel();

                    cow.Id = p.Id;
                    cow.Code = p.Code;
                    cow.Color = p.Color;
                    cow.Weight = p.Weight;
                    cow.Age = p.Age;
                    if (p.Type == 1)
                    {
                        cow.Price = p.Weight * 420;
                    }
                    else
                    {
                        cow.Price = p.Weight * 650;
                    }
                    list.Add(cow);
                }

                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Products/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var products = _productMenager.Get(id);
                if (products == null)
                    return NotFound();
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        [Route("api/Products/GetByType")]
        public IHttpActionResult GetByType(int type)
        {
            try
            {
                var products = _productMenager.GetAll().Where(c=>c.Type == type).ToList();

                var list = new List<ProductViewModel>();
                foreach (var p in products)
                {
                    var cow = new ProductViewModel();

                    cow.Id = p.Id;
                    cow.Code = p.Code;
                    cow.Color = p.Color;
                    cow.Weight = p.Weight;
                    cow.Age = p.Age;
                    if (p.Type == 1)
                    {
                        cow.Price = p.Weight * 420;
                    }
                    else
                    {
                        cow.Price = p.Weight * 650;
                    }
                    list.Add(cow);
                }

                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        //[HttpGet]
        //[Route("api/Products/GetImageLink")]
        //public IHttpActionResult GetImageLink(int id)
        //{
        //    var topSheet = _productMenager.Get(id);
        //    var computerDrive = _driveMenager.Get();
        //    if (topSheet != null)
        //    {
        //        if (computerDrive != null)
        //        {
        //            try
        //            {
        //                string root = HttpContext.Current.Server.MapPath("~");
        //                var outputPath = root + @"Products";
        //                if (Directory.Exists(outputPath))
        //                {
        //                    System.IO.DirectoryInfo di = new DirectoryInfo(outputPath);
        //                    foreach (var file in di.GetFiles())
        //                    {
        //                        file.Delete();
        //                    }
        //                }
        //                else
        //                {
        //                    Directory.CreateDirectory(outputPath);
        //                }

        //                var link = computerDrive.Name + topSheet.ImagePath;
        //                var sourchFile = System.IO.Path.Combine(link);
        //                var tergetPath = System.IO.Path.Combine(root + @"Products", "image" + id + ".jpg");

        //                System.IO.File.Copy(sourchFile, tergetPath, true);
        //            }
        //            catch
        //            {
        //                // ignored
        //            }
        //        }

        //        return Ok(0);
        //    }

        //    return Ok(0);
        //}




        //[Route("api/Products/ImageUpload")]
        //[HttpPost]
        //public HttpResponseMessage Post()
        //{
        //    HttpResponseMessage result = null;
        //    var httpRequest = HttpContext.Current.Request;

        //    var id = HttpContext.Current.Request.Form.Get("id");

        //    // Check if files are available
        //    if (httpRequest.Files.Count > 0)
        //    {
        //        // Save Path
        //        var drive = _driveMenager.Get();
        //        if (drive != null)
        //        {
        //            var savePathWithoutDrive = ":\\BMSS\\Products\\" + id + "\\";
        //            string fileSavePath = drive.Name + savePathWithoutDrive;
        //            if (Directory.Exists(fileSavePath))
        //            {
        //                System.IO.DirectoryInfo di = new DirectoryInfo(fileSavePath);
        //                foreach (var file in di.GetFiles())
        //                {
        //                    file.Delete();
        //                }
        //            }
        //            else
        //            {
        //                Directory.CreateDirectory(fileSavePath);
        //            }

        //            try
        //            {
        //                // corruption file
        //                foreach (string file in httpRequest.Files)
        //                {
        //                    var pdfFile = httpRequest.Files[file];
        //                    if (pdfFile != null)
        //                    {
        //                        var fileName = Path.GetFileName(pdfFile.FileName);
        //                        // var fileExtension = Path.GetExtension(pdfFile.FileName);

        //                        // Check File is Exist
        //                        if (!System.IO.File.Exists(fileSavePath + fileName))
        //                        {
        //                            // Save file
        //                            pdfFile.SaveAs(fileSavePath + fileName);

        //                            // Save Path in Database
        //                            var pdf = new MainEntryPdfDto()
        //                            {
        //                                MasterId = Convert.ToInt32(id),
        //                                FilePath = fileSavePath + fileName
        //                            };

        //                            _productMenager.AddFile(pdf);
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e);
        //                throw;
        //            }
        //        }

        //        var files = new List<string>();
        //        // return result
        //        result = Request.CreateResponse(HttpStatusCode.Created, files);
        //    }
        //    else
        //    {
        //        // return BadRequest (no file(s) available)
        //        result = Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }

        //    return result;
        //}


        // POST: api/Products
        public IHttpActionResult Post([FromBody]ProductDto dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                {
                    return BadRequest("Input Value Not Valid");
                }

                var user = User.Identity.GetUserId();
                return Ok(_productMenager.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody]ProductDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_productMenager.Update(id,dto,user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Products/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var user = User.Identity.GetUserId();
                return Ok(_productMenager.LogicalRemove(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
