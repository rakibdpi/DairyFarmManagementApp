using System;
using BusinessManagementSystemApp.Core.Models.AsthaShop;

namespace BusinessManagementSystemApp.Core.ViewModels.AsthaShop
{
    public class TransectionSearchViewModel
    {
        public DateTime? FormDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int DataTypeId { get; set; }
        public DataType DataType { get; set; }
    }
}