using LibaryAux.Context;
using LibaryDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAux.Repository
{
    public class UserLoginRepository : Repository<User>, IRepository<User>
    {
        public override async Task<bool> Exists(User entity)
        {
            _log = "Email Already Registered";
            return await db.Users.AnyAsync(b => b.Email.Equals(entity.Email));
        }

        public async Task<User> GetUserbyEmail(string Email)
        {
            try
            {
                var user = await db.Users.SingleOrDefaultAsync(bk => bk.Email.ToLower().Equals(Email.ToLower()));
                return user;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
