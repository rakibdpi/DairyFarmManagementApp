using System.Collections.Generic;
using BusinessManagementSystemApp.Core.IdentityCore;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.IdentityModules
{
    public class UserManager
    {
        private readonly BusinessManagementSystemDbContext _dbContext;

        public UserManager()
        {
            _dbContext = new BusinessManagementSystemDbContext();
        }

        public IEnumerable<ApplicationUser> GetAllUser()
        {
            return (IEnumerable<ApplicationUser>)_dbContext.Users;
        }
    }
}