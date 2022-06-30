using Application.Abstractions;
using MediatR;
using MapsterMapper;
using Application.Library.Dto;
using Application.Models;

namespace Application.Library.Queries
{
    public class GetUsersWithMostRentsQuery : IRequest<GetUsersWithMostRentsResponse>
    {
        public GetUsersWithMostRentsQuery(int topRange, DateTimeOffset startTime, DateTimeOffset endTime)
        {
            TopRange = topRange;
            StartTime = startTime;
            EndTime = endTime;
        }

        public int TopRange { get; private set; }

        public DateTimeOffset StartTime { get; private set; }

        public DateTimeOffset EndTime { get; private set; }

        public class GetUsersWithMostRentsQueryHandler : IRequestHandler<GetUsersWithMostRentsQuery, GetUsersWithMostRentsResponse>
        {
            private readonly ILibraryRepository libraryRepository;
            private readonly IMapper mapper;

            public GetUsersWithMostRentsQueryHandler(ILibraryRepository libraryRepository, IMapper mapper)
            {
                this.libraryRepository = libraryRepository;
                this.mapper = mapper;
            }

            public Task<GetUsersWithMostRentsResponse> Handle(GetUsersWithMostRentsQuery request, CancellationToken cancellationToken)
            {
                var response = new GetUsersWithMostRentsResponse
                {
                    Users = mapper.Map<List<UserMostRents>>(libraryRepository.GetUsersWithMostRents(request.TopRange, request.StartTime, request.EndTime))
                };

                return Task.FromResult(response);
            }
        }
    }
}
