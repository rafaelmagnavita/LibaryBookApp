using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibaryAux.Entities;
using LibaryAux;
using Newtonsoft.Json;
namespace LibaryBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public object Get(int? command = null, string searchValue = null)
        {
            try
            {
                if(command == null || string.IsNullOrEmpty(searchValue))
                {
                    List<Book> books = DbOps.GetAllBooks();
                    return JsonConvert.SerializeObject(books);
                }
                else
                {
                    Book book = DbOps.GetBook(command.Value, searchValue);
                    return JsonConvert.SerializeObject(book);
                }
            }
            catch (Exception ex)
            {
                return 500;
            }
        }
    }
}
