using System;
using BusinessManagementSystemApp.Core.Repositories.AsthaOnlineShop;
using BusinessManagementSystemApp.Core.Repositories.CustomerModules;
using BusinessManagementSystemApp.Core.Repositories.Ghee;
using BusinessManagementSystemApp.Core.Repositories.MilkManagement;
using BusinessManagementSystemApp.Core.Repositories.MilkSellsInterfaces;
using BusinessManagementSystemApp.Core.Repositories.MilkPurchase;
using BusinessManagementSystemApp.Core.Repositories.OilSell;
using BusinessManagementSystemApp.Core.Repositories.PaymentInterfaces;
using BusinessManagementSystemApp.Core.Repositories.ProductionInterfaces;
using BusinessManagementSystemApp.Core.Repositories.PurchaseModules;
using BusinessManagementSystemApp.Core.Repositories.SalesModules;
using BusinessManagementSystemApp.Core.Repositories.SetupModules;
using BusinessManagementSystemApp.Core.Repositories.SupplierModules;
using BusinessManagementSystemApp.Core.Repositories.MuriSell;

namespace BusinessManagementSystemApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICustomerRepository Customer { get; }
        ISupplierRepository Supplier { get; }
        IDriveLetterRepository DriveLetter { get; }
        IPurchaseDetailsRepository PurchaseDetails { get; }
        IPurchaseRepository Purchase { get; }
        ISalesRepository Sales { get; }
        ISalesDetailsRepository SalesDetails { get; }


        //Astha Online Shop

        IBusinessPartnerInfoRepository BusinessPartnerInfo { get; }

        //Milk 
        IClientInfoRepository ClientInfo { get; }
        IAreaRepository Area { get; }
        ICowSetupRepository CowSetup { get; }
        IProductionRepository Production { get; }
        IPacketTypeRepository PacketType { get; }
        IRowSaleRepository RowSale { get; }
        IPacketSaleRepository PacketSale { get; }
        IMilkSupplierRepository MilkSupplier { get; }
        IMilkPurchaseRepository MilkPurchase { get; }

        IDataTypeRepository DataType { get; }
        IDatatransectionRepository Datatransection { get; }
        IMonthlyReservationRepository MonthlyReservation { get; }
        IDueBillRepository DueBill { get; }
        IPaymentRepository Payment { get; }

        IDeliveryManRepository DeliveryMan { get; }

        IOilSellRepository OilSell { get; }

        ITransactionRepository Transaction { get; }

        IGheeSalesRepository GheeSale { get; }

        IMuriSellRepository MuriSale { get; }


        int Complete();
    }
}