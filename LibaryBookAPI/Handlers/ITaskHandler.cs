using LibaryAux.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibaryBookAPI.Handlers
{
    public interface ITaskHandler<T>
    {
        Task<IActionResult> HandleCRUD(bool success, T entity);
    }
}
