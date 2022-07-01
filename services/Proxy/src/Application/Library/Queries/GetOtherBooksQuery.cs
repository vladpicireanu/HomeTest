using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;
using Application.Models;

namespace Application.Library.Queries
{
    public class GetOtherBooksQuery : IRequest<GetOtherBooksResponse>
    {
        public GetOtherBooksQuery(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; private set; }

        public class GetOtherBooksQueryHandler : IRequestHandler<GetOtherBooksQuery, GetOtherBooksResponse>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;

            public GetOtherBooksQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
            }

            public async Task<GetOtherBooksResponse> Handle(GetOtherBooksQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetOtherBooks(request.BookId);

                return new GetOtherBooksResponse { Books = new List<Book>(response) };
            }
        }
    }
}
