using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Models.MilkPurchases;
using BusinessManagementSystemApp.Core.ViewModels.MilkPurchase;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.MilkManagement.MilkPurchases
{
    public class MilkSupplierManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public MilkSupplierManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }


        public IEnumerable<MilkSupplierViewModel> GetAll()
        {
            return _unitOfWork.MilkSupplier.Find(c => !c.IsDelete).Select(Mapper.Map<MilkSuppliers, MilkSupplierViewModel>);
        }
    }
}