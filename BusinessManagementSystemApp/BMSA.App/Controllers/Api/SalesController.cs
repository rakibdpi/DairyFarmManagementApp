using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using BusinessManagementSystemApp.Core.Dtos.Sales;
using BusinessManagementSystemApp.Core.Models.Sales;
using BusinessManagementSystemApp.Service.Menagers;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class SalesController : ApiController
    {
        private readonly SalesManager _salesManager;
        private readonly MessagesController _messages;


        public SalesController()
        {
            _salesManager = new SalesManager();
            _messages = new MessagesController();

        }
        // GET: api/Sales
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sales/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sales
        public IHttpActionResult Post([FromBody]SalesDto dto)
        {
            try
            {
                //dto.SalesDate = "28-05-2021";
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var user = User.Identity.GetUserId();
                return Ok(_salesManager.Add(dto, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // PUT: api/Sales/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sales/5
        public void Delete(int id)
        {
        }

        [Route("api/Sales/GetVoucherNumber")]
        [HttpGet]
        public IHttpActionResult GetVoucherNumber()
        {
            try
            {
                var codeNo = _salesManager.GenerateInvoiceNo();
                return Ok(codeNo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("api/Sales/LastInvoiceNo")]
        [HttpGet]
        public IHttpActionResult LastInvoiceNo()
        {
            try
            {
                var codeNo = _salesManager.LastInvoiceNo();
                return Ok(codeNo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/Sales/GetSmsData")]
        [HttpGet]
        public IHttpActionResult GetSmsData()
        {
        
                try
                { 
                    int count = 0;
                    var clients = _salesManager.GetCowBillSms().ToList();
                    foreach (var client in clients)
                    {
                        if (!string.IsNullOrEmpty(client.PhoneNo))
                        {
                            var sms = "সম্মানিত গ্রাহক,আপনার কুরবানির " + client.CowNo + ", কুরবানির পশুর বর্তমান ওজন-" +
                                      client.Weight + " কেজি । মূল্য-" + client.Price + " টাকা, পরিবহণ ও কাঁসাই বিল-"+client.TransportCost+" বিল র্পরিশোধ-" +
                                      client.PayAmount + " টাকা, মোট বকেয়া-" + client.DueAmount + " টাকা ।" +
                                      "আগামীকাল ব্যাংক বন্ধ হওয়ার পুর্বে বকেয়া বিল পরিশোধ করার জন্য অনুরোধ করা হচ্ছে । নথ বেঙ্গল ডেইরী ফার্ম - 01754082306";

                            _salesManager.SMSSend(sms, client.PhoneNo);
                            count++;
                        }
                    }

                    return Ok(count > 0 ? 1 : 0);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
        }




    }
}
