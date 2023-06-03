using System.ComponentModel.DataAnnotations;

namespace MVC_CORE_Application.Models.ViewModel
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required !")]
        [EmailAddress(ErrorMessage = "Email is not valid !")]
        public string Email { get; set; }

    }
}
