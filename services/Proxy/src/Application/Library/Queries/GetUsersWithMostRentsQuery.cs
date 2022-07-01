using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;
using Application.Models;

namespace Application.Library.Queries
{
    public class GetUsersWithMostRentsQuery : IRequest<GetUsersWithMostRentsResponse>
    {
        public GetUsersWithMostRentsQuery(int topRange, DateTimeOffset startDate, DateTimeOffset returnDate)
        {
            TopRange = topRange;
            StartDate = startDate;
            ReturnDate = returnDate;
        }

        public int TopRange { get; private set; }

        public DateTimeOffset StartDate { get; private set; }

        public DateTimeOffset ReturnDate { get; private set; }

        public class GetUsersWithMostRentsQueryHandler : IRequestHandler<GetUsersWithMostRentsQuery, GetUsersWithMostRentsResponse>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;

            public GetUsersWithMostRentsQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
            }

            public async Task<GetUsersWithMostRentsResponse> Handle(GetUsersWithMostRentsQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetUsersWithMostRents(request.TopRange, request.StartDate, request.ReturnDate);

                return new GetUsersWithMostRentsResponse { Users = new List<UserMostRents>(response) };
            }
        }
    }
}
