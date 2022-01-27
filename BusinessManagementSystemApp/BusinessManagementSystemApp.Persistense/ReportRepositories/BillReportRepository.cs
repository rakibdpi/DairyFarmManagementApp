using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.ReportModels;
using BusinessManagementSystemApp.Core.ReportModels.OilReport;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Persistense.ReportRepositories
{
    public class BillReportRepository
    {
        private static BusinessManagementSystemDbContext _context;

        public BillReportRepository()
        {
            _context= new BusinessManagementSystemDbContext();
        }

        public IEnumerable<BilReportMaster> GetMasterInfo(int clientId, string month)
        {
            var query = "SP_GetBillReportMaster @clientId, @month";
            var data = _context.Database.SqlQuery<BilReportMaster>(query,
                new SqlParameter("clientId", clientId),
                new SqlParameter("month", month)
            );

            return data.ToList();
        }
        public IEnumerable<BillReport> GetBillInfo(int clientId, string month)
        {
            var query = "SP_GetBillReport @clientId, @month";
            var data = _context.Database.SqlQuery<BillReport>(query,
                new SqlParameter("clientId", clientId),
                new SqlParameter("month", month)
            );
            
            return data.ToList();
        }

        public IEnumerable<OilBillReport> GetOilBillInfo(int clientId, string month)
        {
            var query = "SP_GetOilBillReport @clientId, @month";
            var data = _context.Database.SqlQuery<OilBillReport>(query,
                new SqlParameter("clientId", clientId),
                new SqlParameter("month", month)
            );

            return data.ToList();
        }



    }
}