using LibaryAux.Context;
using LibaryAux.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LibaryAux
{
    public static class DbOps
    {
        private static LibaryContext db = new LibaryContext();

        public static bool AddUser(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool AddBook(Book book)
        {
            try
            {
                db.Books.Add(book);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool AddLoan(Loan loan)
        {
            try
            {
                db.Loans.Add(loan);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool EditLoan(Loan loan)
        {
            try
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool RemoveBook(Book book)
        {
            try
            {
                db.Books.Remove(book);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Book GetBook(int command, object param)
        {
            try
            {
                Book book = null;
                switch (command)
                {
                    case 1:
                        book = db.Books.SingleOrDefaultAsync(bk => bk.Title.Contains(param.ToString())).GetAwaiter().GetResult();
                        break;
                    case 2:
                        book = db.Books.SingleOrDefaultAsync(bk => bk.ISBN.ToLower().Equals(param.ToString().ToLower())).GetAwaiter().GetResult();
                        break;
                    case 3:
                        book = db.Books.SingleOrDefaultAsync(bk => bk.Id.Equals(Convert.ToInt32(param.ToString()))).GetAwaiter().GetResult();
                        break;
                }
                return book;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static System.Collections.Generic.List<Book> GetAllBooks()
        {
            try
            {
                var books = db.Books.ToListAsync().GetAwaiter().GetResult().ToList();

                return books;
            }
            catch (Exception ex)
            {
                return new System.Collections.Generic.List<Book>();
            }
        }

        public static bool UserExists(string Email)
        {
            var user = db.Users.SingleOrDefaultAsync(bk => bk.Email.ToLower().Equals(Email.ToLower())).GetAwaiter().GetResult();
            if (user != null)
                return true;
            return false;
        }
    }
}
