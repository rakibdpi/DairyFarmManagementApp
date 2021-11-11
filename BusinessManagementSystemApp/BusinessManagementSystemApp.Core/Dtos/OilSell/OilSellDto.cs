using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using System;

namespace BusinessManagementSystemApp.Core.Dtos.OilSell
{
    public class OilSellDto
    {
        public long Id { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public int ClientInfoId { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public string SalesMonth { get; set; }
        public string Year { get; set; }
        public int? DayNumber { get; set; }
        public int? OneKg { get; set; }
        public int? TwoKg { get; set; }
        public int? FiveKg { get; set; }

        public bool IsDelete { get; set; }

        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}