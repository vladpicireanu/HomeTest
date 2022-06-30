using Application.Abstractions;
using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly LibraryDbContext context;

        public LibraryRepository(LibraryDbContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
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
            return context.Books.FirstOrDefault(b => b.BookId == Id);
        }

        public BookAvailability GetBookAvailability(int Id)
        {
            var bookAvailability = context.Books.Where(book => book.BookId == Id)
                .Select(i => new BookAvailability
                {
                    BookId = i.BookId,
                    Name = i.Name,
                    Borrowed = i.Rents.Count,
                    Available = i.Copies - i.Rents.Count
                }).FirstOrDefault();

            return bookAvailability;
        }
    }
}
