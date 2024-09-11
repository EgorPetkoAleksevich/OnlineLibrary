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
        private IRepository<UserEntity> _repository;
        private Dictionary<(TypeUser, TypeUser), List<TypeAction>> rulesWithOthers;
        public UserManager(IRepository<UserEntity> repository) 
        { 
            _repository = repository;
            rulesWithOthers = new Dictionary<(TypeUser, TypeUser), List<TypeAction>>();

            rulesWithOthers[(TypeUser.superAdmin,TypeUser.superAdmin)] = new List<TypeAction>() { TypeAction.create, TypeAction.ban,TypeAction.delete,TypeAction.get};
            rulesWithOthers[(TypeUser.superAdmin, TypeUser.admin)] = new List<TypeAction>() { TypeAction.create, TypeAction.ban, TypeAction.delete, TypeAction.get };
            rulesWithOthers[(TypeUser.superAdmin, TypeUser.moderator)] = new List<TypeAction>() { TypeAction.create, TypeAction.ban, TypeAction.delete, TypeAction.get };
            rulesWithOthers[(TypeUser.superAdmin, TypeUser.user)] = new List<TypeAction>() { TypeAction.create, TypeAction.ban, TypeAction.delete, TypeAction.get };
            rulesWithOthers[(TypeUser.superAdmin, TypeUser.unknown)] = new List<TypeAction>() { TypeAction.create, TypeAction.ban, TypeAction.delete, TypeAction.get };

            rulesWithOthers[(TypeUser.admin, TypeUser.superAdmin)] = new List<TypeAction>() { };
            rulesWithOthers[(TypeUser.admin, TypeUser.admin)] = new List<TypeAction>() { TypeAction.get };
            rulesWithOthers[(TypeUser.admin, TypeUser.moderator)] = new List<TypeAction>() { TypeAction.create, TypeAction.ban, TypeAction.delete, TypeAction.get };
            rulesWithOthers[(TypeUser.admin, TypeUser.user)] = new List<TypeAction>() { TypeAction.create, TypeAction.ban, TypeAction.delete, TypeAction.get };
            rulesWithOthers[(TypeUser.admin, TypeUser.unknown)] = new List<TypeAction>() { TypeAction.create, TypeAction.ban, TypeAction.delete, TypeAction.get };

            rulesWithOthers[(TypeUser.moderator, TypeUser.superAdmin)] = new List<TypeAction>() { };
            rulesWithOthers[(TypeUser.moderator, TypeUser.admin)] = new List<TypeAction>() { TypeAction.get };
            rulesWithOthers[(TypeUser.moderator, TypeUser.moderator)] = new List<TypeAction>() { TypeAction.get };
            rulesWithOthers[(TypeUser.moderator, TypeUser.user)] = new List<TypeAction>() { TypeAction.create, TypeAction.ban, TypeAction.delete, TypeAction.get };
            rulesWithOthers[(TypeUser.moderator, TypeUser.unknown)] = new List<TypeAction>() { TypeAction.create, TypeAction.ban, TypeAction.delete, TypeAction.get };

            rulesWithOthers[(TypeUser.user, TypeUser.superAdmin)] = new List<TypeAction>() { };
            rulesWithOthers[(TypeUser.user, TypeUser.admin)] = new List<TypeAction>() { };
            rulesWithOthers[(TypeUser.user, TypeUser.moderator)] = new List<TypeAction>() { };
            rulesWithOthers[(TypeUser.user, TypeUser.user)] = new List<TypeAction>() { };
            rulesWithOthers[(TypeUser.user, TypeUser.unknown)] = new List<TypeAction>() { };

            rulesWithOthers[(TypeUser.unknown, TypeUser.superAdmin)] = new List<TypeAction>() { };
            rulesWithOthers[(TypeUser.unknown, TypeUser.admin)] = new List<TypeAction>() { };
            rulesWithOthers[(TypeUser.unknown, TypeUser.moderator)] = new List<TypeAction>() { };
            rulesWithOthers[(TypeUser.unknown, TypeUser.user)] = new List<TypeAction>() { };
            rulesWithOthers[(TypeUser.unknown, TypeUser.unknown)] = new List<TypeAction>() { };
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
            if (_repository.GetAll().Where(u => u.Name == entity.Name).Count() > 0)
            {
                return (false, 400, "name taken");
            }

            return (true, 200, "ok");
        }

        public UserEntity? GetByName(string? name)
        {
            foreach(var entity in _repository.GetAll()) 
            { 
                if (entity.Name == name) return entity; 
            }
            return null;
        }

        public UserEntity? GetById (int id)
        {
            foreach (var entity in _repository.GetAll())
            {
                if (entity.Id == id) return entity;
            }
            return null;
        }

        public bool haveRule(TypeUser? typeExecutor, TypeUser? typeUser, TypeAction typeAction)
        {
            if (typeExecutor == null || typeUser == null) return false;
            var key = ((TypeUser)typeExecutor, (TypeUser)typeUser);
            
            if (rulesWithOthers.ContainsKey(key))
            {
                foreach (var rule in rulesWithOthers[key])
                {
                    if(rule == typeAction) return true;
                }
            }
            return false;
        }

        public void Delete(UserEntity entity)
        {
            _repository.Delete(entity);
            _repository.Save();
        }

    }
}
