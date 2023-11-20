using LibaryAux.Context;
using LibaryAux.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryAux.Strategies
{
    public class LoanStrategy : IEntityStrategy<Loan>
    {
        public async Task AddEntity(LibaryContext db, Loan entity)
        {
            try
            {
                await db.Loans.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
