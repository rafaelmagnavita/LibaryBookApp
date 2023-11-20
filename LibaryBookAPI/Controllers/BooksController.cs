using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibaryAux;
using Newtonsoft.Json;
using LibaryDomain.Entities;
using LibaryAux.Repository;

namespace LibaryBookAPI.Controllers
{

    public class BooksController : LibaryController<Book>
    {
        public BooksController(BookRepository bookRepository) : base(bookRepository)
        {
        }

    }
}
