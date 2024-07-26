using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Core.Models
{
    public class User
    {
        public int Id { get; }
        public string Name { get; }
        public string Password { get; }
        public TypeUser Type { get; }
    }
}
