using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Core.Dtos.MilkMamagement
{
    public class SmsTemplateDto
    {
        public string From = "InfoSMS";
        public string To { get; set; }
        public string Text { get; set; }
    }
}
