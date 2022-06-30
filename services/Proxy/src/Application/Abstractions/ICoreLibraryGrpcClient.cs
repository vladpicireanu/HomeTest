using Application.Models;

namespace Application.Abstractions
{
    public  interface ICoreLibraryGrpcClient
    {
        Task<Book> GetBookById(int bookId);

        Task<BookAvailability> GetBookAvailability(int bookId);

        Task<List<Book>> GetMostBorrowedBooks(int topRange);
    }
}
