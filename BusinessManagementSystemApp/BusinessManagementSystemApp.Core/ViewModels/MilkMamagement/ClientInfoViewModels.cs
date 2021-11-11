using System.ComponentModel.DataAnnotations;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;

namespace BusinessManagementSystemApp.Core.ViewModels.MilkMamagement
{
    public class ClientInfoViewModels
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }        
        public string PhoneNo { get; set; }
        public int? HalfKg { get; set; }
        public int? SevenAndHalfGm { get; set; }
        public int? OneKg { get; set; }
        public string Address { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public bool IsActive { get; set; }
        public int? DayInterval { get; set; }
    }
}