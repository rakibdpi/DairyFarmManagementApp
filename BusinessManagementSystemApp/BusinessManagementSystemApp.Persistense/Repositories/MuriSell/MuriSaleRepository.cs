using BusinessManagementSystemApp.Core.Models.Muri;
using BusinessManagementSystemApp.Core.Repositories.MuriSell;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Persistense.Repositories.MuriSell
{
    public class MuriSaleRepository : Repository<MuriSale>, IMuriSellRepository
    {
        public MuriSaleRepository(DbContext context) : base(context)
        {

        }
    }
}
