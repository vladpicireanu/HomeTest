using Domain;

namespace Application.Abstractions
{
    public  interface ICoreLibraryGrpcClient
    {
        Task<Domain.Book> GetBookById(int bookId);

        Task<BookAvailability> GetBookAvailability(int bookId);
    }
}
