namespace BusinessManagementSystemApp.Core.Models.MilkMamagement
{
    public class MonthlyReservation: BaseDomain
    {
        public long Id { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public int ClientInfoId { get; set; }
        public int? DayNumber { get; set; }  
        public int? HalfKg { get; set; }
        public int? SevenAndHalfGm { get; set; }
        public int? OneKg { get; set; }
    }
}