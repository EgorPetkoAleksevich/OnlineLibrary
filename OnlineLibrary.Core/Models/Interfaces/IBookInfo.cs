using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineLibrary.Core.Models.Interfaces
{
    internal interface IBookInfo
    {
        int Id { get; }
        string Title { get; }
        string? Description { get; }
        IDate? DatePublication { get; }
        IDate DateAdded { get; }
        IAuthorInfo? Author { get; }
    }
}
