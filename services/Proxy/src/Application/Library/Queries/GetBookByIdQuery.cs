using Application.Abstractions;
using MediatR;
using MapsterMapper;
using Application.Library.Dto;

namespace Application.Library.Queries
{
    public class GetBookByIdQuery : IRequest<GetBookByIdResponse>
    {
        public GetBookByIdQuery(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; private set; }

        public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, GetBookByIdResponse>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;
            private readonly IMapper mapper;

            public GetBookByIdQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient, IMapper mapper)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
                this.mapper = mapper;
            }

            public async Task<GetBookByIdResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetBookById(request.BookId);

                return mapper.Map<GetBookByIdResponse>(response);
            }
        }
    }
}
