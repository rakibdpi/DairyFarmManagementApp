using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Core.Models.DueBill
{
    public class DueBills
    {
        public int Id { get; set; }
        public int ClientInfoId { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public string MonthId { get; set; }
        public string Year { get; set; }
        public string AmountType { get; set; }
        public decimal DueAmount { get; set; }
    }
}
