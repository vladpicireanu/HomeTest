using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;
using Application.Models;

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

            public GetMostBorrowedBooksQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
            }

            public async Task<GetMostBorrowedBooksResponse> Handle(GetMostBorrowedBooksQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetMostBorrowedBooks(request.TopRange);

                return new GetMostBorrowedBooksResponse { Books = new List<Book>(response) };
            }
        }
    }
}
