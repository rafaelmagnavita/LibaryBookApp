using LibaryAux.Context;
using LibaryAux.Strategies;
using LibaryDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAux.Repository
{
    public class Repository<T> : IRepository<T> where T : LibraryEntity<T>
    {
        protected readonly LibaryContext db;
        public string _log;
        public Repository()
        {
            db = new LibaryContext();
        }
        public virtual async Task<bool> Alter(T entity)
        {
            try
            {
                if (entity.IsValid())
                {
                    db.Entry(entity).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _log = entity.GetInvalid();
                    return false;
                }
            }
            catch (Exception ex)
            {
                _log = ex.Message;
                return false;
            }
        }

        public virtual async Task<bool> Add(T entity)
        {
            try
            {
                if (entity.IsValid())
                {
                    if (await Exists(entity))
                        return false;
                    db.Set<T>().Add(entity);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _log = entity.GetInvalid();
                    return false;
                }
            }
            catch (Exception ex)
            {
                _log = ex.Message;
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
                _log = ex.Message;
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
                _log = ex.Message;
                throw null;
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
                _log = ex.Message;
                throw null;
            }
        }

        public virtual async Task<bool> Exists(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
