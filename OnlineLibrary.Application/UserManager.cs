using OnlineLibrary.Core.Models;
using OnlineLibrary.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application
{
    public class UserManager
    {
        private IRepository<UserEntity> _userRepository;
        public UserManager(IRepository<UserEntity> repository) 
        { 
            _userRepository = repository;
        }

        public (bool,int,string) Validation(UserEntity entity)
        {
            if (entity.Name == null || entity.Password == null || entity.Type == null)
            {

                return (false,400,"No data");
            }
            if (entity.Name.Length < 2)
            {

                return (false, 400, "short name");
            }
            if (entity.Password.Length < 2)
            {

                return (false, 400,"short password");
            }
            if (_userRepository.GetAll().Where(u => u.Name == entity.Name).Count() > 0)
            {
                return (false, 400, "name taken");
            }

            return (true, 200, "ok");
        }


    }
}
