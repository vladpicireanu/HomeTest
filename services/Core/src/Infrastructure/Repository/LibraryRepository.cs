using Application.Abstractions;
using Application.Models;
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

        public Task<Book> GetBookById(int bookId, CancellationToken ct)
        {
            return context.Books.FirstOrDefaultAsync(b => b.BookId == bookId, ct);
        }

        public Task<BookAvailability> GetBookAvailability(int bookId, CancellationToken ct)
        {
            var bookAvailability = context.Books.Where(book => book.BookId == bookId)
                .Select(book => new BookAvailability
                {
                    BookId = book.BookId,
                    Name = book.Name,
                    Borrowed = book.Rents.Count,
                    Available = book.Copies - book.Rents.Count
                })
                .FirstOrDefaultAsync(ct);

            return bookAvailability;
        }

        public Task<List<Book>> GetMostBorrowedBooks(int topRange, CancellationToken ct)
        {
            if (topRange > context.Books.Count())
                return Task.FromResult(new List<Book>());

            var mostBorrowedBooks = context.Books.OrderByDescending(book => book.Rents.Count())
                .Take(topRange)
                .ToListAsync(ct);

            return mostBorrowedBooks;
        }

        public Task<List<UserMostRents>> GetUsersWithMostRents(int topRange, DateTimeOffset startDate, DateTimeOffset returnDate, CancellationToken ct)
        {
            if (topRange > context.Users.Count())
                return Task.FromResult(new List<UserMostRents>());

            var usersWithMostRents = context.Users
                .Select(user => new UserMostRents
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Rents = user.Rents.Where(rent => rent.StartDate >= startDate && rent.StartDate <= returnDate).Count()
                })
                .OrderByDescending(user => user.Rents)
                .Take(topRange)
                .ToListAsync(ct);

            return usersWithMostRents;
        }

        public Task<List<UserRent>> GetUserRents(int userId, CancellationToken ct)
        {
            var userRents = context.Rents.Where(rent => rent.UserId == userId)
                .Select(rent => new UserRent
                {
                    BookId = rent.BookId,
                    Name = rent.Book.Name,
                    StartDate = rent.StartDate,
                    ReturnDate = rent.ReturnDate,
                })
                .ToListAsync(ct);

            return userRents;
        }

        public Task<List<Book>> GetOtherBooks(int bookId, CancellationToken ct)
        {
            var otherBooksRented = context.Rents.Where(rent => 
                    rent.BookId != bookId 
                    && context.Rents.Where(r => r.BookId == bookId).Select(r => r.UserId).Contains(rent.UserId))
                .Select(rent => new Book
                {
                    BookId = rent.BookId,
                    Name = rent.Book.Name,
                    Pages = rent.Book.Pages,
                    Copies = rent.Book.Copies
                })
                .Distinct()
                .ToListAsync(ct);

            return otherBooksRented;
        }

        public Task<int> GetBookReadRate(int bookId, CancellationToken ct)
        {
            var bookReadRates = context.Rents.Where(rent => rent.BookId == bookId && rent.ReturnDate != null)
                .Select(rent => rent.Book.Pages / (rent.ReturnDate.Value.Day  - rent.StartDate.Day))
                .ToListAsync(ct);

            if (bookReadRates.Result.Count == 0)
            {
                return Task.FromResult(0);
            }

            return Task.FromResult(bookReadRates.Result.Sum()/bookReadRates.Result.Count);
        }
    }
}