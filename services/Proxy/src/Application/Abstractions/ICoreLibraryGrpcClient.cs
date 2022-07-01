using Application.Models;

namespace Application.Abstractions
{
    public  interface ICoreLibraryGrpcClient
    {
        Task<Book> GetBookById(int bookId);

        Task<BookAvailability> GetBookAvailability(int bookId);

        Task<List<Book>> GetMostBorrowedBooks(int topRange);

        Task<List<UserMostRents>> GetUsersWithMostRents(int topRange, DateTimeOffset startTime, DateTimeOffset endTime);

        Task<List<UserRent>> GetUserRents(int userId);

        Task<List<Book>> GetOtherBooks(int bookId);
    }
}
