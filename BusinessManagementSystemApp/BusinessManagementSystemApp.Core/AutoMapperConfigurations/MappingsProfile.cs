using AutoMapper;
using BusinessManagementSystemApp.Core.Dtos.AsthaShopDto;
using BusinessManagementSystemApp.Core.Dtos.CustomerModules;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Dtos.MilkProductionDtos;
using BusinessManagementSystemApp.Core.Dtos.MilkPurchase;
using BusinessManagementSystemApp.Core.Dtos.MilkSellsDtos;
using BusinessManagementSystemApp.Core.Dtos.OilSell;
using BusinessManagementSystemApp.Core.Dtos.PaymentDtos;
using BusinessManagementSystemApp.Core.Dtos.Purchase;
using BusinessManagementSystemApp.Core.Dtos.Sales;
using BusinessManagementSystemApp.Core.Dtos.SetupModules;
using BusinessManagementSystemApp.Core.Dtos.SupplierModules;
using BusinessManagementSystemApp.Core.Models.AsthaShop;
using BusinessManagementSystemApp.Core.Models.Base;
using BusinessManagementSystemApp.Core.Models.CustomerModules;
using BusinessManagementSystemApp.Core.Models.Ghee;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement.SetupModules;
using BusinessManagementSystemApp.Core.Models.MilkProduction;
using BusinessManagementSystemApp.Core.Models.MilkSells;
using BusinessManagementSystemApp.Core.Models.MilkPurchases;
using BusinessManagementSystemApp.Core.Models.OilSell;
using BusinessManagementSystemApp.Core.Models.Payments;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;
using BusinessManagementSystemApp.Core.Models.Sales;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Core.Models.SupplierModules;
using BusinessManagementSystemApp.Core.ViewModels;
using BusinessManagementSystemApp.Core.ViewModels.GheeSale;
using BusinessManagementSystemApp.Core.ViewModels.MilkPurchase;

namespace BusinessManagementSystemApp.Core.AutoMapperConfigurations
{
    public class MappingsProfile : Profile
    {
        public override string ProfileName => "MappingsProfile";

        public MappingsProfile()
        {
            //Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();      
            
            //Product
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();    
            
            //Customer
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();          
            
            //Customer
            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierDto, Supplier>();

            //Purchase
            CreateMap<Purchase, PurchaseDto>()
                .ForMember(dto => dto.PurchaseDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.DateToString(m.PurchaseDate)));

            CreateMap<PurchaseDto, Purchase>()
                .ForMember(dto => dto.PurchaseDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.StringToDate(m.PurchaseDate)));

            //Purchase Details
            CreateMap<PurchaseDetails, PurchaseDetailsDto>()
                .ForMember(dto => dto.ManufacturedDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.DateToString(m.ManufacturedDate)))
                .ForMember(dto => dto.ExpireDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.DateToString(m.ExpireDate)));

            CreateMap<PurchaseDetailsDto, PurchaseDetails>()
                .ForMember(dto => dto.ManufacturedDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.StringToDate(m.ManufacturedDate)))
                .ForMember(dto => dto.ExpireDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.StringToDate(m.ExpireDate)));


            CreateMap<Purchase, PurchaseViewModelForDataPass>()
                .ForMember(dto => dto.PurchaseDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.DateToString(m.PurchaseDate)));
            CreateMap<PurchaseViewModelForDataPass, Purchase>()
                .ForMember(dto => dto.PurchaseDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.DateToString(m.PurchaseDate)));


            //Purchase
            CreateMap<Sales, SalesDto>()
                .ForMember(dto => dto.SalesDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.DateToString(m.SalesDate)));

            CreateMap<SalesDto, Sales>()
                .ForMember(dto => dto.SalesDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.StringToDate(m.SalesDate)));


            //bUSSINESS PARTNER INFO
            CreateMap<BusinessPartnerInfo, BusinessPartnerInfoDto>();
            CreateMap<BusinessPartnerInfoDto, BusinessPartnerInfo>();


            //Milk Management

            CreateMap<ClientInfo, ClientInfoDto>();
            CreateMap<ClientInfoDto, ClientInfo>();

            CreateMap<Area, AreaDto>();
            CreateMap<AreaDto, Area>();

            CreateMap<CowSetup, CowSetupDto>();
            CreateMap<CowSetupDto, CowSetup>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<ProductionDto, Production>();
            CreateMap<Production, ProductionDto>();

            CreateMap<RowSale, RowSaleDto>();
            CreateMap<RowSaleDto, RowSale>();

            CreateMap<PacketSale, PacketSaleDto>();
            CreateMap<PacketSaleDto, PacketSale>();

            CreateMap<MilkSuppliers, MilkSupplierViewModel>();
            CreateMap<MilkSupplierViewModel, MilkSuppliers>();

            CreateMap<MilkPurchase, MilkPurchaseDto>()
                .ForMember(dto => dto.PurchaseDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.DateToString(m.PurchaseDate)));
            CreateMap<MilkPurchaseDto, MilkPurchase>()
                .ForMember(dto => dto.PurchaseDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.StringToDate(m.PurchaseDate)));

            CreateMap<MilkPurchase, MilkPurchaseViewModel>()
       .ForMember(dto => dto.PurchaseDate,
           opt => opt.MapFrom(m => DateTimeFormatter.DateToString(m.PurchaseDate)));
            CreateMap<MilkPurchaseViewModel, MilkPurchase>()
                .ForMember(dto => dto.PurchaseDate,
                    opt => opt.MapFrom(m => DateTimeFormatter.StringToDate(m.PurchaseDate)));




            //Test

            CreateMap<TransectionData, TransectionDataDto>()
                .ForMember(dto => dto.DateTime,
                    opt => opt.MapFrom(m => DateTimeFormatter.DateToString(m.DateTime)));
            CreateMap<TransectionDataDto, TransectionData>()
                .ForMember(dto => dto.DateTime,
                    opt => opt.MapFrom(m => DateTimeFormatter.StringToDate(m.DateTime)));

            CreateMap<MonthlyReservation, MonthlyReservationDto>();
            CreateMap<MonthlyReservationDto, MonthlyReservation>();

            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentDto, Payment>();

            CreateMap<OilSells, OilSellDto>();
            CreateMap<OilSellDto, OilSells>();

            CreateMap<GheeSale, GheeSaleViewModel>();
            CreateMap<GheeSaleViewModel, GheeSale>();
        }
    }
}