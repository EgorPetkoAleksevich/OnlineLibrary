using OnlineLibrary.Core.Models;
using OnlineLibrary.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application
{
    public class BookManager
    {
        private IRepository<BookEntity> _repository;
        //private Dictionary<(TypeUser, TypeUser), List<TypeAction>> rulesWithOthers;
        public BookManager(IRepository<BookEntity> repository)
        {
            _repository = repository;
        }

        public BookEntity? GetByName(string? title)
        {
            foreach (var entity in _repository.GetAll())
            {
                if (entity.Title == title) return entity;
            }
            return null;
        }

        public BookEntity? GetById(int id)
        {
            foreach (var entity in _repository.GetAll())
            {
                if (entity.Id == id) return entity;
            }
            return null;
        }

        public void Delete(BookEntity entity)
        {
            _repository.Delete(entity);
            _repository.Save();
        }

    }
}
