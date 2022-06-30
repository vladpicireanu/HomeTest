using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;
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
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;

            public GetUsersWithMostRentsQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
            }

            public async Task<GetUsersWithMostRentsResponse> Handle(GetUsersWithMostRentsQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetUsersWithMostRents(request.TopRange, request.StartTime, request.EndTime);

                return new GetUsersWithMostRentsResponse { Users = new List<UserMostRents>(response) };
            }
        }
    }
}
