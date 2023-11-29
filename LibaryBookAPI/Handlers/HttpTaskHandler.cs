using LibaryAux.Repository;
using LibaryDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibaryBookAPI.Handlers
{
    public class HttpTaskHandler<T> : ITaskHandler<T> where T : LibraryEntity<T>
    {
        private Repository<T> _repository;
        public HttpTaskHandler(Repository<T> repository)
        {
            _repository = repository;
        }

        public Task<IActionResult> HandleCRUD(bool success, T entity)
        {
            if (success)
                return Task.FromResult<IActionResult>(new OkObjectResult(entity));
            else
            {
                return Task.FromResult<IActionResult>(new BadRequestObjectResult($"Error while excecuting CRUD: {_repository._log}"));
            }
        }
    }
}
