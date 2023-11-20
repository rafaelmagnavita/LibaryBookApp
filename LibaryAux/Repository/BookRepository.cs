﻿using LibaryAux.Context;
using LibaryAux.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LibaryAux.Enums.SearchEnums;

namespace LibaryAux.Repository
{
    public class BookRepository : Repository<Book>, IRepository<Book>
    {

        public override async Task<bool> Exists(object param)
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
    }
}
