using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLibrary.Core.Models.Enums;

namespace OnlineLibrary.Core.Models.Interfaces
{
    internal interface IUser
    {
        int Id { get; }
        string Name { get; }
        string Password { get; }
        TypeUser Type { get; }
    }
}
