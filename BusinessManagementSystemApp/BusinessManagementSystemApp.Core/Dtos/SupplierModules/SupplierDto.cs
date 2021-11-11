using System.ComponentModel.DataAnnotations;

namespace BusinessManagementSystemApp.Core.Dtos.SupplierModules
{
    public class SupplierDto
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string Contact { get; set; }
        public string ContactPerson { get; set; }
        public string ImagePath { get; set; }
    }
}