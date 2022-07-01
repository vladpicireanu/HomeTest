using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;

namespace Application.Library.Queries
{
    public class GetBookReadRateQuery : IRequest<GetBookReadRateResponse>
    {
        public GetBookReadRateQuery(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; private set; }

        public class GetBookReadRateQueryHandler : IRequestHandler<GetBookReadRateQuery, GetBookReadRateResponse>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;

            public GetBookReadRateQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
            }

            public async Task<GetBookReadRateResponse> Handle(GetBookReadRateQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetBookReadRate(request.BookId);

                return new GetBookReadRateResponse { BookReadRate = response };
            }
        }
    }
}
