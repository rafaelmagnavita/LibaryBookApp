using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibaryBookAPI.Controllers
{
    public interface ILibraryController<T>
    {
        Task<IActionResult> Create(T entity);
        Task<IActionResult> Update(T entity);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetById(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> CheckExistence(string param);
    }
}
