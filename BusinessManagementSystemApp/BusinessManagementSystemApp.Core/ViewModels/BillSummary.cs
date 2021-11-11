using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Core.ViewModels
{
    public class BillSummary
    {
        public decimal TotalBill { get; set; }
        public decimal CollectionBill { get; set; }
        public decimal DueBill { get; set; }
    }
}
