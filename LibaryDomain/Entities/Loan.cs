using LibaryDomain.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDomain.Entities
{
    public class Loan : LibraryEntity<Loan>
    {
        [Key]
        public int Id { get; private set; }
        public bool BookReturned { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        public DateTime LoanDate { get; set; }
        public int LoanPeriod { get; set; }

        public Loan(int user, int book, DateTime? loanDate = null, int? loanPeriod = null) : base(new LoanValidator())
        {
            BookReturned = false;
            UserId = user;
            BookId = book;
            if (loanDate != null)
                LoanDate = (DateTime)loanDate;
            else
                LoanDate = DateTime.Now;
            if (loanPeriod != null)
                LoanPeriod = (int)LoanPeriod;
            else
                LoanPeriod = 90;
            SetEntity(this);
        }
        public Loan() : base(new LoanValidator())
        {
            SetEntity(this);
        }

        public override bool Equals(object obj)
        {
            return obj is Loan loan &&
                   UserId == loan.UserId
                   && BookId == loan.BookId
                   && LoanDate == loan.LoanDate;
        }
    }
}
