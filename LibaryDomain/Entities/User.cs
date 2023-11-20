﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDomain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}