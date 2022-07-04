using Application.Abstractions;
using Application.Library.Dto.Responses;
using Application.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Presentation;

namespace Infrastructure.gRPC
{
    public class CoreLibraryGrpcClient : GRPCBaseClient, ICoreLibraryGrpcClient
    {
        private string clientUrl;
        private readonly IMapper mapper;
        private readonly Library.LibraryClient client;

        public CoreLibraryGrpcClient(IConfiguration config, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(httpContextAccessor)
        {
            this.mapper = mapper;

            clientUrl = config.GetSection("CoreLibraryServiceHost").Value;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress(clientUrl);
            client = new Library.LibraryClient(channel);
        }

        public async Task<GetBookByIdResponse> GetBookById(int bookId, CancellationToken ct)
        {
            var requestMessage = new GetBookByIdRequest { BookId = bookId };

            var result = await client.GetBookByIdAsync(requestMessage, GetCorrelationMetaData(), cancellationToken: ct);

            return new GetBookByIdResponse { Book = mapper.Map<Application.Models.Book>(result.Book) };
        }

        public async Task<GetBookAvailabilityResponse> GetBookAvailability(int bookId, CancellationToken ct)
        {
            var requestMessage = new GetBookAvailabilityRequest { BookId = bookId };

            var result = await client.GetBookAvailabilityAsync(requestMessage, GetCorrelationMetaData(), cancellationToken: ct);

            return new GetBookAvailabilityResponse { Book = mapper.Map<BookAvailability>(result) };
        }

        public async Task<GetMostBorrowedBooksResponse> GetMostBorrowedBooks(int topRange, CancellationToken ct)
        {
            var requestMessage = new GetMostBorrowedBooksRequest { TopRange = topRange };

            var result = await client.GetMostBorrowedBooksAsync(requestMessage, GetCorrelationMetaData(), cancellationToken: ct);

            return new GetMostBorrowedBooksResponse {
                Books = mapper.Map<List<Application.Models.Book>>(result.Books) };
        }

        public async Task<GetUsersWithMostRentsResponse> GetUsersWithMostRents(int topRange, DateTimeOffset startDate, DateTimeOffset returnDate, CancellationToken ct)
        {
            var requestMessage = new GetUsersWithMostRentsRequest
            { 
                ReturnDate = returnDate.ToTimestamp(),
                StartDate = startDate.ToTimestamp(),
                TopRange = topRange
            };

            var result = await client.GetUsersWithMostRentsAsync(requestMessage, GetCorrelationMetaData(), cancellationToken: ct);

            return new GetUsersWithMostRentsResponse { Users = mapper.Map<List<UserMostRents>>(result.Users) };
        }

        public async Task<GetUserRentsResponse> GetUserRents(int userId, CancellationToken ct)
        {
            var requestMessage = new GetUserRentsRequest { UserId = userId };

            var result = await client.GetUserRentsAsync(requestMessage, GetCorrelationMetaData(), cancellationToken: ct);

            return new GetUserRentsResponse { UserRents = mapper.Map<List<Application.Models.UserRent>>(result.UserRents) };
        }

        public async Task<GetOtherBooksResponse> GetOtherBooks(int bookId, CancellationToken ct)
        {
            var requestMessage = new GetOtherBooksRequest { BookId = bookId };

            var result = await client.GetOtherBooksAsync(requestMessage, GetCorrelationMetaData(), cancellationToken: ct);

            return new GetOtherBooksResponse { Books = mapper.Map<List<Application.Models.Book>>(result.Books) };
        }

        public async Task<GetBookReadRateResponse> GetBookReadRate(int bookId, CancellationToken ct)
        {
            var requestMessage = new GetBookReadRateRequest { BookId = bookId };

            var result = await client.GetBookReadRateAsync(requestMessage, GetCorrelationMetaData(), cancellationToken: ct);

            return new GetBookReadRateResponse { BookReadRate = result.BookReadRate };
        }
    }
}
