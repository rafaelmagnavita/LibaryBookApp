using LibaryDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            if (!EmailIsValid(entity?.Email))
            {
                _invalidFields += "Invalid Email; ";
                valid = false;
            }
            if (string.IsNullOrEmpty(entity?.Name))
            {
                _invalidFields += "Invalid Name; ";
                valid = false;
            }
            return valid;
        }

        private bool EmailIsValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            if (string.IsNullOrEmpty(email))
                return false;
            if (!Regex.IsMatch(email, pattern))
                return false;
            return true;
        }
    }
}
