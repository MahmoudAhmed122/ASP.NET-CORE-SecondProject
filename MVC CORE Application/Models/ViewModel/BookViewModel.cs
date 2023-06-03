using Microsoft.EntityFrameworkCore.Migrations;
using MVC_CORE.DDL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_CORE_Application.Models.ViewModel
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 5 , ErrorMessage ="Name must be between 5 and 100 Characters!")]
        public string Name { get; set; }
        [StringLength(300, MinimumLength = 5 , ErrorMessage = "Description must be between 5 and 300 Characters!")]
        public string? Description { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedOn { get; set; }
        [Range(100, 10000, ErrorMessage = "Salary must be between 100 and 10000")]
        public int Salary { get; set; }
        public virtual Category? Category { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public string? ImageName { get; set; }

        public IFormFile? Image { get; set; }
    }
}
