using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_CORE_Application.Models.ViewModel
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage ="Email is Required !")]
        [EmailAddress(ErrorMessage ="Email is not valid !")]
        public string Email{ get; set; }

        [Required(ErrorMessage = "Password is Required !")]
        [MinLength(5,ErrorMessage ="Min Length is 5 character !")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required !")]
        [Compare("Password",ErrorMessage ="ConfirmPassword does not match Password !")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }

    }
}
