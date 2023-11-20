using LibaryAux.Context;
using LibaryDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAux.Strategies
{
    public class UserStrategy : IEntityStrategy<User>
    {
        public async Task AddEntity(LibaryContext db, User entity)
        {
            try
            {
                await db.Users.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
