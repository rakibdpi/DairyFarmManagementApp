using System.ComponentModel.DataAnnotations;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Models.MilkProduction;

namespace BusinessManagementSystemApp.Core.ViewModels.MilkProductionViewModels
{
    public class ProductionViewModel
    {
        public long Id { get; set; }

        public int CowSetupId { get; set; }
        public CowSetup CowSetup { get; set; }
        [Required]
        public string ProductionMonth { get; set; }
        [Required]
        public string Year { get; set; }

        public decimal MorningQuantity { get; set; }
        public decimal AfterNoonQuantity { get; set; }
        public decimal NightQuantity { get; set; }
        public decimal OtherTime { get; set; }

        public int DayNumber { get; set; }

    }
}