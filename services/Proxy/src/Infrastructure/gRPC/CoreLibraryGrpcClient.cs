using Application.Abstractions;
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

        public async Task<Domain.Book> GetBookById(int bookId)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(clientUrl);

            var client = new Library.LibraryClient(channel);

            var requestMessage = new GetBookByIdRequest { BookId = bookId };

            var result = await client.GetBookByIdAsync(requestMessage, GetCorrelationMetaData());

            return await Task.FromResult(mapper.Map<Domain.Book>(result.Book));
        }
    }
}
