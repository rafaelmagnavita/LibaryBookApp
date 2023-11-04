using LibaryAux.Context;
using LibaryAux.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LibaryAux
{
    public class Main
    {
        private LibaryContext db = new LibaryContext();

        public void AddUser(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void AddBook(Book book)
        {
            try
            {
                db.Books.Add(book);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void AddLoan(Loan loan)
        {
            try
            {
                db.Loans.Add(loan);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void EditLoan(Loan loan)
        {
            try
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void RemoveBook(Book book)
        {
            try
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public Book GetBook(int command, object param)
        {
            try
            {
                Book book = null;
                switch (command)
                {
                    case 1:
                        book = db.Books.SingleOrDefaultAsync(bk => bk.Title.Contains(param.ToString())).GetAwaiter().GetResult();
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

        public System.Collections.Generic.List<Book> GetAllBooks()
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
    }
}
