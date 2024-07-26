using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineLibrary.Core.Models
{
    public class Book
    {
        public int Id { get; }
        public string Title { get; }
        public string? Description { get; }
        public Date? DatePublication { get; }
        public Date DateAdded { get; }
        public Author? Author { get; }
    }
}
