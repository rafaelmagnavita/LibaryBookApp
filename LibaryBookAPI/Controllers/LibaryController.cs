using LibaryAux.Repository;
using LibaryBookAPI.Handlers;
using LibaryDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibaryBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibaryController<T> : ControllerBase, ILibraryController<T> where T : LibraryEntity<T>
    {
        private Repository<T> _repository;
        private ITaskHandler<T> _handler;

        public LibaryController(Repository<T> repository)
        {
            _repository = repository;
            _handler = new HttpTaskHandler<T>(_repository);
        }

        [HttpPost]
        public async virtual Task<IActionResult> Create(T entity)
        {
            try
            {
                var addedEntity = await _repository.Add(entity);
                return await _handler.HandleCRUD(addedEntity,entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }

        [HttpPut]
        public async virtual Task<IActionResult> Update(T entity)
        {
            try
            {
                var updatedEntity = await _repository.Alter(entity);
                return await _handler.HandleCRUD(updatedEntity, entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }

        [HttpGet("CheckExistence")]
        public async virtual Task<IActionResult> CheckExistence(T entity)
        {
            try
            {
                bool exists = await _repository.Exists(entity);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }

        [HttpGet("GetAll")]
        public async virtual Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _repository.FindAll();
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }

        [HttpGet("GetById")]
        public async virtual Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _repository.FindById(id);
                if (result != null)
                    return Ok(JsonConvert.SerializeObject(result));
                else
                    return NotFound($"Could not find entity for id: {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }

        [HttpDelete]
        public async virtual Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = await _repository.FindById(id);

                if (entity == null)
                    return StatusCode(404, "Entity not Found");

                var deletedEntity = await _repository.Remove(entity);

                return await _handler.HandleCRUD(deletedEntity, entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }
    }
}
