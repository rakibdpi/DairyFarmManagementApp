namespace BusinessManagementSystemApp.Core.IdentityCore
{
    public class PermissionViewModel
    {
        public ApplicationRole Role { get; set; }
        public int RoleId { get; set; }

        public string ControllerAndActionName { get; set; }

    }


}