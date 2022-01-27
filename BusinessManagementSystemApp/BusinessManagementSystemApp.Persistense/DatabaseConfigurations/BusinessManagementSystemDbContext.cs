using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManagementSystemApp.Core.IdentityCore;
using BusinessManagementSystemApp.Core.Models.AsthaShop;
using BusinessManagementSystemApp.Core.Models.CustomerModules;
using BusinessManagementSystemApp.Core.Models.DueBill;
using BusinessManagementSystemApp.Core.Models.Ghee;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Models.MilkProduction;
using BusinessManagementSystemApp.Core.Models.MilkPurchases;
using BusinessManagementSystemApp.Core.Models.MilkSells;
using BusinessManagementSystemApp.Core.Models.Muri;
using BusinessManagementSystemApp.Core.Models.OilSell;
using BusinessManagementSystemApp.Core.Models.Payments;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;
using BusinessManagementSystemApp.Core.Models.Sales;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Core.Models.SupplierModules;

namespace BusinessManagementSystemApp.Persistense.DatabaseConfigurations
{
    public class BusinessManagementSystemDbContext : ApplicationDbContext
    {
        public BusinessManagementSystemDbContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        //Setup Modules

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<DriveLetter> DriveLetters { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetails> PurchaseDetail { get; set; }


        //Astha Online Shop
        public DbSet<BusinessPartnerInfo> BusinessPartnerInfos { get; set; }

        //MilkManament
        public DbSet<ClientInfo> ClientInfos { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<CowSetup> CowSetups { get; set; }
        public DbSet<Production> Productions { get; set; }  
        public DbSet<MilkSuppliers> MilkSuppliers { get; set; }
        public DbSet<MilkPurchase> MilkPurchases { get; set; }
        public DbSet<PacketSale> PacketSales { get; set; }  
        public DbSet<RowSale> RowSales { get; set; }


        public DbSet<DataType> DataTypes { get; set; }
        public DbSet<TransectionData> TransectionData { get; set; }
        public DbSet<MonthlyReservation> MonthlyReservations { get; set; }
        public DbSet<DueBills> DueBills { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<DeliveryMan> DeliveryMen { get; set; }

        public DbSet<OilSells> OilSells { get; set; }

        //Sales
        public DbSet<Sales> Sales { get; set; }

        public DbSet<SalesDetails> SalesDetails { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<GheeSale> GheeSales { get; set; }

        public DbSet<MuriSale> MuriSales { get; set; }

    }
}
