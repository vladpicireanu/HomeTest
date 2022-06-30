using Application.Abstractions;
using MediatR;
using MapsterMapper;
using Application.Library.Dto;

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
            private readonly IMapper mapper;

            public GetBookAvailabilityQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient, IMapper mapper)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
                this.mapper = mapper;
            }

            public async Task<GetBookAvailabilityResponse> Handle(GetBookAvailabilityQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetBookAvailability(request.BookId);

                return mapper.Map<GetBookAvailabilityResponse>(response);
            }
        }
    }
}
