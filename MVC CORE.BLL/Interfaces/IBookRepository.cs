﻿using MVC_CORE.DDL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CORE.BLL.Interfaces
{
    public interface IBookRepository:IGenericRepository<Book>
    {

        Task<IEnumerable<Book>>searchByName(string searchValue);

    }
}
