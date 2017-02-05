using System.ComponentModel.DataAnnotations;

namespace hhs_p6_webshop_project.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "De {0} moet minimaal {2} en maximaal {1} karakters lang zijn.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overeen!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}