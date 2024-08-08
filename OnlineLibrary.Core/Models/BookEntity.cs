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
        public AuthorEntity? Author { get; set; }
    }
}
