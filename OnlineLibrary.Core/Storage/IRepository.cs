using OnlineLibrary.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Core.Storage
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Delete(T item);
        List<T> GetAll();
        void Save();
    }
}
