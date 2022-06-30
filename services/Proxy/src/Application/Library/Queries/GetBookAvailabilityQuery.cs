using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;

namespace Application.Library.Queries
{
    public class GetBookAvailabilityQuery : IRequest<GetBookAvailabilityResponse>
    {
        public GetBookAvailabilityQuery(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; private set; }

        public class GetBookAvailabilityQueryHandler : IRequestHandler<GetBookAvailabilityQuery, GetBookAvailabilityResponse>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;

            public GetBookAvailabilityQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
            }

            public async Task<GetBookAvailabilityResponse> Handle(GetBookAvailabilityQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetBookAvailability(request.BookId);

                return new GetBookAvailabilityResponse { Book = response };
            }
        }
    }
}
