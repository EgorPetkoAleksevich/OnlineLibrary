using OnlineLibrary.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application
{
    public static class ConverterToTypeUser
    {
        public static TypeUser FromStringToTypeUser(string? value)
        {
            if(value == null) return TypeUser.unknown;
            value = value.ToLower();
            switch(value)
            {
                case "reader":
                    return TypeUser.reader;
                    
                case "admin":
                    return TypeUser.admin;

                case "writer":
                    return TypeUser.writer;
            }
            return TypeUser.unknown;
        }

        public static TypeUser FromIntToTypeUser(int value)
        {
            int enumCount = Enum.GetNames(typeof(TypeUser)).Length;
            if(value > enumCount) return TypeUser.unknown;
            return (TypeUser)value;
        }
    }
}
