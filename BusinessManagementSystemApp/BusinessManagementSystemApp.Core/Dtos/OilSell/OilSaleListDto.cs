using System.Collections.Generic;

namespace BusinessManagementSystemApp.Core.Dtos.OilSell
{
    public class OilSaleListDto
    {
        public OilSaleListDto()
        {
            OilSaleDtos = new List<OilSellDto>();
        }
        public ICollection<OilSellDto> OilSaleDtos { get; set; }

    }
}