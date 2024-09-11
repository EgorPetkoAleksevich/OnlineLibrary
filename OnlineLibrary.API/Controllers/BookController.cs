using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using OnlineLibrary.Application;
using OnlineLibrary.Core.Models;
using OnlineLibrary.Persistence;

namespace OnlineLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ApplicationContext db;

        public BookController(ApplicationContext db)
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
                    StatusToProduction = TypeStatusToProductionBook.author
                }
            );

            db.SaveChanges();
            return "asd";


        }


        [HttpGet("[Action]")]
        public ActionResult<string> GetInfoById(int id)
        {
            //var a = db.Books.Include("Authors").ToList();
            BookManager manager = new BookManager(db.bookRepository);
            BookEntity? book = manager.GetById(id);
            
            if(book == null)
            {
                return "no found";
            }

            return book.ToJson();
        }

        [HttpGet("[Action]")]
        public ActionResult<string> GetInfoByName(string? name)
        {
            BookManager manager = new BookManager(db.bookRepository);
            BookEntity? book = manager.GetByName(name);
            if(book == null)
            {
                return "no found";
            }
            
            return book.UsersTo.ToJson();
        }

    }
}
