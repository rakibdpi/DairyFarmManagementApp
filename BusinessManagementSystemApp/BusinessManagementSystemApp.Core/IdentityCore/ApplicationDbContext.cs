using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BusinessManagementSystemApp.Core.IdentityCore
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("BusinessManagementSystemDbContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<AspNetPermission> AspNetPermissions { get; set; }

        public DbSet<AspControllerAndActionName> AspControllerAndActionNames { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
    }
}
