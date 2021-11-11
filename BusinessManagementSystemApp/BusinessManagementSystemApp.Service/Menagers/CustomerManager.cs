using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.CustomerModules;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessManagementSystemApp.Core.Models.CustomerModules;

namespace BusinessManagementSystemApp.Service.Menagers
{
    public class CustomerManager : IManager<CustomerDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public CustomerDto Get(int id)
        {
            var entity = _unitOfWork.Customer.Get(id);
            return (Mapper.Map<Customer, CustomerDto>(entity));
        }

        public IEnumerable<CustomerDto> GetAll()
        {
            return _unitOfWork.Customer.Find(c => !c.IsDelete).Select(Mapper.Map<Customer, CustomerDto>);
        }

        public int Add(CustomerDto dto, string user)
        {
            var customer = Mapper.Map<CustomerDto, Customer>(dto);
            customer.CreateBy = user;
            customer.CreateDate = DateTime.Now;
            _unitOfWork.Customer.Add(customer);

            return _unitOfWork.Complete();
        }

        public int Update(int id, CustomerDto dto, string user)
        {
            try
            {
                var customerInDb = _unitOfWork.Customer.Get(id);
                if (customerInDb == null) return 0;
                var createBy = customerInDb.CreateBy;
                var createDate = customerInDb.CreateDate;
                Mapper.Map(dto, customerInDb);
                customerInDb.CreateBy = createBy;
                customerInDb.CreateDate = createDate;
                customerInDb.UpdateBy = user;
                customerInDb.UpdateDate = DateTime.Now;
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Update Fail. Error: " + e.Message);
            }
        }

        public int AddRange(IEnumerable<CustomerDto> dtos, string user)
        {
            throw new NotImplementedException();
        }

        public int LogicalRemove(int id, string user)
        {
            try
            {
                var customerInDb = _unitOfWork.Customer.Get(id);
                if (customerInDb == null) return 0;

                customerInDb.IsDelete = true;
                customerInDb.DeleteBy = user;
                customerInDb.DeleteDate = DateTime.Now;
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Delete Fail. Error: " + e.Message);
            }
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int RemoveRange(IEnumerable<CustomerDto> dtos)
        {
            throw new NotImplementedException();
        }
    }
}
