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
        private BookRepository _bookRepository;
        public BooksController(BookRepository bookRepository) : base(bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost("EditStock")]
        public async Task<IActionResult> EditStock(int id, int value)
        {
            try
            {
                var result = await _bookRepository.ChangeStock(id, value);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }

        [HttpPost("Find")]
        public async Task<IActionResult> Find(int command, object param)
        {
            try
            {
                var result = await _bookRepository.Find(command, param);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }

    }
}
