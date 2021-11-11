using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.Payments;

namespace BusinessManagementSystemApp.Core.Dtos.PaymentDtos
{
    public class PaymentList
    {
        public List<PaymentDto> PaymentDtos { get; set; }   
    }
}