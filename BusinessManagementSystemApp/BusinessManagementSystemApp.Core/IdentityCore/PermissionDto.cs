using System.Collections.Generic;

namespace BusinessManagementSystemApp.Core.IdentityCore
{
    public class PermissionDto
    {
        public IEnumerable<AspNetPermission> Permissions { get; set; }
    }
}