using LibaryDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDomain.Validator
{
    public class LoanValidator : IValidator<Loan>
    {
        public string _invalidFields { get; set; }

        public bool IsValid(Loan entity)
        {
            bool valid = true;

            if (entity == null)
                return false;

            if (entity?.User == null)
            {
                _invalidFields += "User; ";
                valid = false;
            }
            if (entity?.Book == null)
            {
                _invalidFields += "Book; ";
                valid = false;
            }
            if (entity?.BookReturned == null)
            {
                _invalidFields += "BookReturned; ";
                valid = false;
            }
            if (entity?.LoanPeriod <= 0)
            {
                _invalidFields += "LoanPeriod; ";
                valid = false;
            }
            if (entity?.LoanDate == null || entity?.LoanDate > DateTime.Now)
            {
                _invalidFields += "LoanDate; ";
                valid = false;
            }
            return valid;
        }
    }
}
