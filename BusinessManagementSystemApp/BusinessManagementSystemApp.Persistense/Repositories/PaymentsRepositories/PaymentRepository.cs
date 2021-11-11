using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.Payments;
using BusinessManagementSystemApp.Core.Repositories.PaymentInterfaces;

namespace BusinessManagementSystemApp.Persistense.Repositories.PaymentsRepositories
{
    public class PaymentRepository: Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Payment> GetInclude()
        {
            return Context.Set<Payment>().Where(c => !c.IsDelete).
                Include(c => c.Area).
                Include(c => c.ClientInfo).
                ToList();
        }
    }
}