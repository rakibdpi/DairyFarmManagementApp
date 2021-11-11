using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;

namespace BusinessManagementSystemApp.Core.Repositories.MilkManagement
{
    public interface IAreaRepository : IRepository<Area>
    {
        IEnumerable<Area> LoadForeignEntities();
    }
}