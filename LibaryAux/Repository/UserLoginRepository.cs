using LibaryAux.Context;
using LibaryAux.Entities;
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
        public override async Task<bool> Exists(object emailObj)
        {
            try
            {
                var email = emailObj.ToString();
                var user = await db.Users.SingleOrDefaultAsync(bk => bk.Email.ToLower().Equals(email));
                if (user != null)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
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
