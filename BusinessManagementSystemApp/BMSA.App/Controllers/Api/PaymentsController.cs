using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;
using BMSA.App.ViewModels;
using BusinessManagementSystemApp.Core.Dtos.PaymentDtos;
using BusinessManagementSystemApp.Core.Models.DueBill;
using BusinessManagementSystemApp.Core.Models.Payments;
using BusinessManagementSystemApp.Core.ReportModels.ViewModels;
using BusinessManagementSystemApp.Core.ViewModels;
using BusinessManagementSystemApp.Service.Menagers;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;
using BusinessManagementSystemApp.Service.Menagers.ReportManager;
using Microsoft.AspNet.Identity;

namespace BMSA.App.Controllers.Api
{
    public class PaymentsController : ApiController
    {
        private readonly PaymentService _paymentService;
        private readonly ClientInfoMenager _clientMenager;
        private readonly MessagesController _messagesController;
        private readonly BillReportManager _billReport;

        public PaymentsController()
        {
            _paymentService= new PaymentService();
            _clientMenager= new ClientInfoMenager();
            _messagesController= new MessagesController();
            _billReport= new BillReportManager();
        }

        [HttpGet]
        [Route("Payments/GetClients")]
        public IHttpActionResult GetClients(int areaId, string year, string month, int? clientId)
        {
            var resultList = new List<ClientListForPayment>();
            if (clientId > 0)
            {

                var billAlreadyPaid = _paymentService.GetAll()
                    .Where(c => c.Year == year && c.Month == month && c.AreaId == areaId).Select(c => new ClientListForPayment
                    {
                        Id = c.ClientInfoId,
                        AreaId = c.AreaId,
                        AreaName = c.Area.Name,
                        Code = c.ClientInfo.Code,
                        Name = c.ClientInfo.Name,
                        PhoneNo = c.ClientInfo.PhoneNo,
                        Address = c.ClientInfo.Address,
                        BillAmount = _billReport.BillForView(c.ClientInfoId, month,year)
                    }).Distinct().ToList();
                var clientList = _clientMenager.GetAllActive().Where(c => c.AreaId == areaId && c.Id == clientId.GetValueOrDefault()).Select(c => new ClientListForPayment
                {
                    Id = c.Id,
                    AreaId = c.AreaId,
                    AreaName = c.Area.Name,
                    Code = c.Code,
                    Name = c.Name,
                    PhoneNo = c.PhoneNo,
                    Address = c.Address,
                    BillAmount = _billReport.BillForView(c.Id, month,year)
                }).Distinct().ToList();

                foreach (var client in clientList)
                {
                    var any = billAlreadyPaid.Any(c => c.Id == client.Id);
                    if (!any)
                    {
                        resultList.Add(client);
                    }
                }
                return Ok(resultList.OrderBy(c => c.Code));
            }

            var paidList = _paymentService.GetAll()
                .Where(c => c.Year == year && c.Month == month && c.AreaId == areaId).Select(c => new ClientListForPayment
                {
                    Id = c.ClientInfoId,
                    AreaId = c.AreaId,
                    AreaName = c.Area.Name,
                    Code = c.ClientInfo.Code,
                    Name = c.ClientInfo.Name,
                    PhoneNo = c.ClientInfo.PhoneNo,
                    Address = c.ClientInfo.Address,
                    BillAmount = _billReport.BillForView(c.ClientInfoId, month,year)
                }).Distinct().ToList();
            var allClients = _clientMenager.GetAllActive().Where(c => c.AreaId == areaId).Select(c => new ClientListForPayment
            {
                Id = c.Id,
                AreaId = c.AreaId,
                AreaName = c.Area.Name,
                Code = c.Code,
                Name = c.Name,
                PhoneNo = c.PhoneNo,
                Address = c.Address,
                BillAmount = _billReport.BillForView(c.Id, month,year)
            }).Distinct().ToList();

            foreach (var client in allClients)
            {
                var any = paidList.Any(c => c.Id == client.Id);
                if (!any && client.BillAmount > 0 )
                {
                    resultList.Add(client);
                }
            }

            return Ok(resultList.OrderBy(c => c.Code));
        }


        //Due Sms
        [HttpPost]
        [Route("Payments/DueBillSms")]
        public int DueBillSms([FromBody]DueBillsViewModel dto)
        {
          //  int count = 0;
            var resultList = new List<ClientListForPayment>();

            var paidList = _paymentService.GetAll()
                .Where(c => c.Year == dto.Year && c.Month == dto.Month && c.AreaId == dto.AreaId).Select(c => new ClientListForPayment
                {
                    Id = c.ClientInfoId,
                    AreaId = c.AreaId,
                    AreaName = c.Area.Name,
                    Code = c.ClientInfo.Code,
                    Name = c.ClientInfo.Name,
                    PhoneNo = c.ClientInfo.PhoneNo,
                    Address = c.ClientInfo.Address,
                    BillAmount = _billReport.BillForView(c.ClientInfoId, dto.Month,dto.Year)
                }).Distinct().ToList();
            var allClients = _clientMenager.GetAllActive().Where(c => c.AreaId == dto.AreaId).Select(c => new ClientListForPayment
            {
                Id = c.Id,
                AreaId = c.AreaId,
                AreaName = c.Area.Name,
                Code = c.Code,
                Name = c.Name,
                PhoneNo = c.PhoneNo,
                Address = c.Address,
                BillAmount = _billReport.BillForView(c.Id, dto.Month,dto.Year)
            }).Distinct().ToList();

            foreach (var client in allClients)
            {
                var any = paidList.Any(c => c.Id == client.Id);
                if (!any && client.BillAmount > 0 && !string.IsNullOrEmpty(client.PhoneNo) )
                {
                    var message = "সম্মানিত গ্রাহক,আপনার ডিসেম্বর মাসের দুধের বিল " + client.BillAmount + " টাকা ১০ তারিখের মধ্যে আশা করেছিলাম।দ্রুত পরিশোধ করার জন্য অনুরোধ করা যাচ্ছে।ঠাকুরগাঁও ডেইরী ফার্ম, ফোনঃ 01760123281";

                   // var message = "আপনার ফেব্রুয়ারী মাসের দুধের বিল " + client.BillAmount + " টাকা দ্রুত পরিশোধ করার জন্য অনুরোধ রইল-নর্থ বেঙ্গল ডেইরী ফার্ম,যোগাযোগঃ 01748095352";


                    _messagesController.SMSSend(message, client.PhoneNo);
                }
            }

            return 1;
        }



        ////Area Wise Bill


        [HttpGet]
        [Route("Payments/GetBillInfo")]
        public IHttpActionResult GetBillInfo(int areaId, string year, string month)
        {

            var resultList = new List<ClientListForPayment>();


            var paidList = _paymentService.GetAll()
                .Where(c => c.Year == year && c.Month == month && c.AreaId == areaId).Select(c => new ClientListForPayment
                {
                    Id = c.ClientInfoId,
                    AreaId = c.AreaId,
                    AreaName = c.Area.Name,
                    Code = c.ClientInfo.Code,
                    Name = c.ClientInfo.Name,
                    PhoneNo = c.ClientInfo.PhoneNo,
                    Address = c.ClientInfo.Address,
                    BillAmount = _billReport.BillForView(c.ClientInfoId, month,year)
                }).Distinct().ToList();

            var allClients = _clientMenager.GetAllActive().Where(c => c.AreaId == areaId).Select(c => new ClientListForPayment
            {
                Id = c.Id,
                AreaId = c.AreaId,
                AreaName = c.Area.Name,
                Code = c.Code,
                Name = c.Name,
                PhoneNo = c.PhoneNo,
                Address = c.Address,
                BillAmount = _billReport.BillForView(c.Id, month,year)
            }).Distinct().ToList();

            foreach (var client in allClients)
            {
                var any = paidList.Any(c => c.Id == client.Id);
                if (!any && client.BillAmount > 0)
                {
                    resultList.Add(client);
                }
            }


            var totalBill = allClients.Sum(c => c.BillAmount);
            var collectionBill = paidList.Sum(c => c.BillAmount);
            var dueBill = totalBill - collectionBill;


            var billSummary = new BillSummary()
            {
                TotalBill =Convert.ToDecimal(totalBill),
                CollectionBill = Convert.ToDecimal(collectionBill),
                DueBill = Convert.ToDecimal(dueBill)
            };

            return Ok(billSummary);
        }

        // POST: api/Areas
        public IHttpActionResult Post([FromBody]PaymentList dto)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return BadRequest("Input Value Not Valid");

                var list = dto.PaymentDtos.Select(x => new Payment
                {
                    AreaId = x.AreaId,
                    ClientInfoId = x.ClientInfoId,
                    Year = x.Year,
                    Month = x.Month,
                    BillAmount = x.BillAmount,
                    CreateBy = User.Identity.GetUserName(),
                    CreateDate = DateTime.Now
                }).Distinct().ToList();
                var status = _paymentService.Add(list);
                if (status <= 0) return Ok(status);
                foreach (var payment in list)
                {
                    var message = "আপনার ডিসেম্বর মাসের দুধের বিল " + payment.BillAmount + " টাকা বুঝে পেলাম ,ধন্যবাদ । ঠাকুরগাঁও ডেইরী ফার্ম, ফোনঃ 01760123281";
                    var contactNo = _clientMenager.Get(payment.ClientInfoId).PhoneNo;
                    if (contactNo.Length == 11)
                    {
                        _messagesController.SMSSend(message, contactNo);
                    }
                }

                var totalCollect = list.Sum(c => c.BillAmount);

                return Ok(totalCollect);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
