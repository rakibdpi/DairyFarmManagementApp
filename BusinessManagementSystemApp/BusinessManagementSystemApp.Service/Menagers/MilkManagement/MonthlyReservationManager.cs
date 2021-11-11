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
    public class MonthlyReservationManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public MonthlyReservationManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }
        public MonthlyReservationDto Get(int id)
        {
            var entity = _unitOfWork.MonthlyReservation.Get(id);
            return (Mapper.Map<MonthlyReservation, MonthlyReservationDto>(entity));
        }

        public IEnumerable<MonthlyReservationDto> GetAll()
        {
            var infos = _unitOfWork.MonthlyReservation.GetInclude().Select(Mapper.Map<MonthlyReservation, MonthlyReservationDto>).ToList();
            return infos;
        }

        public int Add(ReservationListDto dto, string user)
        {
            if (!dto.Reservations.Any()) return _unitOfWork.Complete();
            var saveStatus = 1;
            foreach (var reservation in dto.Reservations)
            {
                var model = new MonthlyReservation
                {
                    Id = reservation.Id,
                    ClientInfoId = reservation.ClientInfoId,
                    DayNumber = reservation.DayNumber,
                    HalfKg = reservation.HalfKg,
                    SevenAndHalfGm = reservation.SevenAndHalfGm,
                    OneKg = reservation.OneKg,
                    IsDelete = false,
                    CreateBy = user,
                    CreateDate = DateTime.Now
                };
                _unitOfWork.MonthlyReservation.Add(model);
                var response= _unitOfWork.Complete();
                if (response==0)
                    saveStatus = 0;
            }

            return saveStatus;
        }

        public int Update(ReservationListDto dto, string user)
        {
            if (!dto.Reservations.Any()) return _unitOfWork.Complete();
            var saveStatus = 1;
            foreach (var reservation in dto.Reservations)
            {
                var reserve = _unitOfWork.MonthlyReservation
                    .GetAll()
                    .FirstOrDefault(c => c.Id == reservation.Id);
                if (reserve == null) return 0;
                Mapper.Map(reservation, reserve);
                reserve.UpdateBy = user;
                reserve.UpdateDate = DateTime.Now;
                var response = _unitOfWork.Complete();
                if (response == 0)
                    saveStatus = 0;
            }
            return saveStatus;
        }
        public int LogicalRemove(int id, string user)
        {
            try
            {
                var clientInDb = _unitOfWork.MonthlyReservation.Get(id);
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
        

       

       
    }
}