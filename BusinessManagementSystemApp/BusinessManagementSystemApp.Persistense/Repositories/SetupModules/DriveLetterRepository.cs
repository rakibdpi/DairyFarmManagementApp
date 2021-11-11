using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Core.Repositories.SetupModules;

namespace BusinessManagementSystemApp.Persistense.Repositories.SetupModules
{
    public class DriveLetterRepository : Repository<DriveLetter>,IDriveLetterRepository
    {
        public DriveLetterRepository(DbContext context) : base(context)
        {
        }
    }
}