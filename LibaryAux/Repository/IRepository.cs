using LibaryAux.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAux.Repository
{
    public interface IRepository<T>
    {
        Task<bool> Add(T entity);
        Task<bool> Alter(T entity);
        Task<bool> Remove(T entity);
        Task<T> FindById(int id);
        Task<List<T>> FindAll();
        Task<bool> Exists(object param);

    }
}
