using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;

namespace BusinessManagementSystemApp.Core.Repositories.MilkManagement
{
    public interface IMonthlyReservationRepository: IRepository<MonthlyReservation>
    {
        IEnumerable<MonthlyReservation> GetInclude();
    }
}