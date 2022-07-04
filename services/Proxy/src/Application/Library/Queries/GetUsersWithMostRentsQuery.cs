using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;
using Application.Models;
using Microsoft.Extensions.Logging;
using Domain;

namespace Application.Library.Queries
{
    public class GetUsersWithMostRentsQuery : IRequest<GenericResponse<GetUsersWithMostRentsResponse>>
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

        public class GetUsersWithMostRentsQueryHandler : IRequestHandler<GetUsersWithMostRentsQuery, GenericResponse<GetUsersWithMostRentsResponse>>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;
            private readonly ILogger<GetUsersWithMostRentsQueryHandler> logger;

            public GetUsersWithMostRentsQueryHandler(
                ICoreLibraryGrpcClient coreLibraryGrpcClient,
                ILogger<GetUsersWithMostRentsQueryHandler> logger)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
                this.logger = logger;
            }

            public async Task<GenericResponse<GetUsersWithMostRentsResponse>> Handle(GetUsersWithMostRentsQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetUsersWithMostRents(request.TopRange, request.StartDate, request.ReturnDate, cancellationToken);

                if (!response.Users.Any())
                {
                    logger.Log(LogLevel.Error, "ErrorCode : {ErrorCode}; Message : {Message}", ErrorCode.GetUsersWithMostRentsRangeLarge, ErrorMessage.GetUsersWithMostRentsRangeLarge);
                    
                    return new GenericResponse<GetUsersWithMostRentsResponse>(new Error
                    {
                        ErrorCode = ErrorCode.GetUsersWithMostRentsRangeLarge,
                        Message = ErrorMessage.GetUsersWithMostRentsRangeLarge
                    });
                }

                return new GenericResponse<GetUsersWithMostRentsResponse>(response);
            }
        }
    }
}
