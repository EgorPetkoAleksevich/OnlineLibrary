using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using OnlineLibrary.Core.Models;
using OnlineLibrary.Core.Storage;
using static OnlineLibrary.Persistence.ApplicationContext;

namespace OnlineLibrary.Persistence
{
    public class ApplicationContext : DbContext
    {

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<AuthorEntity> Authors => Set<AuthorEntity>();
        public DbSet<BookEntity> Books => Set<BookEntity>();

        public UserRepository userRepository;
        public BookRepository bookRepository;
        public AuthorRepository authorRepository;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            userRepository = new UserRepository(this);
            bookRepository = new BookRepository(this);
            authorRepository = new AuthorRepository(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=test1;Trusted_Connection=True;");
        }

        public class UserRepository : IRepository<UserEntity>
        {
            public ApplicationContext Db{ get; set; }
            public UserRepository(ApplicationContext db)
            {
                Db = db;
            }
            public void Add(UserEntity item)
            {
                Db.Users.Add(item);
            }

            public void Delete(UserEntity item)
            {
                Db.Users.Remove(item);
            }
            
            public List<UserEntity> GetAll()
            {
                return Db.Users.ToList();
            }
            public void Save()
            {
                Db.SaveChanges();
            }
        }

        public class BookRepository : IRepository<BookEntity>
        {
            public ApplicationContext Db { get; set; }
            public BookRepository(ApplicationContext db)
            {
                Db = db;
            }
            public void Add(BookEntity item)
            {
                Db.Books.Add(item);
            }

            public void Delete(BookEntity item)
            {
                Db.Books.Remove(item);
            }

            public List<BookEntity> GetAll()
            {
                return Db.Books.ToList();
            }
            public void Save()
            {
                Db.SaveChanges();
            }
        }

        public class AuthorRepository : IRepository<AuthorEntity>
        {
            public ApplicationContext Db { get; set; }
            public AuthorRepository(ApplicationContext db)
            {
                Db = db;
            }
            public void Add(AuthorEntity item)
            {
                Db.Authors.Add(item);
            }

            public void Delete(AuthorEntity item)
            {
                Db.Authors.Remove(item);
            }

            public List<AuthorEntity> GetAll()
            {
                return Db.Authors.ToList();
            }
            public void Save()
            {
                Db.SaveChanges();
            }
        }

    }
    
}
