using System;

namespace BusinessManagementSystemApp.Core.IdentityCore
{
    public class LoginHistory
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Mac { get; set; }
        public DateTime LoginDateTime { get; set; }
    }
}
