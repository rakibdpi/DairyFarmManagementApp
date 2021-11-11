using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Repositories.MilkManagement;

namespace BusinessManagementSystemApp.Persistense.Repositories.MilkManagement
{
    public class CowSetupRepository:Repository<CowSetup>,ICowSetupRepository
    {
        public CowSetupRepository(DbContext context) : base(context)
        {
        }
    }
}