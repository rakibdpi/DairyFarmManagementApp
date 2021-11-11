using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.MilkProduction;
using BusinessManagementSystemApp.Core.Models.SetupModules;

namespace BusinessManagementSystemApp.Core.Dtos.MilkProductionDtos
{
    public class ProductionListDto
    {
        public ProductionListDto()
        {
            DtoList= new List<ProductionDto>();
        }
        public ICollection<ProductionDto> DtoList { get; set; }
    }
}