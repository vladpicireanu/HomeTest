using Application.Abstractions;
using MediatR;
using MapsterMapper;
using Application.Library.Dto;

namespace Application.Library.Queries
{
    public class GetBookAvailabilityQuery : IRequest<GetBookAvailabilityResponse>
    {
        public GetBookAvailabilityQuery(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; private set; }

        public class GetBookAvailabilityQueryHandler : IRequestHandler<GetBookAvailabilityQuery, GetBookAvailabilityResponse>
        {
            private readonly ILibraryRepository libraryRepository;
            private readonly IMapper mapper;

            public GetBookAvailabilityQueryHandler(ILibraryRepository libraryRepository, IMapper mapper)
            {
                this.libraryRepository = libraryRepository;
                this.mapper = mapper;
            }

            public async Task<GetBookAvailabilityResponse> Handle(GetBookAvailabilityQuery request, CancellationToken ct)
            {
                var response = await libraryRepository.GetBookAvailability(request.BookId, ct);
                
                return mapper.Map<GetBookAvailabilityResponse>(response);
            }
        }
    }
}
