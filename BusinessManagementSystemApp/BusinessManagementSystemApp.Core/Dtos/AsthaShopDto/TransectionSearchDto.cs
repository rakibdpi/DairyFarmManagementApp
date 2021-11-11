using System;
using BusinessManagementSystemApp.Core.Models.AsthaShop;

namespace BusinessManagementSystemApp.Core.Dtos.AsthaShopDto
{
    public class TransectionSearchDto
    {
        public string FormDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? DataTypeId { get; set; }
        public DataType DataType { get; set; }
    }
}