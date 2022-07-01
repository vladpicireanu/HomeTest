using Application.Abstractions;
using MediatR;
using MapsterMapper;
using Application.Library.Dto;
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
            private readonly ILibraryRepository libraryRepository;
            private readonly IMapper mapper;

            public GetOtherBooksQueryHandler(ILibraryRepository libraryRepository, IMapper mapper)
            {
                this.libraryRepository = libraryRepository;
                this.mapper = mapper;
            }

            public Task<GetOtherBooksResponse> Handle(GetOtherBooksQuery request, CancellationToken cancellationToken)
            {
                var response = new GetOtherBooksResponse
                {
                    Books = mapper.Map<List<BookModel>>(libraryRepository.GetOtherBooks(request.BookId))
                };

                return Task.FromResult(response);
            }
        }
    }
}
