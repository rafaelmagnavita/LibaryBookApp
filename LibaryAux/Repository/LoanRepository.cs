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
    public class LoanRepository : Repository<Loan> , IRepository<Loan>
    {
        public async Task<List<Loan>> GetActiveLoansByUserId(int userId)
        {
            try
            {
                var loans = await db.Loans.Where(lo => lo.UserId.Equals(userId) && !lo.BookReturned).ToListAsync();
                return loans;
            }
            catch (Exception)
            {
                return new List<Loan>();
            }
        }
    }
}
