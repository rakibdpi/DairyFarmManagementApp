using System.Data.Entity;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Models.AsthaShop;
using BusinessManagementSystemApp.Core.Repositories.AsthaOnlineShop;
using BusinessManagementSystemApp.Core.Repositories.CustomerModules;
using BusinessManagementSystemApp.Core.Repositories.Ghee;
using BusinessManagementSystemApp.Core.Repositories.MilkManagement;
using BusinessManagementSystemApp.Core.Repositories.MilkPurchase;
using BusinessManagementSystemApp.Core.Repositories.MilkSellsInterfaces;
using BusinessManagementSystemApp.Core.Repositories.OilSell;
using BusinessManagementSystemApp.Core.Repositories.PaymentInterfaces;
using BusinessManagementSystemApp.Core.Repositories.ProductionInterfaces;
using BusinessManagementSystemApp.Core.Repositories.PurchaseModules;
using BusinessManagementSystemApp.Core.Repositories.SalesModules;
using BusinessManagementSystemApp.Core.Repositories.SetupModules;
using BusinessManagementSystemApp.Core.Repositories.SupplierModules;
using BusinessManagementSystemApp.Persistense.Repositories.AsthaOnline;
using BusinessManagementSystemApp.Persistense.Repositories.CustomerModules;
using BusinessManagementSystemApp.Persistense.Repositories.Ghee;
using BusinessManagementSystemApp.Persistense.Repositories.MilkManagement;
using BusinessManagementSystemApp.Persistense.Repositories.MilkPurchaseRepositories;
using BusinessManagementSystemApp.Persistense.Repositories.MilkSellsRepositories;
using BusinessManagementSystemApp.Persistense.Repositories.OilSell;
using BusinessManagementSystemApp.Persistense.Repositories.PaymentsRepositories;
using BusinessManagementSystemApp.Persistense.Repositories.ProductionRepositories;
using BusinessManagementSystemApp.Persistense.Repositories.PurchaseModules;
using BusinessManagementSystemApp.Persistense.Repositories.SalesModules;
using BusinessManagementSystemApp.Persistense.Repositories.SetupModules;
using BusinessManagementSystemApp.Persistense.Repositories.SupplierRepository;

namespace BusinessManagementSystemApp.Persistense
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public ICategoryRepository Category { get; }
        public IProductRepository Product { get; }
        public ISupplierRepository Supplier { get; }
        public IDriveLetterRepository DriveLetter { get; }
        public IPurchaseDetailsRepository PurchaseDetails { get; }
        public IPurchaseRepository Purchase { get; }
        public ISalesRepository Sales { get; }
        public ISalesDetailsRepository SalesDetails { get; }

        public  ICustomerRepository Customer { get; }

        public IBusinessPartnerInfoRepository BusinessPartnerInfo { get; }

        public IClientInfoRepository ClientInfo { get; }

        public IAreaRepository Area { get; }
        public ICowSetupRepository CowSetup { get; }

        public IProductionRepository Production { get; }
        public IPacketTypeRepository PacketType { get; }
        public IRowSaleRepository RowSale { get; }
        public IPacketSaleRepository PacketSale { get; }
        public IMilkSupplierRepository MilkSupplier { get; }
        public IMilkPurchaseRepository MilkPurchase { get; }
        public IDatatransectionRepository Datatransection { get; }
        public IMonthlyReservationRepository MonthlyReservation { get; }
        public IDataTypeRepository DataType { get; }
        public IDueBillRepository DueBill { get; }
        public IPaymentRepository Payment { get; }
        public IDeliveryManRepository DeliveryMan { get; }
        public IOilSellRepository OilSell { get; }
        public ITransactionRepository Transaction { get; }
        public IGheeSalesRepository GheeSale { get; }



        public UnitOfWork(DbContext context)
        {
            _context = context;

            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
            DriveLetter = new DriveLetterRepository(_context);
            Customer = new CustomerRepository(_context);
            Supplier = new SupplierRepository(_context);
            Purchase = new PurchaseRepository(_context);
            PurchaseDetails = new PurchaseDetailsRepository(_context);
            Sales = new SalesRepository(_context);
            SalesDetails = new SalesDetailsRepository(_context);

            BusinessPartnerInfo = new BusinessPartnerInfoRepository(_context);

            // milk management

            ClientInfo = new ClientInfoRepository(_context);
            Area = new AreaRepository(_context);
            CowSetup= new CowSetupRepository(_context);
            Production= new ProductionRepository(_context);
            PacketType= new PacketTypeRepository(_context);
            RowSale= new RowSaleRepository(_context);
            PacketSale= new PacketSaleRepository(_context);
            MilkSupplier= new MilkSupplierRepository(_context);
            MilkPurchase = new MilkPurchaseRepository(_context);
            Datatransection = new DatatransectionRepository(_context);
            DataType = new DataTypeRepository(_context);
            MonthlyReservation= new MonthlyReservationRepository(_context);
            DueBill = new DueBillRepository(_context);
            Payment= new PaymentRepository(_context);
            DeliveryMan= new DeliveryManRepository(_context);
            OilSell = new OilSellRepository(_context);
            Transaction = new TransactionRepository(_context);
            GheeSale = new GheeSalesRepository(_context);
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}