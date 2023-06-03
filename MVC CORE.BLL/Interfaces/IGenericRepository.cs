using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CORE.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task <T> GetById(int id);
        Task <IEnumerable<T>> GetAll();
        Task<int> Add(T item);
        Task<int> Update(T item);
        Task<int> Delete(T item);

    }
}

