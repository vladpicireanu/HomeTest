using Application.Abstractions;
using MediatR;
using MapsterMapper;
using Application.Library.Dto;

namespace Application.Library.Queries
{
    public class GetBookByIdQuery : IRequest<GetBookByIdResponse>
    {
        public GetBookByIdQuery(int bookId)
        {
            Id = bookId;
        }

        public int Id { get; private set; }

        public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, GetBookByIdResponse>
        {
            private readonly ILibraryRepository libraryRepository;
            private readonly IMapper mapper;

            public GetBookByIdQueryHandler(ILibraryRepository libraryRepository, IMapper mapper)
            {
                this.libraryRepository = libraryRepository;
                this.mapper = mapper;
            }

            public Task<GetBookByIdResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
            {
                var response = mapper.Map<GetBookByIdResponse>(libraryRepository.GetBookById(request.Id));

                return Task.FromResult(response);
            }
        }
    }
}
