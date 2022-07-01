using Application.Abstractions;
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

        public CoreLibraryGrpcClient(IConfiguration config, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(httpContextAccessor)
        {
            clientUrl = config.GetSection("CoreLibraryServiceHost").Value;
            this.mapper = mapper;
        }

        public async Task<Application.Models.Book> GetBookById(int bookId)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(clientUrl);

            var client = new Library.LibraryClient(channel);

            var requestMessage = new GetBookByIdRequest { BookId = bookId };

            var result = await client.GetBookByIdAsync(requestMessage, GetCorrelationMetaData());

            return mapper.Map<Application.Models.Book>(result.Book);
        }

        public async Task<BookAvailability> GetBookAvailability(int bookId)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(clientUrl);

            var client = new Library.LibraryClient(channel);

            var requestMessage = new GetBookAvailabilityRequest { BookId = bookId };

            var result = await client.GetBookAvailabilityAsync(requestMessage, GetCorrelationMetaData());

            return mapper.Map<BookAvailability>(result);
        }

        public async Task<List<Application.Models.Book>> GetMostBorrowedBooks(int topRange)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(clientUrl);

            var client = new Library.LibraryClient(channel);

            var requestMessage = new GetMostBorrowedBooksRequest { TopRange = topRange };

            var result = await client.GetMostBorrowedBooksAsync(requestMessage, GetCorrelationMetaData());

            return mapper.Map<List<Application.Models.Book>>(result.Books);
        }

        public async Task<List<UserMostRents>> GetUsersWithMostRents(int topRange, DateTimeOffset startDate, DateTimeOffset returnDate)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(clientUrl);
            var client = new Library.LibraryClient(channel);

            var requestMessage = new GetUsersWithMostRentsRequest
            { 
                ReturnDate = returnDate.ToTimestamp(),
                StartDate = startDate.ToTimestamp(),
                TopRange = topRange
            };

            var result = await client.GetUsersWithMostRentsAsync(requestMessage, GetCorrelationMetaData());

            return mapper.Map<List<UserMostRents>>(result.Users);
        }

        public async Task<List<Application.Models.UserRent>> GetUserRents(int userId)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(clientUrl);

            var client = new Library.LibraryClient(channel);

            var requestMessage = new GetUserRentsRequest { UserId = userId };

            var result = await client.GetUserRentsAsync(requestMessage, GetCorrelationMetaData());

            return mapper.Map<List<Application.Models.UserRent>>(result.UserRents);
        }

        public async Task<List<Application.Models.Book>> GetOtherBooks(int bookId)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(clientUrl);

            var client = new Library.LibraryClient(channel);

            var requestMessage = new GetOtherBooksRequest { BookId = bookId };

            var result = await client.GetOtherBooksAsync(requestMessage, GetCorrelationMetaData());

            return mapper.Map<List<Application.Models.Book>>(result.Books);
        }
    }
}
