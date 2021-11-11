using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.AsthaShopDto;
using BusinessManagementSystemApp.Core.Dtos.SetupModules;
using BusinessManagementSystemApp.Core.Models.AsthaShop;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.AsthaOnline
{
    public class DataTypeManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public DataTypeManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public DataTypeDto Get(int id)
        {
            var entity = _unitOfWork.DataType.Get(id);
            return (Mapper.Map<DataType, DataTypeDto>(entity));
        }

        public IEnumerable<DataTypeDto> GetAll()
        {
            return _unitOfWork.DataType.GetAll().Select(Mapper.Map<DataType, DataTypeDto>);
        }
    }
}