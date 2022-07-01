using Application.Models;
using Domain;

namespace Application.Abstractions
{
    public interface ILibraryRepository
    {
        bool SaveChanges();

        Book GetBookById(int Id);

        BookAvailability GetBookAvailability(int Id);

        List<Book> GetMostBorrowedBooks(int topRange);

        List<UserMostRents> GetUsersWithMostRents(int topRange, DateTimeOffset startDate, DateTimeOffset returnDate);

        List<UserRent> GetUserRents(int userId);

        List<Book> GetOtherBooks(int bookId);

        void CreateBook(Book book);
    }
}
