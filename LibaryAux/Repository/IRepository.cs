using LibaryAux.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAux.Repository
{
    public interface IRepository
    {
        public interface IRepository<TEntity>
        {
            LibaryContext db { get; }
            Task<bool> Add(TEntity entity);
            Task<bool> Alter(TEntity entity);
            Task<bool> Remove(TEntity entity);
            Task<TEntity> FindById(int id);
            Task<List<TEntity>> FindAll();
            Task<bool> Exists(object param);

        }

    }
}
