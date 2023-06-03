using Microsoft.EntityFrameworkCore;
using MVC_CORE.BLL.Interfaces;
using MVC_CORE.DDL.Context;
using MVC_CORE.DDL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CORE.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext context;
        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<int> Add(T item)
        {
            context.Set<T>().Add(item);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(T item)
        {
            context.Set<T>().Remove(item);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll() {
          if(typeof(T)==typeof(Book))
            return  (IEnumerable<T>) await context.Set<Book>().Include(e=>e.Category).ToListAsync();
           return  await context.Set<T>().ToListAsync();

        }
       



        public async Task<T> GetById(int id)
        {
            var item = await context.Set<T>().FindAsync(id);
            return item;

        }


        public async Task<int> Update(T item)
        {
            context.Set<T>().Update(item);
            return await context.SaveChangesAsync();
        }
    }
}
