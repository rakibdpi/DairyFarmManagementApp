using System;
using System.Collections.Generic;
using System.Linq;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers
{
    public class DriveMenager
    {
        private readonly UnitOfWork _unitOfWork;

        public DriveMenager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }

        public DriveLetter Get()
        {
            try
            {
                return _unitOfWork.DriveLetter.GetAll().FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Not Set Computer Drive");
            }
        }

        public List<DriveLetter> GetAll()
        {
            try
            {
                return _unitOfWork.DriveLetter.GetAll().ToList();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Not Set Computer Drive");
            }
        }
    }
}