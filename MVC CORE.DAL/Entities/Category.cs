using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CORE.DDL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(5)]
        public string Code { get; set; }
        public DateTime?CreatedOn { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedOn { get; set; }
        public virtual IEnumerable<Book> Books { get; set; }


    }
}
