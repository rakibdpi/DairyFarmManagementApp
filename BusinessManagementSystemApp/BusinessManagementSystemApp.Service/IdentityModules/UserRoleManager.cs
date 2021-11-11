using System.Collections.Generic;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BusinessManagementSystemApp.Service.IdentityModules
{
    public class UserRoleManager
    {
        private readonly BusinessManagementSystemDbContext _context;

        public UserRoleManager()
        {
            _context = new BusinessManagementSystemDbContext();
        }

        public IEnumerable<IdentityRole> GetAll()
        {
            return _context.Roles;
        }
    }
}