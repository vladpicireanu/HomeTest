using Application.Abstractions;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Presentation;

namespace Infrastructure.gRPC
{
    public class CoreLibraryGrpcClient : GRPCBaseClient, ICoreLibraryGrpcClient
    {
        private string clientUrl;

        public CoreLibraryGrpcClient(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            clientUrl = config.GetSection("CoreLibraryServiceHost").Value;
        }

        public async Task<Domain.Book> GetBookById(int bookId)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(clientUrl);

            var client = new Library.LibraryClient(channel);

            var requestMessage = new GetBookByIdRequest { BookId = bookId };

            var result = await client.GetBookByIdAsync(requestMessage, GetCorrelationMetaData());

            return await Task.FromResult(
                new Domain.Book
                { 
                    Id = result.Book.BookId,
                    Pages = result.Book.Pages,
                    Copies = result.Book.Copies,
                    Name = result.Book.Name
                });
        }
    }
}
