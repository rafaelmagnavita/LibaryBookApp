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
    public class LoanRepository : Repository<Loan> , IRepository<Loan>
    {
        public override async Task<bool> Exists(Loan entity)
        {
            _log = "Loan for this User and Book Already Registered";
            return await db.Loans.AnyAsync(b => b.UserId.Equals(entity.UserId) &&
            b.BookId.Equals(entity.BookId) &&
            b.LoanDate.Equals(entity.LoanDate));
        }

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
