using System;
using System.Security.AccessControl;

namespace BusinessManagementSystemApp.Core.Models.AsthaShop
{
    public class TransectionData
    {
        public int Id { get; set; }
        public int DataTypeId { get; set; }
        public DataType DataType { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }
    }
}