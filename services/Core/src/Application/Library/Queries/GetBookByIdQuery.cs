using Application.Abstractions;
using MediatR;
using MapsterMapper;
using Application.Library.Dto;
using Application.Models;

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
            private readonly ILibraryRepository libraryRepository;
            private readonly IMapper mapper;

            public GetBookByIdQueryHandler(ILibraryRepository libraryRepository, IMapper mapper)
            {
                this.libraryRepository = libraryRepository;
                this.mapper = mapper;
            }

            public async Task<GetBookByIdResponse> Handle(GetBookByIdQuery request, CancellationToken ct)
            {
                var response = await libraryRepository.GetBookById(request.BookId, ct);
                
                return new GetBookByIdResponse
                {
                    Book = mapper.Map<BookModel>(response)
                };
            }
        }
    }
}
