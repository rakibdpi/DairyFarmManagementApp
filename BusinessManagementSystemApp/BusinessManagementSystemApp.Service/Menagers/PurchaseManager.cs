using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessManagementSystemApp.Core;
using BusinessManagementSystemApp.Core.Dtos.Purchase;
using BusinessManagementSystemApp.Core.Dtos.SetupModules;
using BusinessManagementSystemApp.Core.Models.PurchaseModules;
using BusinessManagementSystemApp.Core.Models.SetupModules;
using BusinessManagementSystemApp.Persistense;
using BusinessManagementSystemApp.Persistense.DatabaseConfigurations;

namespace BusinessManagementSystemApp.Service.Menagers
{
    public class PurchaseManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseManager()
        {
            _unitOfWork = new UnitOfWork(new BusinessManagementSystemDbContext());
        }


        public PurchaseDetailsDto GetPreviousPriceProduct(int productId)
        {
            var productInfo = _unitOfWork.PurchaseDetails.Find(c => c.ProductId == productId).LastOrDefault();
            return (Mapper.Map<PurchaseDetails, PurchaseDetailsDto>(productInfo));

        }

        public int Save(Purchase exam)
        {
            var isBillOrInvoiceNoExist = IsBillNumberExist(exam.BillOrInvoiceNo);
            if (isBillOrInvoiceNoExist)
            {
                throw new ApplicationException("This BillOrInvoice Number Already Exist");
            }
            exam.CreateBy = "Admin";
            exam.CreateDate = DateTime.Now;
            _unitOfWork.Purchase.Add(exam);
            return _unitOfWork.Complete();
        }

        public bool IsBillNumberExist(string billNumber)
        {
            var result = _unitOfWork.Purchase.Find(c => c.BillOrInvoiceNo == billNumber)
                .Any();
            return result;
        }
    }
}