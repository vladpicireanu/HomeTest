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

            public Task<GetBookAvailabilityResponse> Handle(GetBookAvailabilityQuery request, CancellationToken cancellationToken)
            {
                var response = mapper.Map<GetBookAvailabilityResponse>(libraryRepository.GetBookAvailability(request.BookId));

                return Task.FromResult(response);
            }
        }
    }
}
