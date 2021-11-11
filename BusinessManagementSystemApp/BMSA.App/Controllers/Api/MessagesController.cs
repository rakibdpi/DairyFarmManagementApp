using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement;
using BusinessManagementSystemApp.Core.ViewModels.MilkMamagement;
using BusinessManagementSystemApp.Service.Menagers;
using BusinessManagementSystemApp.Service.Menagers.MilkManagement;
using BusinessManagementSystemApp.Service.Menagers.ReportManager;

namespace BMSA.App.Controllers.Api
{
    public class MessagesController : ApiController
    {
        private static  ClientInfoMenager _clientInfoManager;
        private readonly BillReportManager _billManager;
        private readonly SalesManager _cowSaleManager;

        public MessagesController()
        {
            _clientInfoManager = new ClientInfoMenager();
            _billManager= new BillReportManager();
            _cowSaleManager = new SalesManager();
        }


        int counter = 1;

        [HttpPost]
        //public void SendSMS(string mobileNo, string message, int which)
        public int SendSMS([FromBody]MessageViewModel dto) // which = 1 for admissin 2 for payment 3 for attendance
        {
            try
            {
                counter++;
                int count = 0;
                var clients = _clientInfoManager.GetAll().Where(c => c.AreaId== dto.AreaId && c.IsActive).ToList();
                foreach (var client in clients)
                {
                    if (!string.IsNullOrEmpty(dto.Message) && !string.IsNullOrEmpty(client.PhoneNo))
                    {
                    var billInfo = _billManager.GetMaster(client.Id, "October").FirstOrDefault();
                        
                        decimal amount=0;

                        if (billInfo != null)
                        {
                            amount = billInfo.SubTotal;
                        }
                        if (amount>1)
                        {
                            var split = dto.Message.Split('-');
                            var newMessage = split[0].ToString() + " " + amount + " " + split[1].ToString();
                            ++count;
                            SMSSend(newMessage, client.PhoneNo);
                        }
                    }
                }

                  return count>0 ? 1 : 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }


        [HttpPost]
        [Route("api/Messages/NormalSms")]
        //public void SendSMS(string mobileNo, string message, int which)
        public int NormalSms ([FromBody]SmsSendViewModel dto) // which = 1 for admissin 2 for payment 3 for attendance
        {
            try
            {
                string createNumberString = "";
                if (dto.SmsType == 1)
                {
                    int count = 0;
                    var clients = _clientInfoManager.GetAll().Where(c=>c.PhoneNo != null).ToList();
                    foreach (var client in clients)
                    {
                        if (!string.IsNullOrEmpty(dto.Message) && !string.IsNullOrEmpty(client.PhoneNo))
                        {
                            ++count;

                            var number = client.PhoneNo + ",";

                            createNumberString = createNumberString + number;


                        }
                    }
                    SMSSend(dto.Message, createNumberString);

                    return count > 0 ? 1 : 0;
                }

                if (dto.SmsType == 2)
                {
                    int count = 0;
                    var clients = _clientInfoManager.GetAllActive().ToList();
                    foreach (var client in clients)
                    {
                        if (!string.IsNullOrEmpty(dto.Message) && !string.IsNullOrEmpty(client.PhoneNo))
                        {
                            ++count;

                            var number = client.PhoneNo + ",";

                            createNumberString = createNumberString + number;

                           
                        }
                    }
                    SMSSend(dto.Message, createNumberString);

                    return count > 0 ? 1 : 0;
                }

                if (dto.SmsType == 3)
                {
                    int count = 0;
                    var clients = _clientInfoManager.GetAllDeActive().ToList();
                    foreach (var client in clients)
                    {
                        if (!string.IsNullOrEmpty(dto.Message) && !string.IsNullOrEmpty(client.PhoneNo))
                        {
                            ++count;
                            var number = client.PhoneNo + ",";

                            createNumberString = createNumberString + number;

                        }
                    }
                    SMSSend(dto.Message, createNumberString);

                    return count > 0 ? 1 : 0;
                }


                if (dto.SmsType == 5)
                {
                    int count = 0;
                    var clients = _clientInfoManager.GetAllActive().Where(c=>c.AreaId == dto.AreaId).ToList();
                    foreach (var client in clients)
                    {
                        if (!string.IsNullOrEmpty(dto.Message) && !string.IsNullOrEmpty(client.PhoneNo))
                        {
                            ++count;
                            var number = client.PhoneNo + ",";

                            createNumberString = createNumberString + number;
                        }

                    }

                    SMSSend(dto.Message, createNumberString);

                    return count > 0 ? 1 : 0;
                }


                if (!string.IsNullOrEmpty(dto.Message) && !string.IsNullOrEmpty(dto.PhoneNumber))
                {
                    SMSSend(dto.Message, dto.PhoneNumber);
                    return 1;
                }

                return 0;







            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


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



            //Recipient Phone Number multiple number must be separated by comma
            String message = System.Uri.EscapeUriString(messsage);

            //Create a request using a URL that can receive a post.
            WebRequest request = WebRequest.Create("http://66.45.237.70/api.php");
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            // Create POST data and convert it to a byte array.  
            string postData = "username=" + userid + "&password=" + password + "&number=" + number + "&message=" + message;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            //////Close the Stream object.

            dataStream.Close();
        }
    }
}
