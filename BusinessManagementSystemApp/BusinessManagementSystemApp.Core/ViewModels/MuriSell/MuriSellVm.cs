using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Core.ViewModels.MuriSell
{
    public class MuriSellVm
    {
        public long Id { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public int ClientInfoId { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public string SalesMonth { get; set; }
        public string Year { get; set; }
        public int? HalfKg { get; set; }
        public int? OneKg { get; set; }
    }
}
