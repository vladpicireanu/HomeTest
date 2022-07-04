using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;
using Application.Models;
using Domain;
using Microsoft.Extensions.Logging;

namespace Application.Library.Queries
{
    public class GetBookAvailabilityQuery : IRequest<GenericResponse<GetBookAvailabilityResponse>>
    {
        public GetBookAvailabilityQuery(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; private set; }

        public class GetBookAvailabilityQueryHandler : IRequestHandler<GetBookAvailabilityQuery, GenericResponse<GetBookAvailabilityResponse>>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;
            private readonly ILogger<GetBookAvailabilityQueryHandler> logger;

            public GetBookAvailabilityQueryHandler(
                ICoreLibraryGrpcClient coreLibraryGrpcClient, 
                ILogger<GetBookAvailabilityQueryHandler> logger)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
                this.logger = logger;
            }

            public async Task<GenericResponse<GetBookAvailabilityResponse>> Handle(GetBookAvailabilityQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetBookAvailability(request.BookId, cancellationToken);

                if (response.Book.BookId == default(int))
                {
                    logger.Log(LogLevel.Error, "ErrorCode : {ErrorCode}; Message : {Message}",
                        ErrorCode.GetBookAvailabilityNotFound, ErrorMessage.GetBookAvailabilityNotFound);

                    return new GenericResponse<GetBookAvailabilityResponse>(new Error
                    {
                        ErrorCode = ErrorCode.GetBookAvailabilityNotFound,
                        Message = ErrorMessage.GetBookAvailabilityNotFound
                    });
                }

                return new GenericResponse<GetBookAvailabilityResponse>(response);
            }
        }
    }
}
