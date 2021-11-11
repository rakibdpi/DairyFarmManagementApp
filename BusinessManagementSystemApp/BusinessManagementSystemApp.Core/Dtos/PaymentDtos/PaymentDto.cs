using System.ComponentModel.DataAnnotations;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;

namespace BusinessManagementSystemApp.Core.Dtos.PaymentDtos
{
    public class PaymentDto
    {
        public long Id { get; set; }
        [Required]
        public string Month { get; set; }
        [Required]
        public string Year { get; set; }
        public Area Area { get; set; }
        public int AreaId { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public int ClientInfoId { get; set; }
        public decimal BillAmount { get; set; }
    }
}