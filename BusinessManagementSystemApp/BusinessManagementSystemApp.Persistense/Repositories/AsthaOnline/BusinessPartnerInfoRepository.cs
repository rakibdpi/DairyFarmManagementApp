using BusinessManagementSystemApp.Core.Models.AsthaShop;
using BusinessManagementSystemApp.Core.Repositories.AsthaOnlineShop;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Persistense.Repositories.AsthaOnline
{
    public class BusinessPartnerInfoRepository : Repository<BusinessPartnerInfo>, IBusinessPartnerInfoRepository
    {
        public BusinessPartnerInfoRepository(DbContext context) : base(context)
        {
        }
    }
}
