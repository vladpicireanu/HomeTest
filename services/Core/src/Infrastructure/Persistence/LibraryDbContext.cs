using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        { 

        }

        public DbSet<Rent> Rents { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }
    }
}
