using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystemApp.Core.Models.AsthaShop
{
    public class BusinessPartnerInfo : BaseDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  string Occupation { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string NidNo { get; set; }
        public string Education { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public bool? Gender { get; set; }
        //Father Information
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public bool IsActive { get; set; }

    }
}
