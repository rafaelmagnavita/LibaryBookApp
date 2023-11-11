using LibaryAux.Context;
using LibaryAux.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LibaryAux.Enums.SearchEnums;
using static LibaryAux.Repository.IRepository;

namespace LibaryAux.Repository
{
    public class BookRepository : IRepository<Book>
    {
        public LibaryContext db => new LibaryContext();

        public async Task<bool> Add(Book entity)
        {
            try
            {
                db.Books.Add(entity);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            };
        }

        public async Task<bool> Alter(Book entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exists(object param)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> FindAll()
        {
            try
            {
                var books = await db.Set<Book>().ToListAsync();

                return books;
            }
            catch (Exception ex)
            {
                return new List<Book>();
            }
        }

        public async Task<Book> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Book> Find(int command, object param)
        {
            try
            {
                Book book = null;
                switch (command)
                {
                    case (int)BookSearchCommand.TITLE:
                        book = await db.Books.SingleOrDefaultAsync(bk => bk.Title.Contains(param.ToString()));
                        break;
                    case (int)BookSearchCommand.ISBN:
                        book = await db.Books.SingleOrDefaultAsync(bk => bk.ISBN.ToLower().Equals(param.ToString().ToLower()));
                        break;
                    case (int)BookSearchCommand.ID:
                        book = await db.Books.SingleOrDefaultAsync(bk => bk.Id.Equals(Convert.ToInt32(param.ToString())));
                        break;
                }
                return book;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Remove(Book entity)
        {
            try
            {
                db.Set<Book>().Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
