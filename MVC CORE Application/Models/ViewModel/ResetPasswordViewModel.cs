using System.ComponentModel.DataAnnotations;

namespace MVC_CORE_Application.Models.ViewModel
{
    public class ResetPasswordViewModel
    {

        public string Email { get; set; }

        public string Token { get; set; }

        [Required(ErrorMessage = "Password is Required !")]
        [MinLength(5, ErrorMessage = "Min Length is 5 character !")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required !")]
        [Compare("NewPassword", ErrorMessage = "ConfirmPassword does not match Password !")]
        public string ConfirmPassword { get; set; }

    }
}
