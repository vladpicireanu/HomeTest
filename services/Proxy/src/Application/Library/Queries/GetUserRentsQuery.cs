using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;
using Application.Models;
using Microsoft.Extensions.Logging;
using Domain;

namespace Application.Library.Queries
{
    public class GetUserRentsQuery : IRequest<GenericResponse<GetUserRentsResponse>>
    {
        public GetUserRentsQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; private set; }

        public class GetUserRentsQueryHandler : IRequestHandler<GetUserRentsQuery, GenericResponse<GetUserRentsResponse>>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;
            private readonly ILogger<GetUserRentsQueryHandler> logger;

            public GetUserRentsQueryHandler(
                ICoreLibraryGrpcClient coreLibraryGrpcClient,
                ILogger<GetUserRentsQueryHandler> logger)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
                this.logger = logger;
            }

            public async Task<GenericResponse<GetUserRentsResponse>> Handle(GetUserRentsQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetUserRents(request.UserId, cancellationToken);

                if (!response.UserRents.Any())
                {
                    logger.Log(LogLevel.Error, "ErrorCode : {ErrorCode}; Message : {Message}", 
                        ErrorCode.GetUserRentsNoData, ErrorMessage.GetUserRentsNoData);

                    return new GenericResponse<GetUserRentsResponse>(new Error
                    {
                        ErrorCode = ErrorCode.GetUserRentsNoData,
                        Message = ErrorMessage.GetUserRentsNoData
                    });
                }

                return new GenericResponse<GetUserRentsResponse>(response);
            }
        }
    }
}
