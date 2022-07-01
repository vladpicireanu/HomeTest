using Application.Abstractions;
using Application.Models;
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

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
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
                })
                .FirstOrDefault();

            return bookAvailability;
        }

        public List<Book> GetMostBorrowedBooks(int topRange)
        {
            var mostBorrowedBooks = context.Books.OrderByDescending(book => book.Rents.Count()).ToList();

            if (topRange > context.Books.Count())
                return new List<Book>();

            return mostBorrowedBooks.GetRange(0, topRange);
        }

        public List<UserMostRents> GetUsersWithMostRents(int topRange, DateTimeOffset startDate, DateTimeOffset returnDate)
        {
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
                .ToList();

            if (topRange > context.Users.Count())
                return new List<UserMostRents>();

            return usersWithMostRents.GetRange(0, topRange);
        }

        public List<UserRent> GetUserRents(int userId)
        {
            var userRents = context.Rents.Where(rent => rent.UserId == userId)
                .Select(rent => new UserRent
                {
                    BookId = rent.BookId,
                    Name = rent.Book.Name,
                    StartDate = rent.StartDate,
                    ReturnDate = rent.ReturnDate,
                })
                .ToList();

            return userRents;
        }

        public List<Book> GetOtherBooks(int bookId)
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
                .ToList();

            return otherBooksRented;
        }

        public int GetBookReadRate(int bookId)
        {
            var bookReadRates = context.Rents.Where(rent => rent.BookId == bookId && rent.ReturnDate != null)
                .Select(rent => rent.Book.Pages / (rent.ReturnDate.Value.Day  - rent.StartDate.Day))
                .ToList();

            if (bookReadRates.Count == 0)
            {
                return 0;
            }

            return bookReadRates.Sum()/bookReadRates.Count;
        }
    }
}