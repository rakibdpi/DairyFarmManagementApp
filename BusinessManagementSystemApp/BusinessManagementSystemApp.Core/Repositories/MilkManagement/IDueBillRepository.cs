using BusinessManagementSystemApp.Core.Models.DueBill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Core.Repositories.MilkManagement
{
    public interface IDueBillRepository : IRepository<DueBills>
    {
        DueBills GetData(int clientId,string monthId);
    }
}
