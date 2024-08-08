using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Core.Models;
using OnlineLibrary.Persistence;
using OnlineLibrary.Application;

namespace OnlineLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext db;

        public UserController(ApplicationContext db) 
        {
            this.db = db;
        }

        [HttpPost("[Action]")]
        public ActionResult<string> Registration(string name, string password, string typeUser)
        {
            UserEntity user = new UserEntity(name, password, ConverterToTypeUser.FromStringToTypeUser(typeUser));
            if(user.Name == null || user.Password == null || user.Type == null) 
            {
                HttpContext.Response.StatusCode = 400;
                return "No data";
            }
            if (user.Name.Length < 2)
            {
                HttpContext.Response.StatusCode = 400;
                return "short name";
            }
            if (user.Password.Length < 2)
            {
                HttpContext.Response.StatusCode = 400;
                return "short password";
            }
            if(db.Users.Any(u => u.Name == user.Name))
            {
                HttpContext.Response.StatusCode = 400;
                return "name taken";
            }

            db.Add(user);
            db.SaveChanges();
            return "success";
        }
    }
}
