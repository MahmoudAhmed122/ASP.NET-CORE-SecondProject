using MVC_CORE.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CORE.BLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IBookRepository BookRepository { get; }

        public UnitOfWork(ICategoryRepository categoryRepository ,IBookRepository bookRepository)
        {
            CategoryRepository = categoryRepository;
            BookRepository = bookRepository;
        }
    }
}
