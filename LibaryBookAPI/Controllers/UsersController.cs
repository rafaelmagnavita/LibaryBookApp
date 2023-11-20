using LibaryAux.Repository;
using LibaryDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibaryBookAPI.Controllers
{
    public class UsersController : LibaryController<User>
    {
        private UserLoginRepository _userLoginRepository;
        public UsersController(UserLoginRepository userRepository) : base(userRepository)
        {
            _userLoginRepository = userRepository;
        }

        [HttpGet("GetByEmail")]
        public async virtual Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var result = await _userLoginRepository.GetUserbyEmail(email);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }
    }
}
