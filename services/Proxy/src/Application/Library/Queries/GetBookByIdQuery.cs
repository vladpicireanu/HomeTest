using Application.Abstractions;
using MediatR;
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

            public GetBookByIdQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
            }

            public async Task<GetBookByIdResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetBookById(request.BookId);

                return new GetBookByIdResponse { Book = response };
            }
        }
    }
}
