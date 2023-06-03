using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CORE.DDL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [StringLength(100,MinimumLength =5)]     
        public string Name { get; set; }
        [StringLength(100, MinimumLength = 5)]
        public string? Description { get; set; }
        public DateTime? CreatedOn { get; set; }=DateTime.Now;
        public DateTime? LastUpdatedOn { get; set; }
        public string? ImageName { get; set; }
        [Range(100,10000,ErrorMessage ="Salary must be between 100 and 10000")]
        public int Salary { get; set; }
        public virtual Category? Category { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
}
    }
