using LibaryAux.Repository;
using LibaryDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibaryBookAPI.Controllers
{
    public class UsersController : LibaryController<User>
    {
        public UsersController(UserLoginRepository userRepository) : base(userRepository)
        {

        }
    }
}
