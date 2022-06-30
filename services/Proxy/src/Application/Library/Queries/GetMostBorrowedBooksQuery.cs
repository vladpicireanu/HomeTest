using Application.Abstractions;
using MediatR;
using MapsterMapper;
using Application.Library.Dto;

namespace Application.Library.Queries
{
    public class GetMostBorrowedBooksQuery : IRequest<GetMostBorrowedBooksResponse>
    {
        public GetMostBorrowedBooksQuery(int topRange)
        {
            TopRange = topRange;
        }

        public int TopRange { get; private set; }

        public class GetMostBorrowedBooksQueryHandler : IRequestHandler<GetMostBorrowedBooksQuery, GetMostBorrowedBooksResponse>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;
            private readonly IMapper mapper;

            public GetMostBorrowedBooksQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient, IMapper mapper)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
                this.mapper = mapper;
            }

            public async Task<GetMostBorrowedBooksResponse> Handle(GetMostBorrowedBooksQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetMostBorrowedBooks(request.TopRange);

                return new GetMostBorrowedBooksResponse { Books = new List<Models.Book>(response) };
            }
        }
    }
}
