using Application.Abstractions;
using MediatR;
using MapsterMapper;
using Application.Library.Dto;
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
            private readonly ILibraryRepository libraryRepository;
            private readonly IMapper mapper;

            public GetMostBorrowedBooksQueryHandler(ILibraryRepository libraryRepository, IMapper mapper)
            {
                this.libraryRepository = libraryRepository;
                this.mapper = mapper;
            }

            public async Task<GetMostBorrowedBooksResponse> Handle(GetMostBorrowedBooksQuery request, CancellationToken ct)
            {
                var response = await libraryRepository.GetMostBorrowedBooks(request.TopRange, ct);

                return new GetMostBorrowedBooksResponse
                {
                    Books = mapper.Map<List<BookModel>>(response)
                };
            }
        }
    }
}
