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
    public class BookRepository : GenericRepository<Book>,IBookRepository {

        public ApplicationDbContext Context { get; }
        public BookRepository(ApplicationDbContext context) :base(context) { 
            Context=context;    
        
        }
        public async Task<IEnumerable<Book>> searchByName(string searchValue)
        {
            var books = await Context.books.Where(c => c.Name.Contains(searchValue)).ToListAsync();
            return books;
        }

    }
}
