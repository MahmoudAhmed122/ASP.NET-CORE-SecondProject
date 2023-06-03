using System.ComponentModel.DataAnnotations;

namespace MVC_CORE_Application.Models.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [MinLength(5 ,ErrorMessage ="Name must be more than 5 characters!" )]
        [MaxLength(50 , ErrorMessage = "Name must be less than 50 characters!")]
        [Required(ErrorMessage ="Name is required!")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "Name must be less than 5 characters!")]
        public string Code { get; set; }


        public DateTime? LastUpdatedOn { get; set; } 
    }
}
