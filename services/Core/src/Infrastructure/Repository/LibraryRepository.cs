using Application.Abstractions;
using Application.Models;
using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
                .Select(book => new BookAvailability
                {
                    BookId = book.BookId,
                    Name = book.Name,
                    Borrowed = book.Rents.Count,
                    Available = book.Copies - book.Rents.Count
                }).FirstOrDefault();

            return bookAvailability;
        }

        public List<Book> GetMostBorrowedBooks(int topRange)
        {
            var bookAvailability = context.Books.OrderByDescending(book => book.Rents.Count()).ToList();

            if (topRange > context.Books.Count())
                return new List<Book>();

            return bookAvailability.GetRange(0, topRange);
        }

        public List<UserMostRents> GetUsersWithMostRents(int topRange, DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var bookAvailability = context.Users
                .Select(user => new UserMostRents
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Rents = user.Rents.Where(rent => rent.StartDate >= startTime && rent.StartDate <= endTime).Count()
                })
                .OrderByDescending(user => user.Rents)
                .ToList();

            if (topRange > context.Users.Count())
                return new List<UserMostRents>();

            return bookAvailability.GetRange(0, topRange);
        }
    }
}
