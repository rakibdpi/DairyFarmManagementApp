using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.Payments;

namespace BusinessManagementSystemApp.Core.Repositories.PaymentInterfaces
{
    public interface IPaymentRepository: IRepository<Payment>
    {
        IEnumerable<Payment> GetInclude();
    }
}