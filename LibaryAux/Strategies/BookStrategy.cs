using LibaryAux.Context;
using LibaryDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAux.Strategies
{
    public class BookStrategy : IEntityStrategy<Book>
    {
        public async Task AddEntity(LibaryContext db, Book entity)
        {
            try
            {
                await db.Books.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
