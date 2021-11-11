namespace BusinessManagementSystemApp.Core.IdentityCore
{
    public class AspNetPermission
    {
        public int Id { get; set; }
        public string ControllerAndActionName { get; set; }
        public string RoleId { get; set; }
    }
}