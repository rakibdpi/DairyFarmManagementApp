namespace BusinessManagementSystemApp.Core.ViewModels.MilkMamagement
{
    public class SmsSendViewModel
    {
        public int SmsType { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public int AreaId { get; set; }
    }

    public class MessageViewModel
    {
        public string Message { get; set; }
        public int AreaId { get; set; }
    }
}