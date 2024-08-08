using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using OnlineLibrary.Core.Models;

namespace OnlineLibrary.Persistence
{
    public class ApplicationContext : DbContext
    {

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<AuthorEntity> Authors => Set<AuthorEntity>();
        public DbSet<BookEntity> Books => Set<BookEntity>();
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=test1;Trusted_Connection=True;");
        }
    }
}
