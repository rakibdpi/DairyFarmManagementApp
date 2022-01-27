using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.MilkMamagement;
using BusinessManagementSystemApp.Core.Models.MilkMamagement;
using BusinessManagementSystemApp.Core.ReportModels.ViewModels;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers.MilkManagement
{
    public class ClientInfoMenager 
    {

        private readonly IUnitOfWork _unitOfWork;

        public ClientInfoMenager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }
        public int Add(ClientInfoDto dto, string user)
        {

            var clientInfo = Mapper.Map<ClientInfoDto, ClientInfo>(dto);
            var isPhoneNumberExist = IsPhoneNumberExist(dto.PhoneNo);
            if (isPhoneNumberExist)
            {
                throw new ApplicationException("Phone Number Already Exist");
            }

            clientInfo.CreateBy = user;
            clientInfo.CreateDate = DateTime.Now;
            _unitOfWork.ClientInfo.Add(clientInfo);

            return _unitOfWork.Complete();
        }


        public string GenerateClientCode(int areaId)
        {
            var areaSortName = _unitOfWork.Area.Find(c => c.Id == areaId).FirstOrDefault();
            int invNo = 0;
            var lastCode = _unitOfWork.ClientInfo.GetAll().Where(c=>c.AreaId == areaId).OrderByDescending(c => c.Id).FirstOrDefault();
            if (lastCode != null)
            {
                if (!string.IsNullOrEmpty(lastCode.Code))
                {
                    var split = lastCode.Code.Split('-');
                    invNo = Convert.ToInt32(split[1]);
                }
            }
            else
            {
                var codeNo = areaSortName.CodeNo + "-0001";
                return codeNo;
            }
            ++invNo;
            var invoiceNo = areaSortName.CodeNo + "-" + invNo.ToString().PadLeft(4, '0');
            return invoiceNo;
        }

        public string GenerateCode()
        {
            int invNo = 0;
            var lastCode = _unitOfWork.Customer.GetAll().OrderByDescending(c => c.Id).FirstOrDefault();
            if (lastCode != null)
            {
                if (!string.IsNullOrEmpty(lastCode.Code))
                {
                    var split = lastCode.Code.Split('-');
                    invNo = Convert.ToInt32(split[1]);
                }
            }
            ++invNo;
            var invoiceNo = "NBDF-"+ invNo.ToString().PadLeft(4, '0');
            return invoiceNo;
        }

        public ClientInfoDto Get(int id)
        {
            var entity = _unitOfWork.ClientInfo.Get(id);
            return (Mapper.Map<ClientInfo, ClientInfoDto>(entity));
        }

        public IEnumerable<ClientInfoDto> GetAll()
        {
            var infos = _unitOfWork.ClientInfo.GetAllInclude().Select(Mapper.Map<ClientInfo, ClientInfoDto>).ToList();
            return infos;
        }

        public IEnumerable<ClientInfoDto> GetAllActive()
        {
            return _unitOfWork.ClientInfo.GetAllInclude().Where(c => c.IsActive && !c.IsDelete).Select(Mapper.Map<ClientInfo, ClientInfoDto>);
        }
        public IEnumerable<ClientInfoDto> GetAllDeActive()
        {
            return _unitOfWork.ClientInfo.Find(c => !c.IsActive && !c.IsDelete).Select(Mapper.Map<ClientInfo, ClientInfoDto>);
        }

        public int LogicalRemove(int id, string user) 
        {
            try
            {
                var clientInDb = _unitOfWork.ClientInfo.Get(id);
                if (clientInDb == null) return 0;

                clientInDb.IsDelete = true;
                clientInDb.DeleteBy = user;
                clientInDb.DeleteDate = DateTime.Now;
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Delete Fail. Error: " + e.Message);
            }
        }

        public int Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool CodeExistCheck(string code,int id)
        {
            if (id > 0)
            {
                var result2 = _unitOfWork.ClientInfo.Find(c => c.Code == code && c.Id != id)
                    .Any();
                return result2;
            }
            var result = _unitOfWork.ClientInfo.Find(c => c.Code == code)
                .Any();
            return result;
        }

        public int Update(int id, ClientInfoDto dto, string user)
        {
            try
            {
                var clientInDb = _unitOfWork.ClientInfo.Get(id);
                if (clientInDb == null) return 0;
                var createBy = clientInDb.CreateBy;
                var createDate = clientInDb.CreateDate;
                Mapper.Map(dto, clientInDb);
                clientInDb.CreateBy = createBy;
                clientInDb.CreateDate = createDate;
                clientInDb.UpdateBy = user;
                clientInDb.UpdateDate = DateTime.Now;
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception("Data Update Fail. Error: " + e.Message);
            }
        }

        public bool IsPhoneNumberExist(string phoneNubmer)
        {
            var result = _unitOfWork.ClientInfo.Find(c => c.PhoneNo == phoneNubmer && c.PhoneNo!= null)
                .Any();
            return result;
        }

        public IEnumerable<ClientInfoVm> GetClientInfo(int areaId,int clientType)
        {
            var clients = _unitOfWork.ClientInfo.GetAllInclude().Where(c => c.AreaId == areaId).OrderBy(c=>c.Code);

            var clientList = new List<ClientInfoVm>();

            if (clientType == 1)
            {
                var activeClient = clients.Where(c => c.IsActive == true).ToList();

                foreach (var client in activeClient)
                {
                    var vm = new ClientInfoVm()
                    {
                        Name = client.Name,
                        PhoneNo = client.PhoneNo,
                        Area = client.Area.Name,
                        Address = client.Address,
                        Code = client.Code
                    };
                    clientList.Add(vm);
                }

            }
            else if (clientType == 2)
            {
                var deActiveClient = clients.Where(c => c.IsActive == false).ToList();
                foreach (var client in deActiveClient)
                {
                    var vm = new ClientInfoVm()
                    {
                        Name = client.Name,
                        PhoneNo = client.PhoneNo,
                        Area = client.Area.Name,
                        Address = client.Address,
                        Code = client.Code
                    };
                    clientList.Add(vm);
                }
            }
            else
            {
                foreach (var client in clients)
                {
                    var vm = new ClientInfoVm()
                    {
                        Name = client.Name,
                        PhoneNo = client.PhoneNo,
                        Area = client.Area.Name,
                        Address = client.Address,
                        Code = client.Code
                    };
                    clientList.Add(vm);
                }
            }

            return clientList;

        }
    }
}