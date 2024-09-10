using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineLibrary.Core.Models
{
    public class BookEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? DatePublication { get; set; }
        public string? DateAdded { get; set; }
        public List<AuthorEntity> Authors { get; set; } = new();
        public List<UserToBook> UsersTo { get; set; } = new();

        public BookEntity(string? title, string? description, string? datePublication, string? dateAdded)
        {
            Title = title;
            Description = description;
            DatePublication = datePublication;
            DateAdded = dateAdded;
        }
        public BookEntity() 
        { 

        }

    }
}
