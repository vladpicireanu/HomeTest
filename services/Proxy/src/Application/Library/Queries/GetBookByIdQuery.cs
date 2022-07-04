using Application.Abstractions;
using Application.Library.Dto.Responses;
using Application.Models;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Library.Queries
{
    public class GetBookByIdQuery : IRequest<GenericResponse<GetBookByIdResponse>>
    {
        public GetBookByIdQuery(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; private set; }

        public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, GenericResponse<GetBookByIdResponse>>
        {
            private readonly ICoreLibraryGrpcClient coreLibraryGrpcClient;
            private readonly ILogger<GetBookByIdQueryHandler> logger;

            public GetBookByIdQueryHandler(ICoreLibraryGrpcClient coreLibraryGrpcClient, ILogger<GetBookByIdQueryHandler> logger)
            {
                this.coreLibraryGrpcClient = coreLibraryGrpcClient;
                this.logger = logger;
            }

            public async Task<GenericResponse<GetBookByIdResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
            {
                var response = await coreLibraryGrpcClient.GetBookById(request.BookId, cancellationToken);

                if (response.Book is null)
                {
                    logger.Log(LogLevel.Error, "ErrorCode : {ErrorCode}; Message : {Message}", ErrorCode.GetBookByIdNotFound, ErrorMessage.GetBookByIdNotFound);

                    return new GenericResponse<GetBookByIdResponse>(new Error
                    {
                        ErrorCode = ErrorCode.GetBookByIdNotFound,
                        Message = ErrorMessage.GetBookByIdNotFound
                    });
                }
                
                return new GenericResponse<GetBookByIdResponse>(response);
            }
        }
    }
}
