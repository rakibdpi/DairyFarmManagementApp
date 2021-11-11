using System;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Models.MilkProduction;

namespace BusinessManagementSystemApp.Core.Dtos.MilkProductionDtos
{
    public class ProductionDto
    {
        public long Id { get; set; }

        public int CowSetupId { get; set; }
        public CowSetup CowSetup { get; set; }

        public string ProductionMonth { get; set; }

        public string Year { get; set; }

        public decimal MorningQuantity { get; set; }
        public decimal AfterNoonQuantity { get; set; }
        public decimal NightQuantity { get; set; }
        public decimal OtherTime { get; set; }

        public int DayNumber { get; set; }
    }
}