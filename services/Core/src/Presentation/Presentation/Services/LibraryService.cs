using Application.Library.Queries;
using Grpc.Core;
using MediatR;


namespace Presentation.Services
{
    public class LibraryService : Library.LibraryBase
    {
        private readonly ILogger<LibraryService> _logger;
        private readonly IMediator mediator;

        public LibraryService(ILogger<LibraryService> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        public override async Task<GetBookByIdReply> GetBookById(GetBookByIdRequest request, ServerCallContext context)
        {
            var result = await mediator.Send(new GetBookByIdQuery(request.BookId), context.CancellationToken);
            
            return new GetBookByIdReply
            {
                Book = new Book
                { 
                    BookId = result.Id,
                    Copies = result.Copies,
                    Pages = result.Pages,
                    Name = result.Name
                }
            };
        }
    }
}