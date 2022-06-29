using Application.Abstractions;
using Domain;
using MediatR;

namespace Application.Library.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public GetBookByIdQuery(int bookId)
        {
            Id = bookId;
        }

        public int Id { get; private set; }

        public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
        {
            private readonly ILibraryRepository libraryRepository;

            public GetBookByIdQueryHandler(ILibraryRepository libraryRepository)
            {
                this.libraryRepository = libraryRepository;
            }

            public Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
            {
                return Task.FromResult(libraryRepository.GetBookById(request.Id));
            }
        }
    }
}
