using System;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;

namespace BusinessManagementSystemApp.Core.Models.MilkSells
{
    public class PacketSale:BaseDomain
    {
        public long Id { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public int ClientInfoId { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public string SalesMonth { get; set; }
        public int DayNumber { get; set; }
        public int OneKg { get; set; }
        public int HalfKg { get; set; }
        public int SevenAndHalfGm { get; set; }   
    }
}