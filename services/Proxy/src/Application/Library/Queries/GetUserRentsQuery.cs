using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;
using Application.Models;

namespace Application.Library.Queries
{
    public class GetUserRentsQuery : IRequest<GetUserRentsResponse>
    {
        public GetUserRentsQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; private set; }

        public class GetUserRentsQueryHandler : IRequestHandler<GetUserRentsQuery, GetUserRentsResponse>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;

            public GetUserRentsQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
            }

            public async Task<GetUserRentsResponse> Handle(GetUserRentsQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetUserRents(request.UserId);

                return new GetUserRentsResponse { UserRents = new List<UserRent>(response) };
            }
        }
    }
}
