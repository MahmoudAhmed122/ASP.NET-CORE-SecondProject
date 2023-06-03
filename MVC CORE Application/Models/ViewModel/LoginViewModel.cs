using System.ComponentModel.DataAnnotations;

namespace MVC_CORE_Application.Models.ViewModel
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Email is Required !")]
        [EmailAddress(ErrorMessage = "Email is not valid !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required !")]
        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}
