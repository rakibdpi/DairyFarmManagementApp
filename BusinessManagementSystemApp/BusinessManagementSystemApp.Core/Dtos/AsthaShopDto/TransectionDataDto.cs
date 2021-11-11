using System;
using BusinessManagementSystemApp.Core.Models.AsthaShop;

namespace BusinessManagementSystemApp.Core.Dtos.AsthaShopDto
{
    public class TransectionDataDto
    {
        public int Id { get; set; }
        public int DataTypeId { get; set; }
        public DataType DataType { get; set; }
        public string DateTime { get; set; }
        public decimal Value { get; set; }

    }
}