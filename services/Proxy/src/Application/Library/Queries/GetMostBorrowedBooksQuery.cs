using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;
using Application.Models;
using Domain;
using Microsoft.Extensions.Logging;

namespace Application.Library.Queries
{
    public class GetMostBorrowedBooksQuery : IRequest<GenericResponse<GetMostBorrowedBooksResponse>>
    {
        public GetMostBorrowedBooksQuery(int topRange)
        {
            TopRange = topRange;
        }

        public int TopRange { get; private set; }

        public class GetMostBorrowedBooksQueryHandler : IRequestHandler<GetMostBorrowedBooksQuery, GenericResponse<GetMostBorrowedBooksResponse>>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;
            private readonly ILogger<GetMostBorrowedBooksQueryHandler> logger;

            public GetMostBorrowedBooksQueryHandler(
                ICoreLibraryGrpcClient coreLibraryGrpcClient,
                ILogger<GetMostBorrowedBooksQueryHandler> logger)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
                this.logger = logger;
            }

            public async Task<GenericResponse<GetMostBorrowedBooksResponse>> Handle(GetMostBorrowedBooksQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetMostBorrowedBooks(request.TopRange, cancellationToken);

                if (!response.Books.Any())
                {
                    logger.Log(LogLevel.Error, "ErrorCode : {ErrorCode}; Message : {Message}", ErrorCode.GetMostBorrowedBooksRangeLarge, ErrorMessage.GetMostBorrowedBooksRangeLarge);

                    return new GenericResponse<GetMostBorrowedBooksResponse>(new Error
                    {
                        ErrorCode = ErrorCode.GetMostBorrowedBooksRangeLarge,
                        Message = ErrorMessage.GetMostBorrowedBooksRangeLarge
                    });
                }

                return new GenericResponse<GetMostBorrowedBooksResponse>(response);
            }
        }
    }
}
