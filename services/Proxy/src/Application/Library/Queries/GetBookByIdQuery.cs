using Application.Abstractions;
using Domain;
using MediatR;

namespace Application.Library.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public GetBookByIdQuery(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; private set; }

        public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;

            public GetBookByIdQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
            }

            public Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
            {
                return coreLibraryGrpcClient.GetBookById(request.BookId);
            }
        }
    }
}
