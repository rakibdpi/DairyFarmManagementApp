using BusinessManagementSystemApp.Core.Models.DueBill;
using BusinessManagementSystemApp.Core.Repositories.MilkManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Persistense.Repositories.MilkManagement
{
    public class DueBillRepository : Repository<DueBills>, IDueBillRepository
    {
        public DueBillRepository(DbContext context) : base(context)
        {
        }
        public DueBills GetData(int clientId, string monthId)
        {
            return Context.Set<DueBills>().Where(c => c.ClientInfoId == clientId && c.MonthId == monthId)
               .Include(c => c.ClientInfo)
               .FirstOrDefault();
        }
    }
}
