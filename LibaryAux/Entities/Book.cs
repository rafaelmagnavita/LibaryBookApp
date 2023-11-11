using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAux.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; private set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublishYear { get; set; }

        public Book(string title, string author, string isbn, int publishYear)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            PublishYear = publishYear;
        }
        public Book()
        {
        }
    }
}
