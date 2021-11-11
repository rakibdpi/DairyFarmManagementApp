using System.ComponentModel.DataAnnotations;

namespace BusinessManagementSystemApp.Core.Dtos.SetupModules
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}