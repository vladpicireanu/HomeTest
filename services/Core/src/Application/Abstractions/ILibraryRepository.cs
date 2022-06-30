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

        List<UserMostRents> GetUsersWithMostRents(int topRange, DateTimeOffset startTime, DateTimeOffset endTime);

        void CreateBook(Book book);
    }
}
