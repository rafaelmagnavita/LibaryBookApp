using LibaryDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDomain.Validator
{
    public class UserValidator : IValidator<User>
    {
        public string _invalidFields { get; set; }

        public bool IsValid(User entity)
        {
            bool valid = true;

            if (entity == null)
                return false;

            if (string.IsNullOrEmpty(entity?.Email))
            {
                _invalidFields += "Email; ";
                valid = false;
            }
            if (string.IsNullOrEmpty(entity?.Name))
            {
                _invalidFields += "Name; ";
                valid = false;
            }

            return valid;
        }
    }
}
