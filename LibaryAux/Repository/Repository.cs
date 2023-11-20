using LibaryAux.Context;
using LibaryAux.Strategies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAux.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly LibaryContext db;
        //private readonly IEntityStrategy<T> _strategy;
        public Repository()
        {
            db = new LibaryContext();
            //_strategy = strategy;
        }

        //public virtual async Task<bool> Add(T entity)
        //{
        //    try
        //    {
        //        await _strategy.AddEntity(db, entity);
        //        await db.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public virtual async Task<bool> Alter(T entity)
        {
            try
            {
                db.Entry(entity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual async Task<bool> Add(T entity)
        {
            try
            {
                db.Set<T>().Add(entity);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual async Task<bool> Remove(T entity)
        {
            try
            {
                db.Set<T>().Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual async Task<T> FindById(int id)
        {
            try
            {
                return await db.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public virtual async Task<List<T>> FindAll()
        {
            try
            {
                return await db.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public virtual async Task<bool> Exists(object param)
        {
            throw new NotImplementedException();
        }
    }
}
