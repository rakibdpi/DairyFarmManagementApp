using System.ComponentModel.DataAnnotations;

namespace BusinessManagementSystemApp.Core.Models.SetupModules
{
    public class Category : BaseDomain
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}