using LibaryDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDomain.Validator
{
    public class BookValidator : IValidator<Book>
    {
        public string _invalidFields { get;  set; }

        public bool IsValid(Book entity)
        {
            bool valid = true;

            if (entity == null)
                return false;

            if (string.IsNullOrEmpty(entity?.ISBN))
            {
                _invalidFields += "ISBN; ";
                valid = false;
            }
            if (string.IsNullOrEmpty(entity?.Title))
            {
                _invalidFields += "Title; ";
                valid = false;
            }
            if (string.IsNullOrEmpty(entity?.Author))
            {
                _invalidFields += "Author; ";
                valid = false;
            }
            if (entity?.PublishYear <= 0 || entity?.PublishYear > DateTime.Now.Year)
            {
                _invalidFields += "PublishYear; ";
                valid = false;
            }
            return valid;
        }
    }
}
