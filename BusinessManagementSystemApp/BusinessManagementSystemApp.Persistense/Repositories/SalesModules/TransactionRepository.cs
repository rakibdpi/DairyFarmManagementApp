using System.Data.Entity;
using BusinessManagementSystemApp.Core.Models.Sales;
using BusinessManagementSystemApp.Core.Repositories.SalesModules;

namespace BusinessManagementSystemApp.Persistense.Repositories.SalesModules
{
    public class TransactionRepository : Repository<Transaction>,ITransactionRepository
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }
    }
}