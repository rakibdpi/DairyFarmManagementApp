using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Core.ReportModels.ViewModels
{
    public class DueBillVm
    {
        public int AreaId { get; set; }

        public string Year { get; set; }

        public string Month { get; set; }

        public int? ClientId { get; set; }
    }
}
