using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Core.Models
{
    public class UserToBook
    {
        public int Id { get; set; }
        public BookEntity Book { get; set; } 
        public UserEntity User { get; set; } 

        public TypeStatusForAccessingBook StatusForAccessing { get; set; }
        public TypeStatusToProductionBook StatusToProduction { get; set; }

    }
}
