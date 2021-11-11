using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.Sales;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;
using BusinessManagementSystemApp.Core.Models.Sales;
using BusinessManagementSystemApp.Core.ReportModels;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers
{
    public class SalesManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalesManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }


        public int Add(SalesDto dto, string user)
        {
            var level = 0;
            int salesId = 0;
            try
            {
                var status = true;
                var entity = Mapper.Map<SalesDto, Sales>(dto);
                entity.CreateBy = user;
                entity.CreateDate = DateTime.Now;

                _unitOfWork.Sales.Add(entity);
                status = _unitOfWork.Complete() > 0;
                level = 1;
                salesId = entity.Id;

                _unitOfWork.Transaction.Add(new Transaction()
                {
                    InvoiceNo = dto.InvoiceNo,
                    CustomerId = dto.CustomerId,
                    TransactionDate = DateTime.Now,
                    PayAmount = dto.PayAmount,
                    ReferenceTableId = salesId
                });

                status = _unitOfWork.Complete() > 0;
                level = 2;
                return status ? entity.Id : 0;
            }
            catch (Exception e)
            {
                if (level == 1)
                {
                    var sales = _unitOfWork.Sales.Get(salesId);
                    var sd = _unitOfWork.SalesDetails.Find(c => c.SalesId == salesId).ToList();
                    _unitOfWork.SalesDetails.RemoveRange(sd);
                    _unitOfWork.Sales.Remove(sales);
                    _unitOfWork.Complete();
                }
                else if (level == 2)
                {
                    var stock = _unitOfWork.Transaction.Find(c => c.ReferenceTableId == salesId).ToList();
                    _unitOfWork.Transaction.RemoveRange(stock);

                    var sales = _unitOfWork.Sales.Get(salesId);
                    var sd = _unitOfWork.SalesDetails.Find(c => c.SalesId == salesId).ToList();
                    _unitOfWork.SalesDetails.RemoveRange(sd);
                    _unitOfWork.Sales.Remove(sales);
                    _unitOfWork.Complete();

                }

                throw new Exception(e.Message);
            }
        }




        public string GenerateInvoiceNo()
        {
            int codeNo = 0;

            var list = _unitOfWork.Sales.GetAllInclude().OrderByDescending(c => c.Id)
                .FirstOrDefault();

            if (list == null)
            {
                var code = "INVOICE-" + "0001";
                return code;
            }

            {
                string[] parts = list.InvoiceNo.Split('-');
                codeNo = Convert.ToInt32(parts[1]);
            }

            var finalCode = "INVOICE-" + (codeNo + 1).ToString().PadLeft(6, '0');
            return finalCode;
        }

        public string LastInvoiceNo()
        {
            var data = _unitOfWork.Sales.GetAll().OrderByDescending(c => c.Id).Take(1).Select(c => c.InvoiceNo).ToList()
                .FirstOrDefault();
            return data;

        }

        public int GetSaleId(string invoiceNo)
        {
            var sales = _unitOfWork.Sales.SingleOrDefault(c => c.InvoiceNo == invoiceNo);

            return sales.Id;
        }


        public List<SalesDetailsReport> GetSaleDetail(int saleId)
        {
            var salesData = _unitOfWork.SalesDetails.GetAllInclude().Where(c => c.SalesId == saleId).ToList();

            var list = new List<SalesDetailsReport>();

            foreach (var s in salesData)
            {
                var l = new SalesDetailsReport()
                {
                    ProductCode = s.Product.Code,
                    Color = s.Product.Color,
                    Age = s.Product.Age,
                    Weight = s.Weight,
                    Price = s.UnitPrice
                };
                list.Add(l);
            }
            return list;
        }


        public List<CowSaleReport> GetSale(string invoiceNo)
        {
            var sale = _unitOfWork.Sales.GetAllInclude().SingleOrDefault(c => c.InvoiceNo == invoiceNo);

            var transaction = _unitOfWork.Transaction.GetAll().SingleOrDefault(c => c.ReferenceTableId == sale.Id);

            var cowsales = new List<CowSaleReport>();


            var cowsale = new CowSaleReport()
            {
                CustomerName = sale.Customer.Name,
                CustomerId = sale.Customer.Code,
                PhoneNo = sale.Customer.Contact,
                Address = sale.Customer.Address,
                InvoiceNo = sale.InvoiceNo,
                TotalBill = sale.TotalBill,
                TransportBill = sale.TransportCost,
                Payable = sale.Payable,
                Payment = transaction.PayAmount,
                Due = sale.Payable - transaction.PayAmount
            };
            cowsales.Add(cowsale);

            return cowsales;
        }


        public IEnumerable<CowSellBillSms> GetCowBillSms()
        {
            var cowSellList = new List<CowSellBillSms>();

            var animal = "";
            var animalWeight = ""; 

            var saleInfo = _unitOfWork.Sales.GetAllInclude().ToList();


            foreach (var sale in saleInfo)
            {
                var singalCustomer = new CowSellBillSms();

                var saleDetails = _unitOfWork.SalesDetails.GetAllInclude().Where(c => c.SalesId == sale.Id).ToList();
                var i = 0;
                foreach (var s in saleDetails)
                {

                    if (saleDetails.Count > 1)
                    {
                        var number = "";
                        var weight = "";
                        if (i == 0)
                        {
                            number = s.Product.Code;
                            weight = Convert.ToString(s.Weight);
                        }
                        else
                        {
                            number = " ও " + s.Product.Code;
                            weight = " ও " + Convert.ToString(s.Weight);
                        }
                        animal = animal + number;

                        animalWeight = animalWeight + weight;


                    }
                    else
                    {
                        animal = s.Product.Code;
                        animalWeight = Convert.ToString(s.Weight);
                    }

                    i++;
                }

                singalCustomer.CowNo = animal;
                singalCustomer.Weight = animalWeight;
                singalCustomer.CustomerName = sale.Customer.Name;
                singalCustomer.PhoneNo = sale.Customer.Contact;
                singalCustomer.Price = sale.TotalBill;
                var totalPayment = _unitOfWork.Transaction.Find(c => c.CustomerId == sale.CustomerId).ToList()
                    .Sum(c => c.PayAmount);
                singalCustomer.PayAmount = totalPayment;
                singalCustomer.DueAmount = sale.TotalBill + sale.TransportCost.GetValueOrDefault() - totalPayment;
                singalCustomer.TransportCost = sale.TransportCost.GetValueOrDefault();
                cowSellList.Add(singalCustomer);

                 animal = "";
                 animalWeight = "";


            }
            return cowSellList;
        }


        public void SMSSend(string messsage, string number)
        {

            //Test

            //44516094907044961609490704

            //var apikey = "44516094907044961609490704"; //Your Login ID
            //var senderId = "8801844532630";

            //String message = System.Uri.EscapeUriString(messsage);

            //// Create a request using a URL that can receive a post.   
            //WebRequest request = WebRequest.Create("http://sms.iglweb.com/api/v1/send");
            //// Set the Method property of the request to POST.  
            //request.Method = "POST";
            //// Create POST data and convert it to a byte array.  
            //string postData = "api_key=" + apikey + "&contacts=" + number + "&senderid=" + senderId + "&msg=" + message;
            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //// Set the ContentType property of the WebRequest.  
            //request.ContentType = "application/x-www-form-urlencoded";
            //// Set the ContentLength property of the WebRequest.  
            //request.ContentLength = byteArray.Length;
            //// Get the request stream.  
            //Stream dataStream = request.GetRequestStream();
            //// Write the data to the request stream.  
            //dataStream.Write(byteArray, 0, byteArray.Length);





            ////Previous

            //String userid = "01768890336"; //Your Login ID
            //String password = "Admin555@#%"; //Your Password


            String userid = "01797803454"; //Your Login ID
            String password = "Rakib1234@"; //Your Password



            //                          //Recipient Phone Number multiple number must be separated by comma
            String message = System.Uri.EscapeUriString(messsage);

            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create("http://66.45.237.70/api.php");
            //// Set the Method property of the request to POST.  
            request.Method = "POST";
            //// Create POST data and convert it to a byte array.  
            string postData = "username=" + userid + "&password=" + password + "&number=" + number + "&message=" + message;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //// Set the ContentType property of the WebRequest.  
            request.ContentType = "application/x-www-form-urlencoded";
            //// Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            //// Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            //// Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object. 

            dataStream.Close();
        }
    }
}