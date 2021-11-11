using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Core.Dtos.MilkMamagement
{
    public class SendSmsDto
    {
        public int SmsType { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public int Which { get; set; }  
    }
}
