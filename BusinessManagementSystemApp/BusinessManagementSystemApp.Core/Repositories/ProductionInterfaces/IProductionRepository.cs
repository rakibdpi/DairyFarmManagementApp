using System.Collections.Generic;
using BusinessManagementSystemApp.Core.Models.MilkProduction;

namespace BusinessManagementSystemApp.Core.Repositories.ProductionInterfaces
{
    public interface IProductionRepository:IRepository<Production>
    {
        IEnumerable<Production> GetAllInclude();    
    }
}