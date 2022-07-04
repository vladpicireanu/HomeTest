using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;
using Application.Models;
using Microsoft.Extensions.Logging;
using Domain;

namespace Application.Library.Queries
{
    public class GetOtherBooksQuery : IRequest<GenericResponse<GetOtherBooksResponse>>
    {
        public GetOtherBooksQuery(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; private set; }

        public class GetOtherBooksQueryHandler : IRequestHandler<GetOtherBooksQuery, GenericResponse<GetOtherBooksResponse>>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;
            private readonly ILogger<GetOtherBooksQueryHandler> logger;

            public GetOtherBooksQueryHandler(
                ICoreLibraryGrpcClient coreLibraryGrpcClient,
                ILogger<GetOtherBooksQueryHandler> logger)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
                this.logger = logger;
            }

            public async Task<GenericResponse<GetOtherBooksResponse>> Handle(GetOtherBooksQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetOtherBooks(request.BookId, cancellationToken);

                if (!response.Books.Any())
                {
                    logger.Log(LogLevel.Error, "ErrorCode : {ErrorCode}; Message : {Message}", 
                        ErrorCode.GetOtherBooksNoData, ErrorMessage.GetOtherBooksNoData);

                    return new GenericResponse<GetOtherBooksResponse>(new Error
                    {
                        ErrorCode = ErrorCode.GetOtherBooksNoData,
                        Message = ErrorMessage.GetOtherBooksNoData
                    });
                }

                return new GenericResponse<GetOtherBooksResponse>(response);
            }
        }
    }
}
