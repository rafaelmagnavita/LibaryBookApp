using LibaryDomain.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDomain.Entities
{
    public class Book : LibraryEntity<Book>
    {
        [Key]
        public int Id { get; private set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublishYear { get; set; }
        public int? Stock { get; private set; }
        public Book(string title, string author, string isbn, int publishYear) : base(new BookValidator())
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            PublishYear = publishYear;
            SetEntity(this);
        }
        public Book() : base(new BookValidator())
        {
            SetEntity(this);
        }

        public void AddStock(int count)
        {
            if (Stock == null)
                Stock = 0;
            Stock += count;
        }

        public void RemoveStock(int count)
        {
            var value = Stock - count;
            Stock = value >= 0 ? value : 0;
        }
    }
}
