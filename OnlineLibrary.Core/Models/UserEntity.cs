﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Core.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public TypeUser? Type { get; set; }

        public UserEntity(string? name, string? password, TypeUser? type) { 
            Name = name;
            Password = password; 
            Type = type;
        }
    }
}