using Application.Abstractions;
using MediatR;
using MapsterMapper;
using Application.Library.Dto;

namespace Application.Library.Queries
{
    public class GetBookReadRateQuery : IRequest<GetBookReadRateResponse>
    {
        public GetBookReadRateQuery(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; private set; }

        public class GetBookReadRateQueryHandler : IRequestHandler<GetBookReadRateQuery, GetBookReadRateResponse>
        {
            private readonly ILibraryRepository libraryRepository;
            private readonly IMapper mapper;

            public GetBookReadRateQueryHandler(ILibraryRepository libraryRepository, IMapper mapper)
            {
                this.libraryRepository = libraryRepository;
                this.mapper = mapper;
            }

            public async Task<GetBookReadRateResponse> Handle(GetBookReadRateQuery request, CancellationToken ct)
            {
                var response = await libraryRepository.GetBookReadRate(request.BookId, ct);

                return new GetBookReadRateResponse
                {
                    BookReadRate = response
                };
            }
        }
    }
}
