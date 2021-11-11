using System.ComponentModel.DataAnnotations;

namespace BMSA.App.ViewModels.AccountViewModels
{

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
