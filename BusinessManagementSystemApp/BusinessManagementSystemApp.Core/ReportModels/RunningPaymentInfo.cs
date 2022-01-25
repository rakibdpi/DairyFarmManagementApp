using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Core.ReportModels
{
    public class RunningPaymentInfo
    {
        public string Area { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public decimal? BillAmount { get; set; }
        public decimal? PayAmount { get; set; }
        public decimal? DueAmount { get; set; }


    }
}
