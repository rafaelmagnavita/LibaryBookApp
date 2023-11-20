using LibaryAux.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAux.Strategies
{
    public interface IEntityStrategy<T>
    {
        Task AddEntity(LibaryContext db, T entity);

    }
}
