using Microsoft.EntityFrameworkCore;
using MVC_CORE.BLL.Interfaces;
using MVC_CORE.DDL.Context;
using MVC_CORE.DDL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CORE.BLL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public ApplicationDbContext Context { get; }
        public CategoryRepository(ApplicationDbContext context) : base(context) {

            Context = context;
        }
        public async Task<IEnumerable<Category>> searchByName(string searchValue) { 
         var categories=  await Context.Categories.Where(c=>c.Name.Contains(searchValue)).ToListAsync();
            return categories;
        }

      
    }
}

