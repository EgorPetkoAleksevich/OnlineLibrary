using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Core.Models
{
    public class AuthorEntity
    {
        public int Id { get; set;  }
        public string? Name { get; set; }
        public AuthorEntity(string name) 
        { 
            Name = name;
        }
        public List<BookEntity> Books { get; set; } = new();
    }
}
