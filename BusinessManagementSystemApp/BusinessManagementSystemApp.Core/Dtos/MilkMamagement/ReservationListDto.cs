using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;

namespace BusinessManagementSystemApp.Core.Dtos.MilkMamagement
{
    public class ReservationListDto
    {
        public ReservationListDto()
        {
            Reservations= new List<MonthlyReservationDto>();
        }
        public IEnumerable<MonthlyReservationDto> Reservations { get; set; }    
    }
}