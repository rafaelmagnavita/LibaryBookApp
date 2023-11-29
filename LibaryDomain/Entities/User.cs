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
    public class User : LibraryEntity<User>
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public User(string name, string email) : base(new UserValidator())
        {
            Name = name;
            Email = email;
            SetEntity(this);
        }
        public User() : base(new UserValidator())
        {
            SetEntity(this);
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Email == user.Email;
        }

    }
}
