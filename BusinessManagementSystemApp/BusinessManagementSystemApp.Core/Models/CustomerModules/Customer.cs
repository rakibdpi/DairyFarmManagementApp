using System.ComponentModel.DataAnnotations;

namespace BusinessManagementSystemApp.Core.Models.CustomerModules
{
    public class Customer : BaseDomain
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        public string Contact { get; set; }
    }
}