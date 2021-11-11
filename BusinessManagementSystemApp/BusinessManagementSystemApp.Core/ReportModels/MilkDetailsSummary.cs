using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Core.ReportModels
{
    public class MilkDetailsSummary
    {
        public decimal TotalMilkSale { get; set; }

        public decimal TotalMilkAmount { get; set; }

        public decimal  TotalCollection { get; set; }

        public decimal? TotalDue { get; set; }


        public decimal OneKgPacket { get; set; }

        public decimal SevenHalfKgPacket { get; set; }

        public decimal HalfKgPacket { get; set; }

        public decimal? TotalOilPrice { get; set; }

        public decimal TotalGhePrice { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal EntryDue { get; set; }

    }
}
