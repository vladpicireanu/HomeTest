using Application.Abstractions;
using MediatR;
using Application.Library.Dto.Responses;
using Microsoft.Extensions.Logging;
using Application.Models;
using Domain;

namespace Application.Library.Queries
{
    public class GetBookReadRateQuery : IRequest<GenericResponse<GetBookReadRateResponse>>
    {
        public GetBookReadRateQuery(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; private set; }

        public class GetBookReadRateQueryHandler : IRequestHandler<GetBookReadRateQuery, GenericResponse<GetBookReadRateResponse>>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;
            private readonly ILogger<GetBookReadRateQueryHandler> logger;

            public GetBookReadRateQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient, ILogger<GetBookReadRateQueryHandler> logger)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
                this.logger = logger;
            }

            public async Task<GenericResponse<GetBookReadRateResponse>> Handle(GetBookReadRateQuery request, CancellationToken cancellationToken)
            {
                var getBook = await coreLibraryGrpcClient.GetBookById(request.BookId, cancellationToken);

                if (getBook.Book is null)
                {
                    logger.Log(LogLevel.Error, "ErrorCode : {ErrorCode}; Message : {Message}", ErrorCode.GetBookReadRateNotFound, ErrorMessage.GetBookReadRateNotFound);

                    return new GenericResponse<GetBookReadRateResponse>(new Error
                    {
                        ErrorCode = ErrorCode.GetBookReadRateNotFound,
                        Message = ErrorMessage.GetBookReadRateNotFound
                    });
                }

                var response = await coreLibraryGrpcClient.GetBookReadRate(request.BookId, cancellationToken);

                if (response.BookReadRate == default(int))
                {
                    logger.Log(LogLevel.Error, "ErrorCode : {ErrorCode}; Message : {Message}", ErrorCode.GetBookReadRateNoData, ErrorMessage.GetBookReadRateNoData);

                    return new GenericResponse<GetBookReadRateResponse>(new Error
                    {
                        ErrorCode = ErrorCode.GetBookReadRateNoData,
                        Message = ErrorMessage.GetBookReadRateNoData
                    });
                }

                return new GenericResponse<GetBookReadRateResponse>(response);
            }
        }
    }
}
