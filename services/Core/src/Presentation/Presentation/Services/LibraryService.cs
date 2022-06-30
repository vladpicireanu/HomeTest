using Application.Library.Queries;
using Grpc.Core;
using MapsterMapper;
using MediatR;


namespace Presentation.Services
{
    public class LibraryService : Library.LibraryBase
    {
        private readonly ILogger<LibraryService> _logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public LibraryService(ILogger<LibraryService> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public override async Task<GetBookByIdReply> GetBookById(GetBookByIdRequest request, ServerCallContext context)
        {
            var result = await mediator.Send(new GetBookByIdQuery(request.BookId), context.CancellationToken);
            
            return new GetBookByIdReply
            { 
                Book = mapper.Map<Book>(result)
            };
        }

        public override async Task<GetBookAvailabilityReply> GetBookAvailability(GetBookAvailabilityRequest request, ServerCallContext context)
        {
            var result = await mediator.Send(new GetBookAvailabilityQuery(request.BookId), context.CancellationToken);

            return mapper.Map<GetBookAvailabilityReply>(result);
        }
    }
}