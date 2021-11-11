using System;
using BusinessManagementSystemApp.Core.Models.AsthaShop;

namespace BusinessManagementSystemApp.Core.ViewModels.AsthaShop
{
    public class TransectionDataViewModel
    {
        public int Id { get; set; }
        public int DataTypeId { get; set; }
        public DataType DataType { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }
    }
}