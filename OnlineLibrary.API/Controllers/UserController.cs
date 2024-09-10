using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Core.Models;
using OnlineLibrary.Persistence;
using OnlineLibrary.Application;
using System.Runtime.Intrinsics.Arm;
using NuGet.Protocol;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

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
        public ActionResult<string> test()
        {
            db.SaveChanges();
            UserEntity vasa = new UserEntity("vasa", "12", TypeUser.user);
            UserEntity admin = new UserEntity("admin", "1234", TypeUser.admin);
            UserEntity modern = new UserEntity("mod", "123", TypeUser.moderator);
            db.Users.AddRange(vasa, admin, modern);

            BookEntity a = new BookEntity("a", "AaaAa", null, null);
            BookEntity b = new BookEntity("b", "BbbbBBbb", null, null);
            BookEntity c = new BookEntity("c", "CcccCCcC", null, null);
            db.Books.AttachRange(a, b, c);

            AuthorEntity biba = new AuthorEntity("biba");
            AuthorEntity boba = new AuthorEntity("boba");
            AuthorEntity dido = new AuthorEntity("dido");
            db.Authors.AddRange(biba, boba, dido);

            biba.Books.AddRange([a, b]);
            boba.Books.AddRange([b]);
            dido.Books.AddRange([c]);

            vasa.BooksTo.Add(
                new UserToBook() 
                { 
                    Book = a, 
                    StatusForAccessing = TypeStatusForAccessingBook.reading, 
                    StatusToProduction = TypeStatusToProductionBook.none
                }
            );

            b.UsersTo.Add(
                new UserToBook()
                {
                    User = modern,
                    StatusForAccessing = TypeStatusForAccessingBook.moderation,
                    StatusToProduction=TypeStatusToProductionBook.author
                }
            );

            db.SaveChanges();
            return "asd";

            
        }
        
        [HttpPost("[Action]")]
        public ActionResult<string> Registration(string name, string password, string? typeUser)
        {
            UserManager manager = new UserManager(db.userRepository);
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

        [HttpDelete("[Action]")]
        public ActionResult<string> Removal(string nameRemoved, string nameExecutor, string password)
        {
            var manager = new UserManager(db.userRepository);
            UserEntity? executor = manager.GetByName(nameExecutor);
            UserEntity? user = manager.GetByName(nameRemoved);


            if (user == null || executor == null)
            {
                return NotFound();
            }

            if(password != executor.Password)
            {
                return BadRequest();
            }

            if(manager.haveRule(executor.Type,user.Type,TypeAction.delete))
            { 
              manager.Delete(user);
                return Ok();
            }
            return "not enough rights";
        }

        

        [HttpGet("[Action]")]
        public ActionResult<string> GetByName(string name, string nameExecutor, string password)
        {
            UserManager manager = new UserManager(db.userRepository);
            UserEntity? executor = manager.GetByName(nameExecutor);
            UserEntity? user = manager.GetByName(name);
            var input = HttpContext.Request.BodyReader;
            var output = HttpContext.Response.BodyWriter;

            string ans = input.ToJson();

            if (user == null || executor == null)
            {
                return NotFound();
            }

            if (password != executor.Password)
            {
                return BadRequest();
            }

            if (manager.haveRule(executor.Type, user.Type, TypeAction.get))
            {
                
                return user.ToJson();
            }
            return "not enough rights";
        }

        [HttpGet("[Action]")]
        public ActionResult<string> GetById(int id, string nameExecutor, string password)
        {
            UserManager manager = new UserManager(db.userRepository);
            UserEntity? executor = manager.GetByName(nameExecutor);
            UserEntity? user = manager.GetById(id);


            if (user == null || executor == null)
            {
                return NotFound();
            }

            if (password != executor.Password)
            {
                return BadRequest();
            }

            if (manager.haveRule(executor.Type, user.Type, TypeAction.get))
            {

                return user.ToJson();
            }
            return "not enough rights";
        }


    }
}
