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
                Book = mapper.Map<Book>(result.Book)
            };
        }

        public override async Task<GetBookAvailabilityReply> GetBookAvailability(GetBookAvailabilityRequest request, ServerCallContext context)
        {
            var result = await mediator.Send(new GetBookAvailabilityQuery(request.BookId), context.CancellationToken);

            return mapper.Map<GetBookAvailabilityReply>(result);
        }

        public override async Task<GetMostBorrowedBooksReply> GetMostBorrowedBooks(GetMostBorrowedBooksRequest request, ServerCallContext context)
        {
            var result = await mediator.Send(new GetMostBorrowedBooksQuery(request.TopRange), context.CancellationToken);
            var response = new GetMostBorrowedBooksReply();

            response.Books.AddRange(mapper.Map<List<Book>>(result.Books));

            return response;
        }

        public override async Task<GetUsersWithMostRentsReply> GetUsersWithMostRents(GetUsersWithMostRentsRequest request, ServerCallContext context)
        {
            var queryRequest = new GetUsersWithMostRentsQuery(
                request.TopRange, request.StartDate.ToDateTimeOffset(), request.ReturnDate.ToDateTimeOffset());

            var result = await mediator.Send(queryRequest, context.CancellationToken);
            var response = new GetUsersWithMostRentsReply();

            response.Users.AddRange(mapper.Map<List<User>>(result.Users));

            return response;
        }

        public override async Task<GetUserRentsReply> GetUserRents(GetUserRentsRequest request, ServerCallContext context)
        {
            var result = await mediator.Send(new GetUserRentsQuery(request.UserId), context.CancellationToken);
            var response = new GetUserRentsReply();

            response.UserRents.AddRange(mapper.Map<List<UserRent>>(result.UserRents));

            return response;
        }

        public override async Task<GetOtherBooksReply> GetOtherBooks(GetOtherBooksRequest request, ServerCallContext context)
        {
            var result = await mediator.Send(new GetOtherBooksQuery(request.BookId), context.CancellationToken);
            var response = new GetOtherBooksReply();

            response.Books.AddRange(mapper.Map<List<Book>>(result.Books));

            return response;
        }

        public override async Task<GetBookReadRateReply> GetBookReadRate(GetBookReadRateRequest request, ServerCallContext context)
        {
            var result = await mediator.Send(new GetBookReadRateQuery(request.BookId), context.CancellationToken);
            
            return new GetBookReadRateReply
            {
                BookReadRate = result.BookReadRate
            };
        }
    }
}