using Application.Models;
using Domain;

namespace Application.Abstractions
{
    public interface ILibraryRepository
    {
        bool SaveChanges();

        Task<Book> GetBookById(int bookId, CancellationToken ct);

        Task<BookAvailability> GetBookAvailability(int bookId, CancellationToken ct);

        Task<List<Book>> GetMostBorrowedBooks(int topRange, CancellationToken ct);

        Task<List<UserMostRents>> GetUsersWithMostRents(int topRange, DateTimeOffset startDate, DateTimeOffset returnDate, CancellationToken ct);

        Task<List<UserRent>> GetUserRents(int userId, CancellationToken ct);

        Task<List<Book>> GetOtherBooks(int bookId, CancellationToken ct);

        Task<int> GetBookReadRate(int bookId, CancellationToken ct);
    }
}
