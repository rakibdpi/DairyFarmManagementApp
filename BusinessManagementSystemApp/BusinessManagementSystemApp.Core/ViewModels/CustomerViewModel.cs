using System.ComponentModel.DataAnnotations;

namespace BusinessManagementSystemApp.Core.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public string Contact { get; set; }

    }
}