using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Core.Models;
using OnlineLibrary.Persistence;
using OnlineLibrary.Application;
using System.Runtime.Intrinsics.Arm;
using NuGet.Protocol;

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
        public ActionResult<string> Registration(string? name, string? password, string? typeUser)
        {
            var manager = new UserManager(db.userRepository);
            UserEntity user = new UserEntity(name, password, ConverterToTypeUser.FromStringToTypeUser(typeUser));
            var (fl,code,ans) = manager.Validation(user);
            if (fl)
            {
                db.userRepository.Add(user);
                db.SaveChanges();
            }
            HttpContext.Response.StatusCode = code;
            HttpContext.Response.StatusCode = 401;
            int a = HttpContext.Response.StatusCode;
            return ans;
        }


    }
}
