using System.ComponentModel.DataAnnotations;
using BusinessManagementSystemApp.Core.Models.SetupModules;

namespace BusinessManagementSystemApp.Core.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public decimal Weight { get; set; }

        public string Age { get; set; }

        public string Status { get; set; }

        public decimal? Price { get; set; }

        public int Type { get; set; }

    }
}