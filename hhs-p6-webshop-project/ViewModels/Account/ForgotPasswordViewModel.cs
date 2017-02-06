using System.ComponentModel.DataAnnotations;

namespace hhs_p6_webshop_project.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}