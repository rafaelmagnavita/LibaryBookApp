using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                    if (book != null)
                        return JsonConvert.SerializeObject(book);
                    else
                        return 404;
                }
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        [HttpPost]
        public object Add(Book book)
        {
            try
            {
                //if (string.IsNullOrEmpty(jsonObject))
                //    return 400;

                //var book = JsonConvert.DeserializeObject<Book>(jsonObject);

                if (book == null)
                    return 400;

                bool success = DbOps.AddBook(book);

                if (success)
                {
                    return 200;
                }
                else
                {
                    return 404;
                }

            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        [HttpDelete]
        public object Delete(int command, string searchValue)
        {
            try
            {
                if ((command < 1 || command > 3) || string.IsNullOrEmpty(searchValue))
                {
                    return 404;
                }

                var book = DbOps.GetBook(command, searchValue);

                if (book == null)
                    return 404;

                bool success = DbOps.RemoveBook(book);

                if (success)
                {
                    return 200;
                }
                else
                {
                    return 404;
                }

            }
            catch (Exception ex)
            {
                return 500;
            }
        }
    }
}
