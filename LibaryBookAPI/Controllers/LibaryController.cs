using LibaryAux.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibaryBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibaryController<T> : ControllerBase, ILibraryController<T>
    {
        protected IRepository<T> _repository;

        public LibaryController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async virtual Task<IActionResult> Create(T entity)
        {
            try
            {
                var addedEntity = await _repository.Add(entity);
                return Ok(entity);
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
                return Ok(updatedEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }

        //[HttpGet]
        //public async virtual Task<IActionResult> CheckExistence(object param)
        //{
        //    try
        //    {
        //        bool exists = await _repository.Exists(param);
        //        return Ok(exists);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal Server Error: {ex}");
        //    }
        //}

        //[HttpGet]
        //public async virtual Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        var result = await _repository.FindAll();
        //        return Ok(JsonConvert.SerializeObject(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal Server Error: {ex}");
        //    }
        //}

        //[HttpGet("{id}")]
        //public async virtual Task<IActionResult> GetById(int id)
        //{
        //    try
        //    {
        //        var result = await _repository.FindById(id);
        //        if (result != null)
        //            return Ok(JsonConvert.SerializeObject(result));
        //        else
        //            return NotFound($"Could not find entity for id: {id}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal Server Error: {ex}");
        //    }
        //}

        [HttpDelete]
        public async virtual Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = await _repository.FindById(id);

                if (entity == null)
                    return StatusCode(404, "Entity not Found");

                await _repository.Remove(entity);

                return Ok("Entity Removed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }
    }
}
