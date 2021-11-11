using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Repositories.MilkManagement;

namespace BusinessManagementSystemApp.Persistense.Repositories.MilkManagement
{
    public class MonthlyReservationRepository: Repository<MonthlyReservation>, IMonthlyReservationRepository
    {
        public MonthlyReservationRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<MonthlyReservation> GetInclude()
        {
            var reservation = Context.Set<MonthlyReservation>().Where(c => !c.IsDelete).Include(c => c.ClientInfo)
                .ToList();
            return reservation;
        }
    }
}