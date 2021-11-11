using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Dtos.MilkSellsDtos;

namespace BusinessManagementSystemApp.Core.Dtos.Sales
{
    public class PacketSaleListDto
    {
        public PacketSaleListDto()
        {
            PacketSaleDtos= new List<PacketSaleDto>();
        }
        public ICollection<PacketSaleDto> PacketSaleDtos { get; set; }  
    }
}