using System.Collections.Generic;

namespace BusinessManagementSystemApp.Core.Repositories.MilkPurchase
{
    public interface IMilkPurchaseRepository : IRepository<Models.MilkPurchases.MilkPurchase>
    {
        IEnumerable<Models.MilkPurchases.MilkPurchase> GetAllInclude(int supplierId);

    }
}