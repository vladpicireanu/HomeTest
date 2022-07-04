using Application.Library.Dto.Responses;

namespace Application.Abstractions
{
    public  interface ICoreLibraryGrpcClient
    {
        Task<GetBookByIdResponse> GetBookById(int bookId, CancellationToken cancellationToken);

        Task<GetBookAvailabilityResponse> GetBookAvailability(int bookId, CancellationToken cancellationToken);

        Task<GetMostBorrowedBooksResponse> GetMostBorrowedBooks(int topRange, CancellationToken cancellationToken);

        Task<GetUsersWithMostRentsResponse> GetUsersWithMostRents(int topRange, DateTimeOffset startTime, DateTimeOffset endTime, CancellationToken cancellationToken);

        Task<GetUserRentsResponse> GetUserRents(int userId, CancellationToken cancellationToken);

        Task<GetOtherBooksResponse> GetOtherBooks(int bookId, CancellationToken cancellationToken);

        Task<GetBookReadRateResponse> GetBookReadRate(int bookId, CancellationToken cancellationToken);
    }
}
