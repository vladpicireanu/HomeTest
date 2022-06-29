using Application.Abstractions;
using Domain;
using Infrastructure.Persistence;

namespace Infrastructure.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly LibraryDbContext context;

        public LibraryRepository(LibraryDbContext context)
        {
            this.context = context;
        }

        public void CreateBook(Book book)
        {
            if (book != null)
            {
                context.Books.Add(book);
            }
        }

        public Book GetBookById(int Id)
        {
            return context.Books.FirstOrDefault(b => b.Id == Id);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
